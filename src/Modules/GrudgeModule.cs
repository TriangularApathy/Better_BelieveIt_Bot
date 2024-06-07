using Discord;
using Discord.Interactions;
using System.Text.Json;

namespace Better_BelieveIt_Bot.Modules {
    public class Grudge {
        public string Title { get; set; } = string.Empty;
        public string Wronged { get; set; } = string.Empty;
        public string Assailaints { get; set; } = string.Empty;
        public string Wrongdoing { get; set; } = string.Empty;
        public string Date { get; set; } = string.Empty;
        public string Terms { get; set; } = string.Empty;
        public string Status { get; set; } = string.Empty;
        public DateTime SubmitDate {  get; set; }
        public string SubmittedBy { get; set; } = string.Empty;
        public ulong GuildID { get; set; }
    }

    public class GrudgeWrapper {
        public ulong GuildID { get; set; }
        public List<Grudge> Grudges { get; set; } = null!;
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
            Grudge newGrudge = new() {
                Title = title.ToUpper(),
                Wronged = wronged,
                Assailaints = assailants,
                Wrongdoing = wrongdoing,
                Date = date,
                Terms = terms,
                Status = "Pending",
                SubmitDate = DateTime.Now,
                SubmittedBy = Context.User.Username,
                GuildID = guildID
            };

            // Check if the file exists and read data
            string filePath = "../../../src/files/grudges_pending.json";
            if (!File.Exists(filePath)) { File.Create(filePath).Close(); }
            string? jsonContents = File.ReadAllText(filePath);

            List<GrudgeWrapper> guilds;
            if (!string.IsNullOrWhiteSpace(jsonContents)) { guilds = JsonSerializer.Deserialize<List<GrudgeWrapper>>(jsonContents) ?? new List<GrudgeWrapper>(); }
            else { guilds = new List<GrudgeWrapper>(); }
            
            bool guildExists = guilds.Any(guild => guild.GuildID == guildID);
            if (guildExists) {
                GrudgeWrapper currentGuild = guilds.First(guild => guild.GuildID == guildID);
                currentGuild.Grudges.Add(newGrudge);
            }
            else {
                GrudgeWrapper newGuild = new() {
                    GuildID = guildID,
                    Grudges = new List<Grudge> { newGrudge }
                };

                guilds.Add(newGuild);
            }

            // To-Do: catch errors within commands and send user basic reply, possibly with error (only if admin/testers?)

            string grudgeData = JsonSerializer.Serialize(guilds, new JsonSerializerOptions() { WriteIndented = true });
            
            await File.WriteAllTextAsync(filePath, grudgeData);
            await RespondAsync("Thank you for submitting a Grudge. A Book-Holder will make a decision within the next century.");
        }

        /*

        [SlashCommand("list", "Lists all confirmed Grudges.")]
        public async Task ListSubcommand() {
            // List only for the server in which it was called
        }

        

        [SlashCommand("remove", "Allows a Book-Holder to remove Grudges.")]
        public async Task RemoveSubcommand() {
            // Remove only from current guild
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
