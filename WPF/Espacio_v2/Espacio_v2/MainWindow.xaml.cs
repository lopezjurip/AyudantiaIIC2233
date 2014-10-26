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

// Patricio López (pelopez2@uc.cl)

namespace RecursionPRO
{
    /// <summary>
    /// Lógica de interacción para MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        /// <summary>
        /// Intervalo en milisegundos
        /// </summary>
        private const int TICKS_INTERVAL = 20;

        /// <summary>
        /// En pixeles.
        /// </summary>
        private const int DIMENSION_NAVE = 50;

        /// <summary>
        /// Nosotros!
        /// </summary>
        private Nave MiNave { get; set; }

        /// <summary>
        /// Objeto espacio del backend.
        /// </summary>
        private Espacio MiEspacio { get; set; }

        

        public MainWindow()
        {
            InitializeComponent();

            // Programa aparece al centro de la pantalla
            this.WindowStartupLocation = System.Windows.WindowStartupLocation.CenterScreen;
 
            // Quiero una ventana con bordes más marcados.
            this.WindowStyle = System.Windows.WindowStyle.ThreeDBorderWindow;

            // Espacio es una clase del Backend, por aquí nos comunicaremos
            MiEspacio = new Espacio(this.Width, this.Height);
           
            // Creamos la nave, no necesita nada en el backend pues no tiene nada de lógica importante.
            MiNave = new Nave();
            MiNave.Height = MiNave.Width = DIMENSION_NAVE;
            Canvas.SetLeft(MiNave, this.Width/2);
            Canvas.SetTop(MiNave, this.Height/2);
            SpaceCanvas.Children.Add(MiNave);

            // Escondemos el mouse
            Mouse.OverrideCursor = Cursors.None;

            // Suscripcion de eventos
            SpaceCanvas.MouseMove += SpaceCanvas_MouseMove; // El mouse se mueve dentro del Canvas
            MiEspacio.NaceUnObjeto += Espacio_NaceUnObjeto;   // El backend nos avisa que nace un objetoEspacial
            this.SizeChanged += MainWindow_SizeChanged;     // Nuestra ventana (this) nos avisa que el usuario cambió su tamaño.

            // Preparamos el timer.
            DispatcherTimer t = new DispatcherTimer();
            t.Interval = new TimeSpan(0, 0, 0, 0, TICKS_INTERVAL);
            t.Tick += (s, e) =>
            {
                MiEspacio.Tickear(TICKS_INTERVAL/5);
            };
            t.IsEnabled = true;
            t.Start();

            PonerMusica();
        }

        void SpaceCanvas_MouseMove(object sender, MouseEventArgs e)
        {
            // Este método se llama muchas muchas veces por segundo cuando mueven el mouse.
            Point posicion = e.GetPosition(sender as Canvas);
            double x_nuevo = posicion.X;
            double y_nuevo = posicion.Y;

            // Movemos nuestra nave. Recordemos que Canvas también tiene métodos estáticos.
            Canvas.SetLeft(MiNave, x_nuevo);
            Canvas.SetTop(MiNave, y_nuevo);
        }

        void MainWindow_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            // Le damos al backend la nueva dimensión del mundo.
            MiEspacio.AltoEspacio = e.NewSize.Height;
            MiEspacio.LargoEspacio = e.NewSize.Width;
        }

        // El backend notifica y se llama a esto
        void Espacio_NaceUnObjeto(ObjetoEspacial obj)
        {
            CuerpoEspacial c = new CuerpoEspacial(obj);

            c.BorrarCuerpoEspacial += (CuerpoEspacial espacial) =>
            {
                SpaceCanvas.Children.Remove(espacial);
            };

            SpaceCanvas.Children.Add(c);
            c.iniciarAnimacion();
        }

        private void PonerMusica()
        {
            MediaElement mediaElement1 = new MediaElement();
            SpaceCanvas.Children.Add(mediaElement1);
            mediaElement1.Source = new Uri(@"..\..\Sonidos\starfox cornelia.mid", UriKind.Relative);
            mediaElement1.SpeedRatio = 1.1;
            mediaElement1.LoadedBehavior = MediaState.Manual;
            mediaElement1.Play();
        }

        private static double Pigatoras(double x, double y)
        {
            return Math.Sqrt(Math.Pow(x, 2) + Math.Pow(y, 2));
        }
    }
}
