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
// Cómo se importa el Backend?
#region Solución: Importar Backend
using Logica;
#endregion

namespace Ajedrez
{
    /// <summary>
    /// Por Patricio López Juri (pelopez2@uc.cl)
    /// </summary>
    public partial class MainWindow : Window
    {
        private const int TAMANO_ICONO = 40;

        private NucleoInteligente Nucleo { get; set; }
        private PiezaAjedrez LastClick { get; set; }
        private Button[,] Tablero { get; set; }

        public MainWindow()
        {
            InitializeComponent();

            CrearTableroVisual(NucleoInteligente.DIMENSION_TABLERO);

            Nucleo = new NucleoInteligente();
            Nucleo.ActualizacionTablero += ActualizarTablero;
            Nucleo.GenerarTablero();

            MessageBox.Show((NucleoInteligente.PARTE_BLANCO) ? "Parte blanco" : "Parte negro");
        }

        private void CrearTableroVisual(int dimension)
        {
            Tablero = new Button[dimension, dimension];

            for (int i = 0; i < dimension; i++)
            {
                // Cómo creamos filas y columnas y las añadimos al Grid?
                #region Solución: Creación de Filas/colmunas
                Grilla.ColumnDefinitions.Add(new ColumnDefinition());
                Grilla.RowDefinitions.Add(new RowDefinition());
                #endregion
            }

            bool Blanco = true;
            for (int i = 0; i < dimension; i++)
            {
                for (int j = 0; j < dimension; j++)
                {
                    // Tenemos el siguiente botón:
                    Button c = new Button();

                    #region Solución: Crear cada botón
                    c.Background = (Blanco) ? Brushes.White : Brushes.Gray;
                    Blanco = !Blanco;
                    c.FontSize = TAMANO_ICONO;
                    Grid.SetColumn(c, i); 
                    Grid.SetRow(c, j);
                    Grilla.Children.Add(c);
                    Tablero[i, j] = c;
                    c.Click += CeldaClick;
                    #endregion
                }
                Blanco = !Blanco;
            }
        }

        private void ActualizarTablero(PiezaAjedrez[,] obj)
        {
            Dispatcher.BeginInvoke(new Action(() =>
            {
                // Ahora sí estamos en el thread de la interfaz.
                int dimension = Tablero.GetLength(0);
                for (int i = 0; i < dimension; i++)
                {
                    for (int j = 0; j < dimension; j++)
                    {
                        PiezaAjedrez pieza = obj[i, j];
                        if (pieza != null)
                            Tablero[i, j].Content = pieza.Icono;
                        else
                            Tablero[i, j].Content = "";
                    }
                }
            }));
        }

        private void CeldaClick(object sender, RoutedEventArgs e)
        {
            Button boton = sender as Button;
            Coordenada coord = PosicionBoton(boton);
            if (coord == null)
                return;

            PiezaAjedrez pieza = Nucleo.Matrix[coord.X, coord.Y];

            if (LastClick == null)
            {
                if (pieza == null)
                    return;
                else
                    LastClick = pieza;
            }
            else
            {
                Nucleo.MoverPieza(LastClick.CoordenadaActual, coord);
                LastClick = null;
            }
        }

        private Coordenada PosicionBoton(Button boton)
        {
            int dimension = Tablero.GetLength(0);
            for (int i = 0; i < dimension; i++)
            {
                for (int j = 0; j < dimension; j++)
                {
                    if (Tablero[i, j] == boton)
                        return new Coordenada(i, j);
                }
            }
            return null;
        }
    }
}
