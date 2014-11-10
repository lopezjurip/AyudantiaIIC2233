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
        private const int GRID_SIZE = 25;

        private ApocalipsisZombie ApocalipsisZombie;
        private Mapa MapView; // Es un usercontrol
        private Jugador Jugador;

        public MainWindow()
        {
            InitializeComponent();
            MapView = new Mapa(GRID_SIZE);
            Grid.SetColumn(MapView, 1);
            Grid.SetRow(MapView, 3);
            GridPrincipal.Children.Add(MapView);

            ApocalipsisZombie = new ApocalipsisZombie(GRID_SIZE);
            
            AgregarJugador(ApocalipsisZombie.Player);
            ApocalipsisZombie.AparecioUnZombie += AgregarZombie;

            this.KeyDown += TeclaPresionada;
            jardcoreMode.Click += (s,e) =>
            {
                bool modoActual = ApocalipsisZombie.ModoJardcore;
                ApocalipsisZombie.ModoJardcore = !modoActual;
            };

            ApocalipsisZombie.StartApocalipsis();
        }

        void TeclaPresionada(object sender, KeyEventArgs e)
        {
            Coords nuevaPosicion = new Coords(Jugador.Coordenadas.X, Jugador.Coordenadas.Y);
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
            if(nuevaPosicion.dentroDeLasDimensiones(GRID_SIZE) == true)
                Jugador.Coordenadas = nuevaPosicion;
        }

        private void AgregarJugador(Jugador j)
        {
            this.Jugador = j;
            JugadorCell jugadorCell = new JugadorCell(j);
            MapView.Grilla.Children.Add(jugadorCell);
        }

        private void AgregarZombie(Zombie obj)
        {
            // Todo el proceso de creación de zombies está en un thread sin relación con la interfaz.
            Dispatcher.BeginInvoke(new Action(() =>
            {
                // Ahora sí estamos en el thread de la interfaz.
                ZombieCell zc = new ZombieCell(obj);
                MapView.Grilla.Children.Add(zc);
            }));
        }
    }
}
