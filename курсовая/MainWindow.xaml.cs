using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Threading;
using System.Windows.Shapes;
using System.Windows.Controls;
using System.Windows.Media.Media3D;
using static System.Net.Mime.MediaTypeNames;
using Newtonsoft.Json;

namespace курсовая
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public enum GameMode
    {
        Classic,
        Advanced
    }
    public enum Direction
    {
        Up,
        Down,
        Left,
        Right
    }
    public partial class MainWindow : Window
    {
        public Leaderboard leaderboard;
        public string PlayerName => NamePlayer.Text;
        private GameState gameState; 
        public MainWindow()
        {
            InitializeComponent();
            leaderboard = new Leaderboard();
            gameState = new GameState();
        }
        public void SaveHighScore(string name, int score, string gamemod)
        {
            var highScores = LoadHighScores();
            var existingPlayer = highScores.FirstOrDefault(p => p.Name.Equals(name, StringComparison.OrdinalIgnoreCase));
            if (existingPlayer != null)
            {
                if (score > existingPlayer.Score)
                {
                    existingPlayer.Score = score;
                }
            }
            else
            {
                highScores.Add(new Player(name, score, gamemod));
            }
            File.WriteAllText("leaderboard.json", JsonConvert.SerializeObject(highScores));
            leaderboard.UpdateHighScores(highScores);
        }
        private List<Player> LoadHighScores()
        {
            if (File.Exists("leaderboard.json"))
            {
                var json = File.ReadAllText("leaderboard.json");
                return JsonConvert.DeserializeObject<List<Player>>(json) ?? new List<Player>();
            }
            return new List<Player>();
        }
        private void AddPlayerButton_Click(object sender, RoutedEventArgs e)
        {
            string playerName = NamePlayer.Text;
            string gameModeString = gameState.CurrentMode.ToString();
            if (!string.IsNullOrEmpty(playerName))
            {
                leaderboard.AddPlayer(new Player(playerName, 0, gameModeString));
                OutputTextBox.AppendText($"Игрок '{playerName}' добавлен!\n");
            }
        }

        private void RemovePlayerButton_Click(object sender, RoutedEventArgs e)
        {
            string playerName = Prompt.ShowDialog("Введите имя игрока для удаления:", "Удалить игрока");
            if (!string.IsNullOrEmpty(playerName))
            {
                leaderboard.RemovePlayer(playerName);
                OutputTextBox.AppendText($"Игрок '{playerName}' удален!\n");
            }
        }

        private void ShowLeaderboardButton_Click(object sender, RoutedEventArgs e)
        {
            var topPlayers = leaderboard.GetTopPlayers(10);
            OutputTextBox.Clear();
            foreach (var player in topPlayers)
            {
                OutputTextBox.AppendText($"{player.GameMod} {player.Name}: {player.Score}\n");
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            gameState.CurrentMode = GameMode.Classic;
            InitializeGame1 aa = new InitializeGame1(gameState);
            aa.Show();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            gameState.CurrentMode = GameMode.Advanced;
            var gameWindow = new InitializeGame1(gameState);
            gameWindow.Show();
        }
    }
}
