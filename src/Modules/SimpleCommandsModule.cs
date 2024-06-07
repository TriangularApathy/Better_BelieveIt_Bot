using Discord.Interactions;

namespace Better_BelieveIt_Bot.Modules {
    public class SimpleCommandsModule : InteractionModuleBase<SocketInteractionContext> {

        [SlashCommand("ping", "Replies with 'Pong'")]
        public async Task Ping() {
            await RespondAsync("Pong!", ephemeral: true);
        }

        [SlashCommand("math_add", "Add two integers together.")]
        public async Task MathAdd(
            [Summary(description: "The first number to add")] decimal a,
            [Summary(description: "The second number to add")] decimal b) {
            decimal c = a + b;
            await RespondAsync($"The sum of [{a}] and [{b}] is [{c}]");
        }
    }
}