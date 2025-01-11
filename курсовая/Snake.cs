using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace курсовая
{
    public class Snake : ISnake
    {
        public List<Position> Body { get; private set; }
        public Position Head => Body.First(); // Голова змеи — первый элемент тела

        private Direction currentDirection;
        private Point point;

        public Snake()
        {
            Body = new List<Position> { new Position(1, 1), new Position(1, 2), new Position(1, 3) }; // Стартовая позиция змеи
            currentDirection = Direction.Right; // Начальное направление
        }

        public Snake(Point point)
        {
            this.point = point;
        }

        public void Move(Food food)
        {
            Position newHead = Head.Move(currentDirection);
            Body.Insert(0, newHead); // Добавить новое положение головы
            if (newHead.Equals(food.Position))
            {
                Grow(); // Увеличить размер змеи
            }
            else
            {
                Body.RemoveAt(Body.Count - 1);
            }
        }

        public void Grow()
        {
            Body.Add(Body.Last()); // Дублирование последнего сегмента для увеличения
        }

        public void ChangeDirection(Direction direction)
        {
            // Не даёт змее изменить направление
            if ((currentDirection == Direction.Up && direction != Direction.Down)
                || (currentDirection == Direction.Down && direction != Direction.Up)
                || (currentDirection == Direction.Left && direction != Direction.Right)
                || (currentDirection == Direction.Right && direction != Direction.Left))
            {
                currentDirection = direction;
            }
        }

        public bool HasCollidedWithItself()
        {
            return Body.Skip(1).Any(segment => segment.Equals(Head));
        }

        public bool HasCollidedWithWall(int width, int height)
        {
            return Head.X < 0 || Head.X >= width || Head.Y < 0 || Head.Y >= height;
        }

        public void Render2(Canvas gameCanvas)
        {
            foreach (var segment in Body)
            {
                var rect = new System.Windows.Shapes.Rectangle
                {
                    Width = InitializeGame1.CellSize,
                    Height = InitializeGame1.CellSize,
                    Fill = Brushes.OliveDrab
                };
                Canvas.SetLeft(rect, segment.X * InitializeGame1.CellSize);
                Canvas.SetTop(rect, segment.Y * InitializeGame1.CellSize);
                gameCanvas.Children.Add(rect);
            }
        }
    }
}
