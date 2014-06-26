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
using HAL9000;
using System.Threading;

namespace Snake_Online // Al principio iba a hacer un Snake Online, pero después me dijieron que la Ayudantía iba de ser de Threads.
{                      // Y depués no me salió el Snake así que lo hice como Tron, jeje 0:)
    /// <summary>
    /// Por Patricio López Juri
    /// </summary>

    public partial class MainWindow : Window
    {

        public static Random R = new Random();
        int n = 60; // Tamaño Cuadricula
        int Velocidad = 10; // En Milisegundos
        Cell[,] Celdas;
        Tron p1; // Jugador 1
        bool isGameOver = false;
        Dictionary<Coord, Rectangle> Rectangulos = new Dictionary<Coord, Rectangle>();
        List<Rectangle> RectangulosPintados = new List<Rectangle>();
        

        public MainWindow()
        {
            InitializeComponent();
            this.KeyDown += OnButtonKeyDown; // Eso es para que se lean las teclas que presiono en la ventana principal (this)
            Celdas = new Cell[n, n];
            gameover.Visibility = Visibility.Collapsed; // Me gusta crear las cosas visuales por XAML y esconderlas al iniciar el programa.

            // Creamos la cuadrícula
            for (int i = 0; i < n; i++)
            {
                Grilla.ColumnDefinitions.Add(new ColumnDefinition());
                Grilla.RowDefinitions.Add(new RowDefinition());
            }
            // rellenamos la cuadrícula
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    Rectangle r = new Rectangle();
                    r.Fill = Brushes.Black;
                    r.StrokeThickness = 0.1;
                    r.Stroke = Brushes.Gray;
                    Coord c = new Coord(i, j);
                    Celdas[i, j] = new Cell(c);
                    Rectangulos.Add(c, r);
                    Grid.SetColumn(r, i);
                    Grid.SetRow(r, j);
                    Grilla.Children.Add(r);
                }
            }
            // Creamos la "red" de Celdas (Estructura de Datos).
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    // Uso Try/Catch porque soy flojo
                    try { Celdas[i, j].Abajo = Celdas[i, j + 1]; }
                    catch (IndexOutOfRangeException) { Celdas[i, j].Abajo = null; }
                    try { Celdas[i, j].Arriba = Celdas[i, j - 1]; }
                    catch (IndexOutOfRangeException) { Celdas[i, j].Arriba = null; }
                    try { Celdas[i, j].Derecha = Celdas[i+1, j]; }
                    catch (IndexOutOfRangeException) { Celdas[i, j].Derecha = null; }
                    try { Celdas[i, j].Izquierda = Celdas[i-1, j]; }
                    catch (IndexOutOfRangeException) { Celdas[i, j].Izquierda = null; }
                }
            }

            // Creamos al jugador
            int P1_x = R.Next(0, n);
            int P1_y = R.Next(0, n);
            p1 = new Tron(Celdas[P1_x,P1_y], false); // P1 Azul

            // Asociamos los eventos
            p1.Pintar += p1_Pintar;
            p1.Perder += p1_Perder;
            p1.CrearPelotaAqui += p1_CrearPelotaAqui;

            // Preparamos la visual y el Thread
            PintarCelda(new Coord(P1_x, P1_y), p1);
            
            Thread TimerClockThread = new Thread(TimerClock);
            TimerClockThread.IsBackground = true;
            TimerClockThread.Start(p1);

            player.Source = new Uri(@"..\..\Daft Punk - Derezzed 8bit.mp3", UriKind.Relative);
            player.Volume = 1;
            player.Play();
            p1.EmpezarAPonerPelotitas(n);
        }

        // THREAD -------------------------------------
        private void TimerClock(object obj) // Recordar que el método que utilizará el Thread, si es parametrizado, DEBE recibir un Object
        {
            Tron T = (Tron)obj;
            bool TitilarOn = false;
            while (true)
            {
                lock (T)
                {
                    Thread.Sleep(Velocidad);
                    T.Moverse.Invoke();
                    if (isGameOver) // Usaremos el mismo Thread para generar el "efecto" de titilar una vez perdido el juego.
                    {
                        titilar(TitilarOn);
                        TitilarOn = !TitilarOn;
                        Velocidad = 100;
                    }
                }
            }
        }
        // -------------------------------------------

        void p1_CrearPelotaAqui(Coord obj)
        {
            this.Dispatcher.BeginInvoke(new Action<Coord>(p1_CrearPelotaAquiDispatcher), obj);
        }
        void p1_CrearPelotaAquiDispatcher(Coord obj)
        {
            Rectangulos[obj].Fill = Brushes.Yellow;
        }

        void p1_Perder()
        {
            this.Dispatcher.BeginInvoke(new Action(p1_PerderDispatcher), null);
        }
        void p1_PerderDispatcher()
        {
            gameover.Visibility = Visibility.Visible;
            this.KeyDown -= OnButtonKeyDown;
            isGameOver = true;
        }

        void titilar(bool OnOff)
        {
            this.Dispatcher.BeginInvoke(new Action<bool>(titilarDispatcher), OnOff);
        }
        void titilarDispatcher(bool OnOff)
        {
            foreach (Rectangle r in RectangulosPintados)
            {
                if (OnOff)
                    r.Fill = Brushes.Red;
                else
                    r.Fill = Brushes.Cyan;
            }
        }

        void p1_Pintar(Coord arg1, bool arg2)
        {
            this.Dispatcher.BeginInvoke(new Action<Coord, bool>(p1_PintarDispatcher), arg1, arg2);
        }
        void p1_PintarDispatcher(Coord arg1, bool arg2)
        {
            lock (Rectangulos[arg1])
            {
                RectangulosPintados.Add(Rectangulos[arg1]);
                if (arg2)
                    Rectangulos[arg1].Fill = Brushes.Orange;
                else
                    Rectangulos[arg1].Fill = Brushes.Cyan;
            }
        }

        private void OnButtonKeyDown(object sender, KeyEventArgs e)
        {
            switch (e.Key)
            {
                case Key.Down:
                    if (p1.InvalidarAbajo)
                        return;
                    p1.Moverse = p1.Abajo;
                    break;
                case Key.Up:
                    if (p1.InvalidarArriba)
                        return;
                    p1.Moverse = p1.Arriba;
                    break;
                case Key.Left:
                    if (p1.InvalidarIzquierda)
                        return;
                    p1.Moverse = p1.Izquierda;
                    break;
                case Key.Right:
                    if (p1.InvalidarDerecha)
                        return;
                    p1.Moverse = p1.Derecha;
                    break;
            }
        }

        public void PintarCelda(Coord C, Tron T)
        {
            Rectangle r = Rectangulos[C];
            if (T.isRed)
                r.Fill = Brushes.Red;
            else
                r.Fill = Brushes.Blue;
        }

    }
}
