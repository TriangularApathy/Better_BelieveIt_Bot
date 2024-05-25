using Discord;
using Discord.WebSocket;
using Microsoft.Extensions.Configuration;

namespace Better_BelieveIt_Bot {
    internal class Program {
        private static DiscordSocketClient _client;

        public static async Task Main() {
            IConfigurationBuilder configurationBuilder = new ConfigurationBuilder()
                .AddUserSecrets<Program>();
            IConfigurationRoot configuration = configurationBuilder.Build();

            string token = configuration["BotToken"];
            Console.WriteLine(token);


            //string token = Environment.GetEnvironmentVariable("BOT_TOKEN");
            if (string.IsNullOrWhiteSpace(token)) {
                Console.WriteLine("Error: No discord token found. Please provide a token via the DISCORD_TOKEN environment variable.");
                Environment.Exit(1);
            }

            var _config = new DiscordSocketConfig { MessageCacheSize = 100 };
            _client = new DiscordSocketClient(_config);

            await _client.LoginAsync(TokenType.Bot, token);
            await _client.StartAsync();

            // Events
            _client.Log += Log;
            _client.MessageUpdated += MessageUpdated;
            _client.Ready += () => {
                var timestamp = GetCurrentTimestamp();
                Console.WriteLine($"[{timestamp}] Better-BelieveIt-Bot is connected and Ready!,\nBelieve it!");
                return Task.CompletedTask;
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

        private static async Task MessageUpdated(Cacheable<IMessage, ulong> before, SocketMessage after, ISocketMessageChannel channel) {
            // If the message was not in the cache, downloading it will result in getting a copy of `after`.
            var message = await before.GetOrDownloadAsync();
            string timestamp = await GetCurrentTimestamp();
            Console.WriteLine($"[{timestamp}] {message} -> {after}");
        }
    }
}
