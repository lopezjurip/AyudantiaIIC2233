using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logica
{
    public class NucleoInteligente // OJO, tiene que ser PUBLIC
    {
        public const bool PARTE_BLANCO = true;
        public const int DIMENSION_TABLERO = 8;

        public PiezaAjedrez[,] Matrix { get; private set; }
        public bool JuegaBlanco { get; private set; }

        public NucleoInteligente() 
        {
            Matrix = new PiezaAjedrez[DIMENSION_TABLERO, DIMENSION_TABLERO];
            JuegaBlanco = PARTE_BLANCO;
        }

        // Necesitamos un evento (action) llamado ActualizacionTablero que entregue el array "Matrix"
        #region Solución Evento
        public event Action<PiezaAjedrez[,]> ActualizacionTablero;
        public event Action<PiezaAjedrez> PiezaEliminada;
        #endregion

        public void GenerarTablero()
        {
            for (int i = 0; i < DIMENSION_TABLERO; i++)
            {
                Matrix[i, 1] = new Peon(Color.Negro, new Coordenada(i,1));
                Matrix[i, 6] = new Peon(Color.Blanco, new Coordenada(i, 6));
            }

            // Esto pudo ser mejor... 
            Matrix[0, 0] = new Torre(Color.Negro, new Coordenada(0,0));
            Matrix[1, 0] = new Caballo(Color.Negro, new Coordenada(1,0));
            Matrix[2, 0] = new Alfil(Color.Negro, new Coordenada(2,0));
            Matrix[3, 0] = new Rey(Color.Negro, new Coordenada(3,0));
            Matrix[4, 0] = new Reina(Color.Negro, new Coordenada(4,0));
            Matrix[5, 0] = new Alfil(Color.Negro, new Coordenada(5,0));
            Matrix[6, 0] = new Caballo(Color.Negro, new Coordenada(6,0));
            Matrix[7, 0] = new Torre(Color.Negro, new Coordenada(7,0));

            Matrix[0, 7] = new Torre(Color.Blanco, new Coordenada(0, 7));
            Matrix[1, 7] = new Caballo(Color.Blanco, new Coordenada(1, 7));
            Matrix[2, 7] = new Alfil(Color.Blanco, new Coordenada(2, 7));
            Matrix[3, 7] = new Rey(Color.Blanco, new Coordenada(3, 7));
            Matrix[4, 7] = new Reina(Color.Blanco, new Coordenada(4, 7));
            Matrix[5, 7] = new Alfil(Color.Blanco, new Coordenada(5, 7));
            Matrix[6, 7] = new Caballo(Color.Blanco, new Coordenada(6, 7));
            Matrix[7, 7] = new Torre(Color.Blanco, new Coordenada(7, 7));

            NotificarCambios();
        }

        private bool CoordenadaValida(Coordenada coordenada)
        {
            return (coordenada.X >= 0 || coordenada.X < DIMENSION_TABLERO || coordenada.Y >= 0 || coordenada.Y < DIMENSION_TABLERO);
        }

        public void MoverPieza(Coordenada desde, Coordenada hasta)
        {
            if (desde == null || hasta == null)
                return;

            // Verificamos que sea una coordenada válida
            if(!CoordenadaValida(desde) || !CoordenadaValida(hasta))
                return;

            PiezaAjedrez pieza = Matrix[desde.X, desde.Y];

            // Verificar que haya una pieza.
            if(pieza == null) 
                return;

            // Verificar turno válido.
            if(pieza.Color == Color.Blanco && !JuegaBlanco)
                return;
            else if(pieza.Color == Color.Negro && JuegaBlanco)
                return;

            // Verificar si el movimiento es válido.
            if (!pieza.Movimiento(hasta))
                return;

            PiezaAjedrez piezaEnemiga = Matrix[hasta.X, hasta.Y];
            if(piezaEnemiga != null)
            {
                // Está atacando una de su mismo color.
                if(piezaEnemiga.Color == pieza.Color)
                    return;

                Matrix[hasta.X, hasta.Y] = null;

                // Notificamos
                if(PiezaEliminada != null)
                    PiezaEliminada(piezaEnemiga);
            }

            // Todo bien, hacemos el movimiento.
            pieza.CoordenadaActual = hasta;
            Matrix[desde.X, desde.Y] = null;
            Matrix[hasta.X, hasta.Y] = pieza;
            JuegaBlanco = !JuegaBlanco;

            NotificarCambios();
        }

        private void NotificarCambios()
        {
            if (ActualizacionTablero != null)
                ActualizacionTablero(Matrix);
        }
    }
}
