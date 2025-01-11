using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows.Controls;

namespace курсовая
{
    public class Food
    {
        private int width;
        private int height;
        public Food(int width, int height)
        {
            this.width = width;
            this.height = height;
        }
        public Position Position { get; set; }
        public void Render(System.Windows.Controls.Canvas GameCanvas)
        {
            var rect = new System.Windows.Shapes.Rectangle
            {
                Width = InitializeGame1.CellSize,
                Height = InitializeGame1.CellSize,
                Fill = Brushes.DarkSalmon
            };
            Canvas.SetLeft(rect, Position.X * InitializeGame1.CellSize);
            Canvas.SetTop(rect, Position.Y * InitializeGame1.CellSize);
            GameCanvas.Children.Add(rect);
        }
    }
}
