using Better_BelieveIt_Bot.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Discord.WebSocket;
using Discord.Interactions;
using Microsoft.Extensions.Logging;
using Discord;

namespace Better_BelieveIt_Bot {
    internal class Program {
        public static async Task Main(string[] args) {
            using IHost host = Host.CreateDefaultBuilder(args)
                .ConfigureAppConfiguration(config => {
                    config.AddUserSecrets<Program>();
                })
                .ConfigureLogging(logging => {
                    logging.ClearProviders();
                    logging.AddConsole();
                    logging.SetMinimumLevel(LogLevel.Information);
                })
                .ConfigureServices(services => {
                    // add all services here
                    services.AddSingleton(new DiscordSocketClient(
                    new DiscordSocketConfig {
                        GatewayIntents = GatewayIntents.AllUnprivileged,
                        FormatUsersInBidirectionalUnicode = false,
                        // Add GatewayIntents.GuildMembers to the GatewayIntents and change this to true if you want to download all users on startup
                        AlwaysDownloadUsers = false,
                        LogGatewayIntentWarnings = false,
                        LogLevel = LogSeverity.Info
                    }));
                    services.AddSingleton(x => new InteractionService(x.GetRequiredService<DiscordSocketClient>(), new InteractionServiceConfig() {
                        LogLevel = LogSeverity.Info
                    }));
                    services.AddHostedService<InteractionHandlingService>();
                    services.AddHostedService<DiscordStartupService>();
                })
                .Build();
            await host.RunAsync();
        }
    }
}