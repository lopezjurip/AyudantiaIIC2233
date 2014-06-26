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
using Logica;
#region Solución: Importar Backend
//using Logica;
#endregion

namespace Ajedrez
{
    /// <summary>
    /// Por Patricio López Juri
    /// </summary>
    public partial class MainWindow : Window
    {
        Logic CPU = new Logic();
        Button LastClick = null;
        Button[,] Tabla = new Button[8, 8];

        public MainWindow()
        {
            InitializeComponent();
            CrearTableroVisual();
            CPU = new Logic();
            CPU.ActualizacionTablero += CPU_ActualizacionTablero;
            CPU.GenerarTablero();
        }

        private void CrearTableroVisual()
        {
            for (int i = 0; i < 8; i++)
            {
                // Cómo creamos filas y columnas y las añadimos al Grid?

                #region Solución: Creación de Filas/colmunas
                Tablero.ColumnDefinitions.Add(new ColumnDefinition());
                Tablero.RowDefinitions.Add(new RowDefinition());
                #endregion
            }

            bool Blanco = true;
            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    // Tenemos el siguiente botón:
                    Button c = new Button();

                    #region Solución: Crear cada botón
                    if (Blanco)
                        c.Background = Brushes.White;
                    else
                        c.Background = Brushes.Gray;
                    Blanco = !Blanco;
                    c.FontSize = 40;
                    Grid.SetColumn(c, i); Grid.SetRow(c, j);
                    Tablero.Children.Add(c);
                    Tabla[i, j] = c;
                    c.Click += c_Click;
                    #endregion
                }
                Blanco = !Blanco;
            }
        }

        private void CPU_ActualizacionTablero(PiezaAjedrez[,] obj)
        {
            // Cómo llamamos al "CPU_ActualizacionTablero_Dispatcher"?

            #region Solucion Dispatcher:
            this.Dispatcher.BeginInvoke(new Action<PiezaAjedrez[,]>(CPU_ActualizacionTablero_Dispatcher), obj);
            #endregion
        }

        void CPU_ActualizacionTablero_Dispatcher(PiezaAjedrez[,] obj)
        {
            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    if (obj[i, j] != null)
                        UpdateCell(obj[i, j].Tipo, obj[i, j].Color, Tabla[i, j]);
                    else
                        Tabla[i, j].Content = "";
                }
            }
        }

        void UpdateCell(String t, String c, Button b)
        {
            if (c.Equals("Negro"))
            {
                if (t.Equals("Reina")) { b.Content = "♛"; }
                if (t.Equals("Caballo")) { b.Content = "♞"; }
                if (t.Equals("Rey")) { b.Content = "♚"; }
                if (t.Equals("Peon")) { b.Content = "♟"; }
                if (t.Equals("Torre")) { b.Content = "♜"; }
                if (t.Equals("Alfil")) { b.Content = "♝"; }
            }
            else if (c.Equals("Blanco"))
            {
                if (t.Equals("Reina")) { b.Content = "♕"; }
                if (t.Equals("Caballo")) { b.Content = "♘"; }
                if (t.Equals("Rey")) { b.Content = "♔"; }
                if (t.Equals("Peon")) { b.Content = "♙"; }
                if (t.Equals("Torre")) { b.Content = "♖"; }
                if (t.Equals("Alfil")) { b.Content = "♗"; }
            }
        }

        void c_Click(object sender, RoutedEventArgs e)
        {
            Button B = (Button)sender;
            if (LastClick == null)
            {
                if (B.Content.Equals(""))
                    return;
                LastClick = B;
            }
            else
            {
                int Xi, Xf, Yi, Yf;
                Xi = Xf = Yi = Yf = -1;
                for (int i = 0; i < 8; i++)
                {
                    for (int j = 0; j < 8; j++)
                    {
                        if (LastClick.Equals(Tabla[i, j])) { Xi = i; Yi = j; }
                        if (B.Equals(Tabla[i, j])) { Xf = i; Yf = j; }
                    }
                }
                CPU.MoverPieza(Xi, Xf, Yi, Yf);
                LastClick = null;
            }
        }
    }
}
