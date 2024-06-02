using Better_BelieveIt_Bot.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Discord.WebSocket;
using Discord.Interactions;
using Microsoft.Extensions.Logging;
using Discord.Rest;

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
                    logging.SetMinimumLevel(LogLevel.Debug);
                })
                .ConfigureServices(services => {
                    // add all services here
                    services.AddSingleton<DiscordSocketClient>();
                    services.AddSingleton<DiscordRestClient>();
                    services.AddSingleton<InteractionService>();
                    services.AddHostedService<InteractionHandlingService>();
                    services.AddHostedService<DiscordStartupService>();
                })
                .Build();
            await host.RunAsync();
        }
    }
}