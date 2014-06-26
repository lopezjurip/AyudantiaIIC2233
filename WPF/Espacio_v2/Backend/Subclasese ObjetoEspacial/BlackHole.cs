using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Backend
{
    class BlackHole : ObjetoEspacial
    {
        private int multiplicador;

        public BlackHole(Random r)
        {
            imagenNombre = "blackhole.png";
            multiplicador = r.Next(0, 100);
            w = h = r.Next(100, 120);
        }

        public override void Moverse(double cantidad)
        {
            // hace nada.
        }
    }
}
