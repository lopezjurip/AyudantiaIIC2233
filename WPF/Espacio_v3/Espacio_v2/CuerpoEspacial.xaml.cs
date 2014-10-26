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
        /// <summary>
        /// Evento que notifica cuando debe ser borrado el cuerpo espacial.
        /// </summary>
        public event Action<CuerpoEspacial> BorrarCuerpoEspacial;

        /// <summary>
        /// Objeto del backend a representar
        /// </summary>
        private ObjetoEspacial ObjEspacial { get; set; }

        public CuerpoEspacial(ObjetoEspacial objEspacial)
        {
            InitializeComponent();
            this.ObjEspacial = objEspacial;

            Canvas.SetLeft(this, objEspacial.X);
            Canvas.SetTop(this, objEspacial.Y);
            this.Height = objEspacial.H;
            this.Width = objEspacial.W;

            imagenCool.Stretch = Stretch.Uniform;
            imagenCool.Source = new BitmapImage(new Uri(@"/Imagenes/" + objEspacial.NombreImagen, UriKind.Relative));

            /* Otra manera de suscribirse a los eventos:
             * Expresiones Lambda
             * http://msdn.microsoft.com/es-es/library/bb397687.aspx
             */

            // Subscripcion de eventos.
            objEspacial.CambioDeCoordenadas += (x, y) =>
            {
                // No les gustaba que Python adivinara el tipo de variable?
                Canvas.SetLeft(this, x);
                Canvas.SetTop(this, y);
            };

            objEspacial.SeraBorrado += () =>
            {
                if (BorrarCuerpoEspacial != null)
                    BorrarCuerpoEspacial(this);
            };
        }

        public void IniciarAnimacion()
        {
            DoubleAnimation animation = new DoubleAnimation();
            animation.From = 0;
            animation.To = 360;
            animation.Duration = new Duration(TimeSpan.FromMilliseconds(ObjEspacial.TiempoRotacion));
            animation.RepeatBehavior = RepeatBehavior.Forever;
            RotateTransform rotateTransform = new RotateTransform();
            imagenCool.RenderTransform = rotateTransform;
            rotateTransform.BeginAnimation(RotateTransform.AngleProperty, animation);
        }

        /// <summary>
        /// Descripción.
        /// </summary>
        /// <param name="input1">Qué es el parámetro 1.</param>
        /// <param name="input2">Qué es el parámetro 2.</param>
        /// <returns>Qué significa el retorno.</returns>
        public bool Metodo(int input1, String input2)
        {
            return true;
        }
    }
}
