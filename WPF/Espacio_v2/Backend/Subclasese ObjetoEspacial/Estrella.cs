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

        public Estrella(Random generador, double X, double Y) : base(X, Y)
        {
            Constante = generador.Next(-10, 10) * 0.0001;
            Pendiente = generador.Next(-100, 100) * 0.00001;
            W = H = generador.Next(5, 30);
        }

        public override void Moverse(double cantidad)
        {
            X -= cantidad;
            Y += Pendiente * cantidad + Constante;

            GatillarCambioCoordenadas(X, Y);
        }
    }
}
