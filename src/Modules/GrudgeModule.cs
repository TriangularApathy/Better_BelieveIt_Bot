using Discord.Interactions;

namespace Better_BelieveIt_Bot.src.Modules {
    [Group("grudges", "Commands to manage server grudges")]
    public class GrudgeModule : InteractionModuleBase<SocketInteractionContext> {
        [SlashCommand("request", "Submit a Grudge to be reviewed.")]
        public async Task RequestSubcommand(
            [Summary(description: "The title of the grudge ex: ({user}-{user} honor grudge)")] string title,
            [Summary(description: "The user(s) that was/were wronged.")] string wronged,
            [Summary(description: "The user(s) that committed the Wrongdoing.")] string[] assailants,
            [Summary(description: "The Wrongdoing deserving of an eternal grudge. ex: (Unrightful banning of *User* form *server*.)")] string wrongdoing,
            [Summary(description: "The date of the Wrongdoing ex: (01/01/2024)")] DateTime date,
            [Summary(description: "The action(s) that must take place in order to remove the grudge. ex: (Public apology)")] string[] terms) {

        }

        /*

        [SlashCommand("add", "Add a grudge to the fabled list of Grudges.")]
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

        */
    }
}
