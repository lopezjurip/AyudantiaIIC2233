using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logica
{
    public class Logic // OJO, tiene que ser PUBLIC
    {
        public Logic() { }

        PiezaAjedrez[,] Matrix = new PiezaAjedrez[8, 8];

        // Necesitamos un evento (action) llamado ActualizacionTablero que entregue el array "Matrix"

        #region Solución Evento
        public event Action<PiezaAjedrez[,]> ActualizacionTablero;
        #endregion

        bool JuegaBlanco = true;

        public void GenerarTablero()
        {
            for (int i = 0; i < 8; i++)
            {
                Matrix[i, 1] = new Peon("Negro");
                Matrix[i, 6] = new Peon("Blanco");
            }

            Matrix[0, 0] = new Torre("Negro");
            Matrix[1, 0] = new Caballo("Negro");
            Matrix[2, 0] = new Alfil("Negro");
            Matrix[3, 0] = new Rey("Negro");
            Matrix[4, 0] = new Reina("Negro");
            Matrix[5, 0] = new Alfil("Negro");
            Matrix[6, 0] = new Caballo("Negro");
            Matrix[7, 0] = new Torre("Negro");

            Matrix[0, 7] = new Torre("Blanco");
            Matrix[1, 7] = new Caballo("Blanco");
            Matrix[2, 7] = new Alfil("Blanco");
            Matrix[3, 7] = new Rey("Blanco");
            Matrix[4, 7] = new Reina("Blanco");
            Matrix[5, 7] = new Alfil("Blanco");
            Matrix[6, 7] = new Caballo("Blanco");
            Matrix[7, 7] = new Torre("Blanco");

            // Necesitamos gatillar el evento y que notifique a los suscriptores:

            #region Solución: notificacion
            #endregion
            if (ActualizacionTablero != null)
                ActualizacionTablero(Matrix);
        }

        public void MoverPieza(int Xi, int Xf, int Yi, int Yf)
        {
            if (Matrix[Xi, Yi].Movimiento(Xi,Yi,Xf,Yf)) // Mov. válido?
            {
                if (Matrix[Xf, Yf] != null)
                {
                    if (Matrix[Xf, Yf].Color.Equals(Matrix[Xi, Yi].Color))
                        return;
                }

                if (Matrix[Xi, Yi].Color.Equals("Blanco") && JuegaBlanco == false)
                    return;
                else if (Matrix[Xi, Yi].Color.Equals("Negro") && JuegaBlanco == true)
                    return;
                else
                    JuegaBlanco = !JuegaBlanco;

                Matrix[Xf, Yf] = Matrix[Xi, Yi];
                Matrix[Xi, Yi] = null;
                if (ActualizacionTablero != null)
                    ActualizacionTablero(Matrix);
            }
        }
    }
}
