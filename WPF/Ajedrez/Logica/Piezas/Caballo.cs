using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logica
{
    class Caballo : PiezaAjedrez
    {
        public Caballo(Color color, Coordenada inicio) : base("Caballo", color, inicio) { }

        public override bool Movimiento(Coordenada coordenadaNueva)
        {
            int delta_X = Math.Abs(coordenadaNueva.X - CoordenadaActual.X);
            int delta_Y = Math.Abs(coordenadaNueva.Y - CoordenadaActual.Y);
            return (delta_X * delta_X + delta_Y * delta_Y == 5); // Pitágoras
        }

        public override string Icono
        {
            get 
            { 
                return (Color == Color.Negro) ? CABALLO_BLANCO : CABALLO_BLANCO ;
            }
        }
    }
}