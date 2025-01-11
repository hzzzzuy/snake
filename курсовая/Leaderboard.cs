using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using Newtonsoft.Json;

namespace курсовая
{
    public class Leaderboard
    {
        private List<Player> players;  // Список игроков, участвующих в таблице лидеров
        private const string FilePath = "leaderboard.json";

        public Leaderboard()
        {
            players = LoadPlayers();
        }

        // Метод для загрузки списка игроков из файла
        private List<Player> LoadPlayers()
        {
            if (File.Exists(FilePath))
            {
                var json = File.ReadAllText(FilePath);
                // Десериализация JSON в список игроков или возврат нового списка, если десериализация не удалась
                return JsonConvert.DeserializeObject<List<Player>>(json) ?? new List<Player>();
            }
            return new List<Player>();
        }
        public void UpdateHighScores(List<Player> newHighScores)
        {
            players = newHighScores;
        }

        // Метод для сохранения списка игроков в файл
        public void SavePlayers()
        {
            var json = JsonConvert.SerializeObject(players, Newtonsoft.Json.Formatting.Indented);
            File.WriteAllText(FilePath, json);
        }

        // Метод для добавления нового игрока в список
        public void AddPlayer(Player player)
        {
            players.Add(player);
            SavePlayers();
        }

        // Метод для удаления игрока по имени
        public void RemovePlayer(string name)
        {
            players.RemoveAll(p => p.Name.Equals(name, StringComparison.OrdinalIgnoreCase));
            SavePlayers();
        }

        // Метод для получения списка лучших игроков
        public List<Player> GetTopPlayers(int count)
        {
            return players.OrderByDescending(p => p.Score).Take(count).ToList();
        }
    }
}
