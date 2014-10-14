using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logica
{
    class Peon: PiezaAjedrez
    {
        public Peon(Color color, Coordenada inicio) : base("Peón", color, inicio) { }

        public override bool Movimiento(Coordenada coordenadaNueva)
        {
            if (Math.Abs(CoordenadaActual.X - coordenadaNueva.X) <= 1)
            {
                if (Color == Color.Blanco && CoordenadaActual.Y - coordenadaNueva.Y == 1)
                    return true;
                else if (Color == Color.Negro && coordenadaNueva.Y - CoordenadaActual.Y == 1)
                    return true;
            }
            return false;
        }

        public override string Icono
        {
            get 
            { 
                return (Color == Color.Negro) ? PEON_NEGRO : PEON_BLANCO ;
            }
        }
    }
}