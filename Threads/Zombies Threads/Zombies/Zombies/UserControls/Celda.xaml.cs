using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Backend;
using System.Windows.Threading;

namespace Zombies
{
    /// <summary>
    /// Lógica de interacción para Celda.xaml
    /// </summary>
    /// 

    /* SUPER CLASE, y sí, es un usercontrol */

    partial class Celda : UserControl
    {
        protected ElementoDeMapa elemento;

        public Celda(ElementoDeMapa elemento)
        {
            InitializeComponent();

            this.elemento = elemento;
            moverCelda(elemento.Coordenadas);
        }

        public void moverCelda(Coords newCoord)
        {
            lock (elemento) // No estoy seguro si sea necesario
            {
                //Dispatcher.Invoke(DispatcherPriority.Normal, new Action(() =>
                Dispatcher.BeginInvoke(new Action(() =>
                {
                    Grid.SetColumn(this, newCoord.X);
                    Grid.SetRow(this, newCoord.Y);
                }));

                // Aquí se ejecutaria dependiendo de cual Invoke usemos.

            }
            /* Invoke vs BeginInvoke
             * Invoke: la ejecuta y espera para continuar
             * BeginInvoke: la ejecuta y sigue en el código.
             */
        }

    }

    /* CLASES HIJOS, sí, heredan de un usercontrol. */

    public class ZombieCell : Celda
    {
        public ZombieCell(Zombie z) : base(z)
        {
            z.cambioDePosicion += moverAlZombie;
        }

        void moverAlZombie(Coords obj)
        {
            moverCelda(obj);
        }
    }

    public class JugadorCell : Celda
    {
        public JugadorCell(Jugador j) : base(j)
        {
            InitializeComponent();
            j.seMueveJugador += j_seMueveJugador;
            elipse.Fill = Brushes.Red;
        }

        void j_seMueveJugador(Coords obj)
        {
            moverCelda(obj);
        }

    }
}
