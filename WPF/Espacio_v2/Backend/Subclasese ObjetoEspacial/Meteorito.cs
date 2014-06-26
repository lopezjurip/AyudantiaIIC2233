using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Backend
{
    class Meteorito : ObjetoEspacial
    {
        private double constante;
        private bool respectoX;

        public Meteorito(Random r)
        {
            imagenNombre = "meteor.png";
            constante = r.NextDouble() +1;
            respectoX = (r.Next(0, 2) == 0);

            w = h = r.Next(10, 20);
        }

        public override void Moverse(double cantidad)
        {
            if (respectoX)
            {
                x -= cantidad;
                y += Math.Pow(cantidad, 2);
            }
            else
            {
                y -= cantidad;
                x += Math.Pow(cantidad, 2);
            }

            llamarEventoCambioCoordenadas(x, y);
        }
    }
}
