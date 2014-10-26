using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Backend
{
    class Meteorito : ObjetoEspacial
    {
        private bool RespectoX;

        public override string NombreImagen
        {
            get { return "meteor.png"; }
        }

        public Meteorito(Random r, double X, double Y) : base(X, Y)
        {
            RespectoX = (r.Next(2) == 0);
            W = H = r.Next(10, 20);
            TiempoRotacion = r.Next(5000, 7000);
        }

        public override void Moverse(double cantidad)
        {
            if (RespectoX)
            {
                X -= cantidad;
                Y += Math.Pow(cantidad, 2);
            }
            else
            {
                Y -= cantidad;
                X += Math.Pow(cantidad, 2);
            }

            GatillarCambioCoordenadas(X, Y);
        }
    }
}
