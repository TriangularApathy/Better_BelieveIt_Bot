using Better_BelieveIt_Bot.Utility;
using Discord;
using Discord.Interactions;
using System.Text.Json;
using static Better_BelieveIt_Bot.Modules.SuggestionModule;

namespace Better_BelieveIt_Bot.Modules {
    public class Grudge {
        public string Title { get; set; } = string.Empty;
        public string Wronged { get; set; } = string.Empty;
        public string Assailaints { get; set; } = string.Empty;
        public string Wrongdoing { get; set; } = string.Empty;
        public string Date { get; set; } = string.Empty;
        public string Terms { get; set; } = string.Empty;
        public string Status { get; set; } = string.Empty;
        public DateTime SubmitDate { get; set; }
        public string SubmittedBy { get; set; } = string.Empty;
        public ulong GuildID { get; set; }
    }

    public class GrudgeWrapper {
        public ulong GuildID { get; set; }
        public List<Grudge> Grudges { get; set; } = null!;
    }

    public class FilePathManager {
        private static readonly string filePath = "../../../src/files/grudges_pending.json";
        public static string FilePath => filePath;
    }

    [Group("grudges", "Commands to manage server grudges")]
    public class GrudgeModule : InteractionModuleBase<SocketInteractionContext> {
        [SlashCommand("request", "Submit a Grudge to be reviewed.")]
        public async Task RequestSubcommand(
            [Summary(description: "The title of the grudge ex: ({user}-{user} honor grudge)")] string title,
            [Summary(description: "The user(s) that was/were wronged.")] string wronged,
            [Summary(description: "The user(s) that committed the Wrongdoing.")] string assailants,
            [Summary(description: "The Wrongdoing deserving of an eternal grudge. ex: (Unrightful banning of *User* form *server*.)")] string wrongdoing,
            [Summary(description: "The date of the Wrongdoing ex: (01/01/2024)")] string date,
            [Summary(description: "The action(s) that must take place in order to remove the grudge. ex: (Public apology)")] string terms) {

            ulong guildID = Context.Guild.Id;
            string filePath = FilePathManager.FilePath;
            string submittedBy = Context.User.GlobalName;
            Grudge newGrudge = new() {
                Title = title.ToUpper(),
                Wronged = wronged,
                Assailaints = assailants,
                Wrongdoing = wrongdoing,
                Date = date,
                Terms = terms,
                Status = "Pending",
                SubmitDate = DateTime.Now,
                SubmittedBy = submittedBy,
                GuildID = guildID
            };

            await FileHandler.WriteJsonFile<Grudge>(guildID, filePath, newGrudge);
            await RespondAsync($"Thanks {Context.User.Mention} for submitting a Grudge. A Book-Holder will make a decision within the next century.");
        }

        [SlashCommand("list", "Lists all confirmed Grudges.")]
        public async Task ListSubcommand() {
            string filePath = FilePathManager.FilePath;
            string guildName = Context.Guild.Name;
            string user = Context.User.GlobalName;
            ulong guildID = Context.Guild.Id;
            if (File.Exists(filePath)) {
                string? jsonContents = File.ReadAllText(filePath);
                List<GrudgeWrapper> guilds;
                if (!string.IsNullOrWhiteSpace(jsonContents)) {
                    var embed = new EmbedBuilder() {
                        Color = 0x00AA00,
                        Title = "DA DAMMAZ KRON",
                        Author = new EmbedAuthorBuilder()
                            .WithName("Thorgrim Grudgebearer")
                            .WithIconUrl("https://lh4.googleusercontent.com/pNDr1pZZEPRNAIxpc71rlKdAN6pWg2mguNuGZ5AyTHRSdluDis261lYU1b5ghrmH82h6o-MEHF9GkxmhKLV78vsFCunXnsl9nqT5m3BEnbg50zJyU6xL7a76hdxZqiJxcACHYqcL"),
                        Description = $"*Those who have committed atrocities in {guildName} will have their name written into the Book of Grudges, and will not have it crossed out until the terms of settlement have been reached.*",
                        ThumbnailUrl = "https://lh6.googleusercontent.com/h9qwm2mS_1gdTbmsMqpaY0NlgPLB-DukTqiJIqLyhDtYkMqmOlkibWij0zRyylEwxdfOKCZnVcf0WVlY5fpRVq0b0GwvtsVXe3uxB0OR39r3mCOG9hR1Zek9MECAGk4bUVrNwyGS"                       
                    };

                    var footer = new EmbedFooterBuilder()
                        .WithText("Justice will be done")
                        .WithIconUrl("https://lh4.googleusercontent.com/pNDr1pZZEPRNAIxpc71rlKdAN6pWg2mguNuGZ5AyTHRSdluDis261lYU1b5ghrmH82h6o-MEHF9GkxmhKLV78vsFCunXnsl9nqT5m3BEnbg50zJyU6xL7a76hdxZqiJxcACHYqcL");
                    embed.Footer = footer;

                    // Set up next section
                    var field = new EmbedFieldBuilder()
                        .WithName("LIST OF GRUDGES")
                        .WithValue("\u200B");
                    embed.AddField(field);

                    guilds = JsonSerializer.Deserialize<List<GrudgeWrapper>>(jsonContents) ?? new List<GrudgeWrapper>();
                    GrudgeWrapper currentGuild = guilds.First(guild => guild.GuildID == guildID);
                    foreach (Grudge grudge in currentGuild.Grudges) {
                        string formatting = "";
                        if (grudge.Status == "Settled") { formatting = "~~"; }
                        else { formatting = "__"; }

                        string tempMessage = $"* {formatting}Wronged: {grudge.Wronged}{formatting}\n* {formatting}Assailant(s): {grudge.Assailaints}{formatting}\n* {formatting}Wrongdoing: {grudge.Wrongdoing}{formatting}\n* {formatting}Date of Wrongdoing: {grudge.Date}{formatting}\n* {formatting}Terms of Settlement: {grudge.Terms}{formatting}\n* STATUS: **{grudge.Status}**";
                        var newField = new EmbedFieldBuilder()
                            .WithName(grudge.Title)
                            .WithValue(tempMessage);
                        embed.AddField(newField);
                    }

                    await RespondAsync($"Hear ye! Hear ye!\n\n **{user}** has called for the **`Almighty List of Grudges`** for the **`{guildName}`**. Behold!\n", embed: embed.Build());
                }
                else { await RespondAsync($"For better or worse, no Grudges exist in {guildName}."); }
            }
            else { await RespondAsync($"For better or worse, no Grudges exist in {guildName}."); }
        }

        /*
          
        [SlashCommand("confirm", "Allows a Book-Holder to confirm requested Grudges.")]
        public async Task ConfirmSubcommand() {

        }

        [SlashCommand("remove", "Allows a Book-Holder to remove Grudges.")]
        public async Task RemoveSubcommand() {
            // Remove only from current guild
        }
                
        [SlashCommand("settle", "Allows a Book-Holder to mark Grudges as settled.")]
        public async Task SettleSubcommand() {

        }

        // confirm
        // remove
        // settle

        // dbcontext?
        // separate guilds?

        */
    }
}
