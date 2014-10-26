using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Backend
{
    public class Planeta : ObjetoEspacial
    {
        public enum TipoPlaneta
        {
            ConAnillo,
            Normal
        }

        private int Multiplicador { get; set; }
        public TipoPlaneta Tipo { get; private set; }

        public override string NombreImagen
        {
            get {
                switch (Tipo)
                {
                    case TipoPlaneta.ConAnillo:
                        return "planeta1.png";

                    default:
                        return "planeta2.png";
                }
            }
        }

        public Planeta(Random r, double X, double Y) : base(X, Y)
        {
            switch (r.Next(2))
            {
                case 0:
                    Tipo = TipoPlaneta.ConAnillo;
                    break;
                case 1:
                    Tipo = TipoPlaneta.Normal;
                    break;
                default:
                    Tipo = TipoPlaneta.ConAnillo;;
                    break;
            }

            TiempoRotacion = r.Next(1000, 10000);
            Multiplicador = r.Next(0, 4);
            W = H = r.Next(40, 100);
        }

        public override void Moverse(double cantidad)
        {
            cantidad /= 2;
            Y += Math.Sin(cantidad) * Multiplicador ;
            X += cantidad *-0.9; 
            GatillarCambioCoordenadas(X, Y);
        }
    }
}
