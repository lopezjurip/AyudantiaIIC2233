using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logica
{
    class Torre:PiezaAjedrez
    {
        public Torre(Color color, Coordenada inicio) : base("Torre", color, inicio) { }

        public override bool Movimiento(Coordenada coordenadaNueva)
        {
            bool constante_X = (CoordenadaActual.X == coordenadaNueva.X);
            bool constante_Y = (CoordenadaActual.Y == coordenadaNueva.Y);

            return (constante_X && !constante_Y || !constante_X && constante_Y);
        }

        public override string Icono
        {
            get 
            { 
                return (Color == Color.Negro) ? TORRE_NEGRA : TORRE_BLANCA ;
            }
        }
    }
}
