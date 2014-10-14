using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logica
{
    class Rey : PiezaAjedrez
    {
        public Rey(Color color, Coordenada inicio) : base("Rey", color, inicio) { }

        public override bool Movimiento(Coordenada coordenadaNueva)
        {
            int delta_X = Math.Abs(CoordenadaActual.X - coordenadaNueva.X);
            int delta_Y = Math.Abs(CoordenadaActual.Y - coordenadaNueva.Y);
            return (delta_X <= 1 && delta_Y <= 1);
        }

        public override string Icono
        {
            get 
            { 
                return (Color == Color.Negro) ? REY_NEGRO : REY_BLANCO ;
            }
        }
    }
}
