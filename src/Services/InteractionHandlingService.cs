using Better_BelieveIt_Bot.Utility;
using Discord.Interactions;
using Discord.WebSocket;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System.Reflection;

namespace Better_BelieveIt_Bot.Services {
    internal class InteractionHandlingService : IHostedService {
        private readonly DiscordSocketClient _client;
        private readonly InteractionService _interaction;
        private readonly IServiceProvider _services;
        private readonly IConfiguration _config;
        private readonly ILogger<InteractionService> _logger;

        public InteractionHandlingService(
            DiscordSocketClient client,
            InteractionService interaction,
            IServiceProvider services,
            IConfiguration config,
            ILogger<InteractionService> logger) {
            _client = client;
            _interaction = interaction;
            _services = services;
            _config = config;
            _logger = logger;

            _interaction.Log += message => LogHandler.OnLogAsync(_logger, message);
        }

        public async Task StartAsync(CancellationToken cancellationToken) {
            // Commands can take up to an hour to register globally
            //_client.Ready += () => _interaction.RegisterCommandsGloballyAsync(true);
            var testGuildID = _config["TestGuildID"];
            if (!ulong.TryParse(testGuildID, out ulong guildID)) {
                Console.WriteLine("[FATAL ERROR]: Could not convert [TestGuildID] to ulong");
                throw new Exception();
            }
            _client.Ready += () => _interaction.RegisterCommandsToGuildAsync(guildID, true);
            _client.InteractionCreated += OnInteractionAsync;

            await _interaction.AddModulesAsync(Assembly.GetEntryAssembly(), _services);
        }

        public Task StopAsync(CancellationToken cancellationToken) {
            _interaction.Dispose();
            return Task.CompletedTask;
        }

        private async Task OnInteractionAsync(SocketInteraction interaction) {
            try {
                var context = new SocketInteractionContext(_client, interaction);
                var result = await _interaction.ExecuteCommandAsync(context, _services);

                if (!result.IsSuccess)
                    await context.Channel.SendMessageAsync(result.ToString());
            }

            catch {
                if (interaction.Type == Discord.InteractionType.ApplicationCommand) {
                    await interaction.GetOriginalResponseAsync()
                        .ContinueWith(message => message.Result.DeleteAsync());
                }
            }
        }
    }
}