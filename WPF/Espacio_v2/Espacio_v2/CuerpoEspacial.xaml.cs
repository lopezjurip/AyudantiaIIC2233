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
using System.Windows.Media.Animation;

namespace RecursionPRO
{
    /// <summary>
    /// Lógica de interacción para CuerpoEspacial.xaml
    /// </summary>
    public partial class CuerpoEspacial : UserControl
    {
        // Evento que notifica al main.
        public event Action<CuerpoEspacial> BorrarCuerpoEspacial;

        public CuerpoEspacial(ObjetoEspacial esp)
        {
            InitializeComponent();
            Canvas.SetLeft(this, esp.X);
            Canvas.SetTop(this, esp.Y);
            this.Height = esp.H;
            this.Width = esp.W;

            String imagenName = esp.ImagenNombre;

            if (imagenName != null)
            {
                imagenCool.Stretch = Stretch.Uniform;
                String pathImagen = @"/Espacio_v2;component/Imagenes/" + imagenName;
                BitmapImage myBitmapImage = new BitmapImage();
                myBitmapImage.BeginInit();
                myBitmapImage.UriSource = new Uri(pathImagen, UriKind.Relative);
                myBitmapImage.EndInit();
                imagenCool.Source = myBitmapImage;
            }

            // Subscripcion de eventos.
            esp.cambioDeCoordenadas += esp_cambioDeCoordenadas;
            esp.seraBorrado += esp_seraBorrado;
        }

        public void iniciarAnimacion()
        {
            imagenCool.Stretch = Stretch.Uniform;
            imagenCool.RenderTransform = new RotateTransform();

            Storyboard storyboard = new Storyboard();
            storyboard.Duration = new Duration(TimeSpan.FromSeconds(10.0));
            DoubleAnimation rotateAnimation = new DoubleAnimation()
            {
                From = 0,
                To = 360 * 4, // 4 vueltas
                Duration = storyboard.Duration
            };

            Storyboard.SetTarget(rotateAnimation, imagenCool);
            Storyboard.SetTargetProperty(rotateAnimation, new PropertyPath("(UIElement.RenderTransform).(RotateTransform.Angle)"));

            storyboard.Children.Add(rotateAnimation);
            Resources.Add("Storyboard", storyboard);
            ((Storyboard)Resources["Storyboard"]).Begin();
        }


        void esp_seraBorrado()
        {
            if(BorrarCuerpoEspacial != null)
                BorrarCuerpoEspacial(this);
        }


        void esp_cambioDeCoordenadas(double arg1, double arg2)
        {
            Canvas.SetLeft(this, arg1 );
            Canvas.SetTop(this, arg2);
        }
    }
}
