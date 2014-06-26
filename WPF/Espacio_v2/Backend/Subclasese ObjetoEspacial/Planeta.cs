using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Backend
{
    public class Planeta : ObjetoEspacial
    {
        private int multiplicador;

        public Planeta(Random r)
        {
            switch (r.Next(0, 2))
            {
                case 0:
                    imagenNombre = "planeta1.png";
                    break;
                case 1:
                    imagenNombre = "planeta2.png";
                    break;
                default:
                    imagenNombre = "planeta1.png";
                    break;
            }
            multiplicador = r.Next(0, 4);
            w = h = r.Next(40, 100);
        }

        public override void Moverse(double cantidad)
        {
            cantidad /= 2;
            y += Math.Sin(cantidad) * multiplicador ;
            x += cantidad *-0.9; 
            llamarEventoCambioCoordenadas(x, y);
        }
    }
}
