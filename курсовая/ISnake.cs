using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace курсовая
{
    public interface ISnake
    {
        List<Position> Body { get; }
        Position Head { get; }
        void Move(Food food);
        void Grow();
        void ChangeDirection(Direction direction);
        bool HasCollidedWithItself();
        bool HasCollidedWithWall(int width, int height);
        void Render2(Canvas gameCanvas);
    }
}
