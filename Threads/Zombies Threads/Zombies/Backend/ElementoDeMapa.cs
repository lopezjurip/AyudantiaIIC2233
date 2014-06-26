using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Backend
{
    public abstract class ElementoDeMapa
    {
        protected Coords coordenadas;
        public Coords Coordenadas
        {
            get { return coordenadas; }
        }

        public ElementoDeMapa(Coords c_inicial)
        {
            coordenadas = c_inicial;
        }
    }

    
    public class Coords
    {
        private int x, y;

        public Coords(int x, int y)
        {
            this.x = x;
            this.y = y;
        }

        // Vemos que no nos salgamos de la grilla.
        public bool dentroDeLasDimensiones(int dimension)
        {
            if (x < 0 || x >= dimension)
                return false;
            if (y < 0 || y >= dimension)
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

            bool EqualsX = (this.x == item.x);
            bool EqualsY = (this.y == item.y);
            return (EqualsX && EqualsY);
        }

        public int X
        {
            get { return x; }
            set { x = value; }
        }

        public int Y
        {
            get { return y; }
            set { y = value; }
        }
    }
}
