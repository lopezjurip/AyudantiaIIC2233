using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logica
{
    class Alfil : PiezaAjedrez
    {
        public Alfil(Color color, Coordenada inicio) : base("Alfil", color, inicio) { }

        public override bool Movimiento(Coordenada coordenadaNueva)
        {
            return (Math.Abs(coordenadaNueva.X - CoordenadaActual.X) == Math.Abs(coordenadaNueva.Y - CoordenadaActual.Y));
        }

        public override string Icono
        {
            get
            {
                return (Color == Color.Negro) ? ALFIL_NEGRO : ALFIL_BLANCO;
            }
        }
    }
}