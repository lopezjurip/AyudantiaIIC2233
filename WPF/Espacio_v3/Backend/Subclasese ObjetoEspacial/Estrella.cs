using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Backend
{
    class Estrella : ObjetoEspacial
    {
        private double Constante { get; set; }
        private double Pendiente { get; set; }

        public override string NombreImagen
        {
            get { return "star.png"; }
        }

        public Estrella(Random r, double X, double Y) : base(X, Y)
        {
            Constante = r.Next(-10, 10) * 0.0001;
            Pendiente = r.Next(-100, 100) * 0.00001;
            W = H = r.Next(5, 30);
            TiempoRotacion = r.Next(2500, 5000);
        }

        public override void Moverse(double cantidad)
        {
            X -= cantidad;
            Y += Pendiente * cantidad + Constante;

            GatillarCambioCoordenadas(X, Y);
        }
    }
}
