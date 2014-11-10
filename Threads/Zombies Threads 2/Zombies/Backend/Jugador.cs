using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Backend
{
    public class Jugador : ElementoDeMapa
    {
        // Evento que se comunica con el UserControl que lo contendrá
        public event Action<Coords> SeMueveJugador;

        public Jugador(Coords c_iniciales)
            : base(c_iniciales) { }

        public new Coords Coordenadas
        {
            get { return base.Coordenadas; }
            set
            {
                base.Coordenadas = value;
                if (SeMueveJugador != null)
                    SeMueveJugador(value);
            }
        }
    }
}
