using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Backend
{
    public abstract class ElementoDeMapa
    {
        public Coords Coordenadas { get; protected set; }

        public ElementoDeMapa(Coords coordenadaInicial)
        {
            Coordenadas = coordenadaInicial;
        }
    }
}
