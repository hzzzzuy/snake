using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Windows.Threading;
using System.Xml.Linq;

namespace курсовая
{
    /// <summary>
    /// Логика взаимодействия для InitializeGame1.xaml
    /// </summary>
    public partial class InitializeGame1 : Window
    {
        public static double CellSize = 20;
        private const int GridWidth = 40;
        private const int GridHeight = 20;
        private Snake snake;
        public Food food;
        private List<Obstacle> obstacles;
        private DispatcherTimer gameTimer;
        private int score;
        private GameState gameState;
        private Leaderboard leaderboard;
        public InitializeGame1(GameState state)
        {
            InitializeComponent();
            gameState = state;
            InitializeGame();
        }
        private void InitializeGame()
        {
            snake = new Snake();
            food = new Food((int)CellSize, (int)CellSize);
            obstacles = new List<Obstacle>();
            score = 0;
            leaderboard = new Leaderboard();
            gameTimer = new DispatcherTimer();
            gameTimer.Interval = TimeSpan.FromMilliseconds(500);
            gameTimer.Tick += GameLoop; // Подписка на событие Tick таймера
            gameTimer.Start();
            GenerateFood();
            if (gameState.CurrentMode == GameMode.Advanced)
            {
                GenerateObstacles();
            }
            this.Focus();
        }
        private void GameLoop(object sender, EventArgs e)
        {
            snake.Move(food);
            CheckCollisions();
            RenderGame();
        }
        private void CheckCollisions()
        {
            if (snake.Head.Equals(food.Position))
            {
                snake.Grow();
                score++;
                GenerateFood();
                if (gameState.CurrentMode == GameMode.Advanced)
                {
                    gameTimer.Interval = TimeSpan.FromMilliseconds(gameTimer.Interval.TotalMilliseconds - 50);
                    if (gameTimer.Interval.TotalMilliseconds < 100)
                    {
                        gameTimer.Interval = TimeSpan.FromMilliseconds(100);
                    }
                }
                
            }
            if (snake.HasCollidedWithWall(GridWidth, GridHeight) || snake.HasCollidedWithItself())
            {
                EndGame();
            }
            if (obstacles.Any(o => snake.Head.Equals(o.Position)))
            {
                EndGame();
            }
        }
        private void RenderGame()
        {

            var gameCanvas = (Canvas)this.FindName("GameCanvas");

            if (gameCanvas != null)
            {
                gameCanvas.Children.Clear();
                snake.Render2(gameCanvas);
                food.Render(gameCanvas);
                if (gameState.CurrentMode == GameMode.Advanced)
                {
                    foreach (var obstacle in obstacles)
                    {
                        obstacle.Render1(gameCanvas);
                    }
                }
            }

        }
        //Объявление делегата
        public Action<string, int> OnSaveHighScore;
        private void EndGame()
        {
            MainWindow mainwindow1 = Application.Current.Windows.OfType<MainWindow>().FirstOrDefault();
            if (mainwindow1 != null)
            {
                string playerName = mainwindow1.PlayerName;
                string gamemod = gameState.CurrentMode.ToString();
                gameTimer.Stop();
                MessageBox.Show($"Game Over! : {playerName} {score}");
                mainwindow1.SaveHighScore(playerName, score, gamemod);
                leaderboard.AddPlayer(new Player(playerName, score, gamemod));
                this.Close();
                // Вызов делегата для сохранения результата
                OnSaveHighScore?.Invoke(playerName, score);
            }
        }
        private void GenerateFood()
        {
            Random random = new Random();
            Position newFoodPosition;

            do
            {
                int x = random.Next(1, GridWidth);
                int y = random.Next(1, GridHeight);
                newFoodPosition = new Position(x, y);
            }
            while (snake.Body.Contains(newFoodPosition) ||
           obstacles.Any(o => o.Position.Equals(newFoodPosition)) || !IsWithinBounds(newFoodPosition));

            food.Position = newFoodPosition;
        }
        private bool IsWithinBounds(Position position)
        {
            return position.X >= 1 && position.X < GridWidth &&
                   position.Y >= 1 && position.Y < GridHeight;
        }
        private void GenerateObstacles()
        {
            Random random = new Random();
            for (int i = 0; i < 10; i++)
            {
                Position obstaclePosition;
                do
                {
                    obstaclePosition = new Position(random.Next(1, GridWidth), random.Next(1, GridHeight));
                } while (snake.Body.Contains(obstaclePosition) || obstaclePosition.Equals(food.Position));

                obstacles.Add(new Obstacle(obstaclePosition));
            }
        }

        protected override void OnKeyDown(KeyEventArgs e)
        {
            switch (e.Key)
            {
                case Key.Up:
                    snake.ChangeDirection(Direction.Up);
                    break;
                case Key.Down:
                    snake.ChangeDirection(Direction.Down);
                    break;
                case Key.Left:
                    snake.ChangeDirection(Direction.Left);
                    break;
                case Key.Right:
                    snake.ChangeDirection(Direction.Right);
                    break;
            }
        }
    }
}
