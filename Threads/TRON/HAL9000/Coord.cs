using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HAL9000
{
    public struct Coord // Ojo, es una STRUCT
    {
        public int X, Y;

        public Coord(int x, int y)
        {
            X = x;
            Y = y;

        }
    }
}
