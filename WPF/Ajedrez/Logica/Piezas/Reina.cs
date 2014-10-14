using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logica
{
    class Reina: PiezaAjedrez
    {
        public Reina(Color color, Coordenada inicio) : base("Reina", color, inicio) { }

        public override bool Movimiento(Coordenada coordenadaNueva)
        {
            if (CoordenadaActual.X == coordenadaNueva.X)
                return true;
            else if (CoordenadaActual.Y == coordenadaNueva.Y)
                return true;
            else if (Math.Abs(coordenadaNueva.X - CoordenadaActual.X) == Math.Abs(coordenadaNueva.Y - CoordenadaActual.Y))
                return true;
            else
                return false;
        }

        public override string Icono
        {
            get 
            { 
                return (Color == Color.Negro) ? REINA_NEGRA : REINA_BLANCA ;
            }
        }
    }
}
