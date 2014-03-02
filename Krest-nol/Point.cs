using System.Linq;

namespace KrestNol
{
    public class Point
    {
        public int X { get; private set; }
        public int Y { get; private set; }
        public Point(int x, int y)
        {
            X = x;
            Y = y;
        }

        public Point(int[] value)
        {
            if (value.Count() == 2)
            {
                X = value.Last();
                Y = value.First();
            }
        }
    }
}
