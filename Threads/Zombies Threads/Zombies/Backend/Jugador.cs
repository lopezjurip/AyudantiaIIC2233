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
        public event Action<Coords> seMueveJugador;

        private int Vida;

        public bool estaVivo
        {
            get { return (Vida > 0); }
        }

        public new Coords Coordenadas
        {
            get { return coordenadas; }
            set
            {
                coordenadas = value;
                if (seMueveJugador != null)
                    seMueveJugador(value);
            }
        }

        public Jugador(Coords c_iniciales, int Vida)
            : base(c_iniciales)
        {
            this.Vida = Vida;
        }

    }
}
