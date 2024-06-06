using Discord.Interactions;

namespace Better_BelieveIt_Bot.src.Modules {
    [Group("grudges", "Commands to manage server grudges")]
    public class GrudgeModule : InteractionModuleBase<SocketInteractionContext> {
        [SlashCommand("add", "Add a grudge to the fabled lis t of Grudges.")]
        public async Task AddSubcommand() {
            // Add to group db or separate?
            // Newly added grudge should be sent to quarantine
        }

        [SlashCommand("remove", "Allows a Book-Holder to remove Grudges.")]
        public async Task RemoveSubcommand() {
            // Remove only from current guild
        }

        [SlashCommand("list", "Lists all confirmed Grudges.")]
        public async Task ListSubcommand() {
            // List only for the server in which it was called
        }

        [SlashCommand("request", "Submit a Grudge to be reviewed.")]
        public async Task RequestSubcommand(string title, string wronged, string[] assailants, string wrongdoing, DateTime date, string terms) {

        }

        [SlashCommand("confirm", "Allows a Book-Holder to confirm requested Grudges.")]
        public async Task ConfirmSubcommand() {

        }

        [SlashCommand("settle", "Allows a Book-Holder to mark Grudges as settled.")]
        public async Task SettleSubcommand() {

        }

        // confirm
        // list
        // remove
        // request
        // settle

        // dbcontext?
        // separate guilds?
    }
}
