using Better_BelieveIt_Bot.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Discord.WebSocket;
using Discord.Interactions;
using Microsoft.Extensions.Logging;

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
                    services.AddSingleton<InteractionService>();
                    services.AddSingleton<InteractionHandlingService>();
                    services.AddSingleton<DiscordStartupService>();
                })
                .Build();
            await host.RunAsync();
        }
    }
}