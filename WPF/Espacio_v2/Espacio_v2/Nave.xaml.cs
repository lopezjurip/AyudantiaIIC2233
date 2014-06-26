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
        bool activoCampoFuerza;

        public Nave()
        {
            InitializeComponent();

            // PArte el escudo apagado
            activoCampoFuerza = false;
            CampoFuerza.Visibility = Visibility.Hidden;

            // Aciones que gatillan el uso del escudo.
            ImagenNave.MouseLeftButtonDown += Nave_MouseLeftButtonDown;
            ImagenNave.MouseLeftButtonUp += Nave_MouseLeftButtonUp;
        }

        void Nave_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            CampoFuerza.Visibility = Visibility.Hidden;
            activoCampoFuerza = false;
        }

        void Nave_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            CampoFuerza.Visibility = Visibility.Visible;
            activoCampoFuerza = true;
            
            // ponemos un sonido corto
            MediaPlayer mplayer = new MediaPlayer();
            mplayer.Open(new Uri(@"..\..\Sonidos\shield.mp3", UriKind.Relative));
            mplayer.Volume = 1;
            mplayer.Play();
        }

    }
}
