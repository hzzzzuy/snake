using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace курсовая
{
    public class Position
    {
        public int X { get; set; }
        public int Y { get; set; }


        public Position(int x, int y)
        {
            X = x;
            Y = y;
        }


        public Position Move(Direction direction)
        {

            switch (direction)
            {
                case Direction.Up:
                    return new Position(X, Y - 1);
                case Direction.Down:
                    return new Position(X, Y + 1);
                case Direction.Left:
                    return new Position(X - 1, Y);
                case Direction.Right:
                    return new Position(X + 1, Y);
                default:
                    return this;
            }
        }

        public override bool Equals(object obj)
        {
            if (obj is Position other)
            {
                return X == other.X && Y == other.Y;
            }
            return false;
        }


        public override int GetHashCode()
        {
            return (X, Y).GetHashCode();
        }
    }
}
