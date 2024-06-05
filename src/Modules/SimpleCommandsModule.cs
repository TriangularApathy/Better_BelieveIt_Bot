using Discord.Interactions;

namespace Better_BelieveIt_Bot.Modules {
    public class SimpleCommandsModule : InteractionModuleBase<SocketInteractionContext> {

        [SlashCommand("ping", "Replies with 'Pong'")]
        public async Task Ping() {
            await RespondAsync("Pong!", ephemeral: true);
        }

        [SlashCommand("math_add", "Add two integers together.")]
        public async Task MathAdd(int a, int b) {
            long c = a + b;
            await RespondAsync($"The sum of [{a}] and [{b}] is [{c}]");
        }
    }
}