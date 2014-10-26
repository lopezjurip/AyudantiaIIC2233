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

namespace RecursionPRO
{
    /// <summary>
    /// Lógica de interacción para Nave.xaml
    /// </summary>
    public partial class Nave : UserControl
    {
        public Nave()
        {
            InitializeComponent();

            // Parte con el escudo apagado
            CampoFuerza.Visibility = Visibility.Hidden;

            /* Otra manera de suscribirse a los eventos:
             * Expresiones Lambda
             * http://msdn.microsoft.com/es-es/library/bb397687.aspx
             */

            // Aciones que gatillan el uso del escudo.
            ImagenNave.MouseLeftButtonDown += (sender, args) =>
            {
                CampoFuerza.Visibility = Visibility.Visible;

                // ponemos un sonido corto
                MediaPlayer mplayer = new MediaPlayer();
                mplayer.Open(new Uri(@"..\..\Sonidos\shield.mp3", UriKind.Relative));
                mplayer.Volume = 1;
                mplayer.Play();
            };

            ImagenNave.MouseLeftButtonUp += (sender, args) =>
            {
                CampoFuerza.Visibility = Visibility.Hidden;
            };
        }
    }
}
