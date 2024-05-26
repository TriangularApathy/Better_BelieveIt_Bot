using Discord;
using Discord.WebSocket;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Better_BelieveIt_Bot {
    internal class Program {
        private static DiscordSocketClient _client = null!;

        // Dependency injection
        private static IServiceProvider _serviceProvider;


        // Dependency injection
        static IServiceProvider CreateProvider() {
            var collection = new ServiceCollection();
            //...
            return collection.BuildServiceProvider();
        }

        static IServiceProvider CreateServices() {
            var config = new DiscordSocketConfig() {
                // MessageCacheSize = 100
                //...
            };

            var collection = new ServiceCollection()
                .AddSingleton(config)
                .AddSingleton<DiscordSocketClient>();

            return collection.BuildServiceProvider();
        }

        public static async Task Main(string[] args) {
            IConfigurationBuilder configurationBuilder = new ConfigurationBuilder()
                .AddUserSecrets<Program>();
            IConfigurationRoot configuration = configurationBuilder.Build();

            // Dependency injection
            _serviceProvider = CreateProvider();

            string token = configuration["BotToken"];
            if (string.IsNullOrWhiteSpace(token)) {
                string timestamp = await GetCurrentTimestamp();
                Console.WriteLine($"[{timestamp}] Error: No discord token found. Please provide a token via the Secret Manager/User Secrets.");
                Environment.Exit(1);
            }

            var _config = new DiscordSocketConfig { MessageCacheSize = 100 };
            _client = new DiscordSocketClient(_config);

            await _client.LoginAsync(TokenType.Bot, token);
            await _client.StartAsync();

            // Events
            _client.Log += Log;
            _client.Ready += async () => {
                string timestamp = await GetCurrentTimestamp();
                Console.WriteLine($"[{timestamp}] Better-BelieveIt-Bot is connected and Ready, Believe it!");
            };

            await Task.Delay(-1);
        }

        private static async Task Log(LogMessage msg) {
            string timestamp = await GetCurrentTimestamp();
            Console.WriteLine($"[{timestamp}] {msg.ToString()}");
        }

        private static async Task<string> GetCurrentTimestamp() {
            string timestamp = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");

            // Dummy await
            await Task.CompletedTask;
            return timestamp;
        }


        // Needs privileged intent. Will just use slash commands instead
        // _client.MessageUpdated += MessageUpdated; // (goes in Main)
        /*
         * private static async Task MessageUpdated(Cacheable<IMessage, ulong> before, SocketMessage after, ISocketMessageChannel channel) {
            // If the message was not in the cache, downloading it will result in getting a copy of `after`.
            var message = await before.GetOrDownloadAsync();

            // Testing
            Console.WriteLine($"before: {before}");
            Console.WriteLine($"after: {after}");
            Console.WriteLine($"channel: {channel}");

            string timestamp = await GetCurrentTimestamp();
            Console.WriteLine($"[{timestamp}] {message} -> {after}");
        }
        */
    }
}
