using System.Text.Json;

namespace Better_BelieveIt_Bot.Utility {
    internal class FileHandler {
        public class GuildWrapper<T> {
            public ulong GuildID { get; set; }
            public List<T> Items { get; set; } = null!;
        }

        public static async Task WriteJsonFile<T>(ulong guildID, string filePath, T item) where T: class {
            // Check if the file exists and read data
            if (!File.Exists(filePath)) { File.Create(filePath).Close(); }
            string? jsonContents = File.ReadAllText(filePath);

            List<GuildWrapper<T>> items;
            if (!string.IsNullOrWhiteSpace(jsonContents)) { items = JsonSerializer.Deserialize<List<GuildWrapper<T>>>(jsonContents) ?? new List<GuildWrapper<T>>(); }
            else { items = new List<GuildWrapper<T>>(); }

            bool guildExists = items.Any(guild => guild.GuildID == guildID);
            if (guildExists) {
                GuildWrapper<T> currentGuild = items.First(guild => guild.GuildID == guildID);
                currentGuild.Items.Add(item);
            }
            else {
                GuildWrapper<T> newGuild = new() {
                    GuildID = guildID,
                    Items = new List<T> { item }
                };

                items.Add(newGuild);
            }

            string jsonData = JsonSerializer.Serialize(items, new JsonSerializerOptions() { WriteIndented = true });
            await File.WriteAllTextAsync(filePath, jsonData);
        }
    }
}
