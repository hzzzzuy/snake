using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media;

namespace курсовая
{
    public class Obstacle
    {
        public Position Position { get; private set; }
        public Obstacle(Position position)
        {
            Position = position;
        }
        // Метод для отрисовки препятствия на заданном холсте (canvas)
        public void Render1(System.Windows.Controls.Canvas canvas)
        {
            var rect = new System.Windows.Shapes.Rectangle
            {
                Width = InitializeGame1.CellSize,
                Height = InitializeGame1.CellSize,
                Fill = Brushes.DarkSlateGray
            };
            Canvas.SetLeft(rect, Position.X * InitializeGame1.CellSize);
            Canvas.SetTop(rect, Position.Y * InitializeGame1.CellSize);
            canvas.Children.Add(rect);
        }

    }
}
