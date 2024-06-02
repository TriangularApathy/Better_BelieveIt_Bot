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
                    logging.SetMinimumLevel(LogLevel.Trace);
                })
                .ConfigureServices(services => {
                    // add all services here
                    Console.WriteLine("Configuring services...");
                    try {                        
                        services.AddSingleton<DiscordSocketClient>();
                        Console.WriteLine("Successfully added [DiscordSocketClient]");
                    }
                    catch { Console.WriteLine("Failed to add [DiscordSocketClient]"); }
                    try {                   
                        services.AddSingleton<InteractionService>();
                        Console.WriteLine("Successfully added [InteractionService]");
                    }
                    catch { Console.WriteLine("Failed to add [InteractionService]"); }
                    try {
                        services.AddSingleton<InteractionHandlingService>();
                        Console.WriteLine("Successfully added [InteractionHandlingService]");
                    }
                    catch { Console.WriteLine("Failed to add [InteractionHandlingService]"); }
                    try {
                        services.AddSingleton<DiscordStartupService>();
                        Console.WriteLine("Successfully added [DiscordStartupService]");
                    }
                    catch { Console.WriteLine("Failed to add [DiscordStartupService]"); }
                })
                .Build();
            Console.WriteLine("Finished building host");
            await host.RunAsync();
        }
    }
}