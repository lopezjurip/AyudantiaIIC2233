using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Backend
{
    public class Coords
    {
        public int X { get; set; }
        public int Y { get; set; }

        public Coords(int x, int y)
        {
            this.X = x;
            this.Y = y;
        }

        // Vemos que no nos salgamos de la grilla.
        public bool dentroDeLasDimensiones(int dimension)
        {
            if (X < 0 || X >= dimension)
                return false;
            if (Y < 0 || Y >= dimension)
                return false;

            return true;
        }

        public override bool Equals(object obj)
        {
            var item = obj as Coords;

            if (item == null)
            {
                return false;
            }

            bool EqualsX = (this.X == item.X);
            bool EqualsY = (this.Y == item.Y);
            return (EqualsX && EqualsY);
        }
    }
}
