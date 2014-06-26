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

/* 
 * Patricio López 
 * pelopez2@uc.cl
 */

namespace Zombies
{
    /// <summary>
    /// Lógica de interacción para MainWindow.xaml
    /// </summary> 

    public partial class MainWindow : Window
    {
        private const int tamanoGrid = 25;

        ApocalipsisZombie apocalipsisZombie;

        Mapa mapView;
        Jugador jugador;

        public MainWindow()
        {
            InitializeComponent();
            mapView = new Mapa(tamanoGrid);
            Grid.SetColumn(mapView, 1);
            Grid.SetRow(mapView, 3);
            gridPrincipal.Children.Add(mapView);

            // Usar programa con el error de concurrencia arreglado
            apocalipsisZombie = new ApocalipsisZombieSolucion(tamanoGrid);

            // Usar el programa con potencial problema de concurrencia en el foreach
            //apocalipsisZombie = new ApocalipsisZombie(tamanoGrid);
            
            agregarJugador(apocalipsisZombie.jugadorActual);
            apocalipsisZombie.aparecioUnZombie += aparecioUnZombie;

            this.KeyDown += teclaPresionada;
            jardcoreMode.Click += jardcoreMode_Click;

            apocalipsisZombie.StartApocalipsis();
        }

        void teclaPresionada(object sender, KeyEventArgs e)
        {
            Coords nuevaPosicion = new Coords(jugador.Coordenadas.X, jugador.Coordenadas.Y);
            switch(e.Key)
            {
                case Key.Down:
                    nuevaPosicion.Y += 1;
                    break;

                case Key.Up:
                    nuevaPosicion.Y -= 1;
                    break;

                case Key.Left:
                    nuevaPosicion.X -= 1;
                    break;

                case Key.Right:
                    nuevaPosicion.X += 1;
                    break;
            }
            if(nuevaPosicion.dentroDeLasDimensiones(tamanoGrid) == true)
                jugador.Coordenadas = nuevaPosicion;
        }

        void agregarJugador(Jugador j)
        {
            this.jugador = j;
            JugadorCell jugadorCell = new JugadorCell(j);
            mapView.Grilla.Children.Add(jugadorCell);
        }

        void aparecioUnZombie(Zombie obj)
        {
            /* Tenemos que hacer un lock, puesto que tenemos que asegurarnos de que la operación sea atómica. */
            lock (obj)
            {
                // Todo el proceso de creación de zombies está en un thread sin relación con la interfaz.
                Dispatcher.BeginInvoke(new Action(() =>
                {
                    // Ahora sí estamos en el thread de la interfaz.
                    ZombieCell zc = new ZombieCell(obj);
                    mapView.Grilla.Children.Add(zc);
                }));
            }
        }

        void jardcoreMode_Click(object sender, RoutedEventArgs e)
        {
            bool modoActual = apocalipsisZombie.ModoJardcore;
            apocalipsisZombie.ModoJardcore = !modoActual;
        }
    }
}
