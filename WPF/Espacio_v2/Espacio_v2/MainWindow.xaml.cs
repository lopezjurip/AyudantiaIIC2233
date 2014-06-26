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

namespace RecursionPRO
{
    /// <summary>
    /// Lógica de interacción para MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        // Nosotros
        private Nave nave;

        // Referencia al backend.
        private Espacio espacio;

        // otros
        private double x_anterior, y_anterior;
        private const int dimensionNave = 50;

        public MainWindow()
        {
            InitializeComponent();
            this.WindowStartupLocation = System.Windows.WindowStartupLocation.CenterScreen; // Programa aparece al centro de la pantalla
            this.WindowStyle = System.Windows.WindowStyle.ThreeDBorderWindow; // Quiero una ventana con bordes más marcados.

            // Espacio es una clase del Backend, por aquí nos comunicaremos
            espacio = new Espacio(this.Width, this.Height);
           
            // Creamos la nave
            nave = new Nave();
            nave.Height = nave.Width = dimensionNave;
            Canvas.SetLeft(nave, 20);
            Canvas.SetTop(nave, 20);
            SpaceCanvas.Children.Add(nave);

            // Escondemos el mouse
            Mouse.OverrideCursor = Cursors.None;

            /* Suscripcion de eventos */
            SpaceCanvas.MouseMove += SpaceCanvas_MouseMove; // El mouse se mueve dentro del Canvas
            espacio.naceUnObjeto += espacio_naceUnObjeto;   // El backend nos avisa que nace un objetoEspacial
            this.SizeChanged += MainWindow_SizeChanged;     // Nuestro ventana (this) nos avisa que el usuario cambió su tamaño.

            ponerMusica();
        }

        void SpaceCanvas_MouseMove(object sender, MouseEventArgs e)
        {
            // Este método se llama muchas muchas veces por segundo cuando mueven el mouse.
            Point posicion = e.GetPosition(sender as Canvas);
            double x_nuevo = posicion.X;
            double y_nuevo = posicion.Y;
            double distancia = Pigatoras(x_nuevo - x_anterior, y_nuevo - y_anterior);

            // Movemos nuestra nave. Recordemos que Canvas también tiene cosas estáticas.
            Canvas.SetLeft(nave, x_nuevo);
            Canvas.SetTop(nave, y_nuevo);

            // Le entregamos al backend lo que avanzamos.
            espacio.entregarPixelesViajados(distancia);

            // etc...
            x_anterior = x_nuevo;
            y_anterior = y_nuevo;
        }

        void MainWindow_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            // Le damos al backend la nueva dimensión del mundo.
            double nuevoH = e.NewSize.Height;
            double nuevoW = e.NewSize.Width;
            espacio.cambiarTamano(nuevoW, nuevoH);
        }

        // El backend notifica y se llama a esto
        void espacio_naceUnObjeto(ObjetoEspacial obj)
        {
            CuerpoEspacial c = new CuerpoEspacial(obj);
            c.BorrarCuerpoEspacial += c_BorrarCuerpoEspacial;
            SpaceCanvas.Children.Add(c);
            c.iniciarAnimacion();
        }

        // El cuerpo notifica que morirá y pasa esto
        void c_BorrarCuerpoEspacial(CuerpoEspacial obj)
        {
            SpaceCanvas.Children.Remove(obj);
        }

        private void ponerMusica()
        {
            MediaElement mediaElement1 = new MediaElement();
            SpaceCanvas.Children.Add(mediaElement1);
            mediaElement1.Source = new Uri(@"..\..\Sonidos\starfox cornelia.mid", UriKind.Relative);
            mediaElement1.SpeedRatio = 1.0;
            mediaElement1.LoadedBehavior = MediaState.Manual;
            mediaElement1.Play();
        }

        private double Pigatoras(double x, double y)
        {
            return Math.Sqrt(Math.Pow(x, 2) + Math.Pow(y, 2));
        }
    }
}
