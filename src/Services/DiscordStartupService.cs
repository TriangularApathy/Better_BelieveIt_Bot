using Discord;
using Discord.WebSocket;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Better_BelieveIt_Bot.Utility;

namespace Better_BelieveIt_Bot.Services {
    internal class DiscordStartupService : IHostedService {
        private readonly DiscordSocketClient _client;
        private readonly IConfiguration _config;
        private readonly ILogger<DiscordSocketClient> _logger;

        public DiscordStartupService(DiscordSocketClient client, IConfiguration config, ILogger<DiscordSocketClient> logger) {
            _client = client;
            _config = config;
            _logger = logger;

            Console.WriteLine("I made it to 'DiscordStartupService'");
            Console.WriteLine($"Client: {_client}");
            Console.WriteLine($"Config: {_config}");
            Console.WriteLine($"Logger: {_logger}");

            _client.Log += message => LogHandler.OnLogAsync(_logger, message);
        }

        public async Task StartAsync(CancellationToken cancellationToken) {
            Console.WriteLine("I made it to 'StartAsync'");
            await _client.LoginAsync(TokenType.Bot, _config["BotToken"]);
            await _client.StartAsync();
        }

        public async Task StopAsync(CancellationToken cancellationToken) {
            await _client.LogoutAsync();
            await _client.StopAsync();
        }
    }
}
