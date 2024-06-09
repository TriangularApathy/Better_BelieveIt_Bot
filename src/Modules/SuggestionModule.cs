using Discord;
using Discord.Interactions;
using Better_BelieveIt_Bot.Utility;

namespace Better_BelieveIt_Bot.Modules {
    public class SuggestionModule : InteractionModuleBase<SocketInteractionContext> {
        public class Suggestion {
            public string Idea { get; set; } = null!;
            public string User { get; set; } = null!;
            public DateTime SubmittedDate { get; set; }
            public ulong GuildID { get; set; }

        }

        public class SuggestionWrapper {
            public ulong GuildID;
            public List<Suggestion> Suggestions { get; set; } = null!;
        }

        public class FilePathManager {
            private static readonly string filePath = "../../../src/files/suggestions.json";
            public static string FilePath => filePath;
        }

        [SlashCommand("suggestion_box", "Submit a suggestion for the server or bot.")]
        public async Task SuggestionBox() => await Context.Interaction.RespondWithModalAsync<SuggestionModal>("suggestion_box");

        public class SuggestionModal : IModal {
            public string Title => "Server Suggestion Box";
            [InputLabel("What is your suggestion?")]
            [ModalTextInput("suggestion", TextInputStyle.Paragraph, placeholder: "I just want more commands!")]
            public string Suggestion { get; set; } = null!;
        }

        [ModalInteraction("suggestion_box")]
        public async Task SuggestionModalResponse(SuggestionModal modal) {
            ulong guildID = Context.Guild.Id;
            string filePath = FilePathManager.FilePath;
            Suggestion newSuggestion = new() {
                Idea = modal.Suggestion,
                User = Context.User.GlobalName,
                SubmittedDate = DateTime.Now,
                GuildID = guildID
            };

            await FileHandler.WriteJsonFile<Suggestion>(guildID, filePath, newSuggestion);

            string message = $"Thanks {Context.User.Mention} for submitting a suggestion! An Admin will deem wether it is worthy of action.";
            await RespondAsync(message); 
        }
    }
}
