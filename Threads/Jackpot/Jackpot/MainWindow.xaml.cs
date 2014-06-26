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
using System.Threading;

namespace Jackpot
{
    /// <summary>
    /// Por Patricio López Juri
    /// </summary>
    public partial class MainWindow : Window
    {
        bool isJugando = false;
        Button[] slots;
        String[] Ies;
        int[] jugadores = {1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 
                           11, 12, 13, 14, 15, 16, 17, 18, 19, 20, 
                           21, 22, 23, 24, 25, 26, 27, 28, 29, 30, 
                           31, 32, 33, 34, 35, 36, 37, 38, 39, 40, 
                           41, 42, 43, 44, 45, 46, 47, 48, 49, 50, 
                           51, 52, 53, 54, 55, 56, 57, 58, 59, 60, 
                           61, 62, 63, 64, 65, 66, 67, 68, 69, 70, 
                           71, 72, 73, 74, 75};
        /* RESPALDO
        int[] jugadores = {1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 
                           11, 12, 13, 14, 15, 16, 17, 18, 19, 20, 
                           21, 22, 23, 24, 25, 26, 27, 28, 29, 30, 
                           31, 32, 33, 34, 35, 36, 37, 38, 39, 40, 
                           41, 42, 43, 44, 45, 46, 47, 48, 49, 50, 
                           51, 52, 53, 54, 55, 56, 57, 58, 59, 60, 
                           61, 62, 63, 64, 65, 66, 67, 68, 69, 70, 
                           71, 72, 73, 74, 75};
         */
        Thread TimerThread;
        ManualResetEvent Rodar = new ManualResetEvent(false); // Inician bloqueados.
        ManualResetEvent TimerEvent = new ManualResetEvent(false);
        int Puntaje = 0;

        public MainWindow()
        {
            InitializeComponent();
            // Prefiero poner lo visual por medio de la interfaz visial del XAML, porque uno ve en tiempo real lo que se hace.
            // Posteriormente al iniciar el programa hago que inicien ocultas.
            Manilla_down1.Visibility = Visibility.Collapsed;
            Manilla_down2.Visibility = Visibility.Collapsed;

            // Manilla_up es una Image.
            Manilla_up.MouseLeftButtonDown += Manilla_up_MouseLeftButtonDown;
            Manilla_up.MouseLeftButtonUp += Manilla_up_MouseLeftButtonUp;

            slots = new Button[3];
            Ies = new String[4];
            slots[0] = slot1;
            slots[1] = slot2;
            slots[2] = slot3;
            Ies[0] = "I1";
            Ies[1] = "I2";
            Ies[2] = "I3";
            Ies[3] = "E";

            // Creamos los Threads, cada Thread usa el mismo método pero recibe un input que indica qué
            // tan rápido cambiarán los valores de la casilla correspondiente.
            Thread Slot1 = new Thread(Spin);
            Slot1.IsBackground = true;
            Slot1.Start("1"); // Uso un String pues debe recibir un Object, recordemos que un String es un objeto.

            Thread Slot2 = new Thread(Spin);
            Slot2.IsBackground = true;
            Slot2.Start("2");

            Thread Slot3 = new Thread(Spin);
            Slot3.IsBackground = true;
            Slot3.Start("3");

            TimerThread = new Thread(Timer);
            TimerThread.IsBackground = true;
            TimerThread.Start();
        }

        private void Spin(object slot)
        {
            Random r = new Random();
            int Slot = Int32.Parse(slot as String);
            int delay = 0;
            switch (Slot)
            {
                case (1):
                    delay = 500;
                    break;
                case (2):
                    delay = 200;
                    break;
                case (3):
                    delay = 10;
                    break;
            }
            while (2+2==4)
            {
                Rodar.WaitOne(); // Esperamos que el ManualResetEvent de la señal.
                lock (slots[Slot - 1])
                {
                    string s = "";
                    switch (Slot)
                    {
                        case (1):
                            s = jugadores[r.Next(0, jugadores.Length)] + "";
                            break;
                        case (2):
                            s = Ies[r.Next(0,4)];
                            break;
                        case (3):
                            Puntaje = r.Next(-5, 5);
                            if (Puntaje > 0)
                                s = "+" + Puntaje;
                            else
                                s = Puntaje + "";
                            break;
                    }
                    Actualizar(Slot, s);
                    Thread.Sleep(delay);
                }
            }
        }

        void Actualizar(int slot, string s)
        {
            this.Dispatcher.BeginInvoke(new Action<int, string>(Actualizar_dispatcher),slot,s);
        }
        void Actualizar_dispatcher(int slot, string s)
        {
            slots[slot - 1].Content = s; // Actualizamos la interfaz.
        }

        void Manilla_up_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (isJugando)
                return;

            Image i = (Image)sender;
            i.Opacity = 0.1;
            Manilla_down1.Visibility = Visibility.Visible;
            Manilla_down2.Visibility = Visibility.Visible;
            player.Source = new Uri(@"..\..\handle.wav", UriKind.Relative);
            player.Volume = 1;
            player.Play(); 
        }

        void Manilla_up_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            if (isJugando)
                return;

            Image i = (Image)sender;
            i.Opacity = 1;
            Manilla_down1.Visibility = Visibility.Collapsed;
            Manilla_down2.Visibility = Visibility.Collapsed;
            player2.Source = new Uri(@"..\..\jugando.wav", UriKind.Relative);
            player2.Volume = 1;
            player2.Play();
            isJugando = true;
            foreach (Button b in slots)
                b.FontSize = 30;
            TimerEvent.Set();
            Rodar.Set();
        }

        void EpicSound()
        {
            this.Dispatcher.BeginInvoke(new Action(EpicSoundDispatcher),null);
        }

        void EpicSoundDispatcher()
        {
            if (Puntaje > 0)
            {
                player.Source = new Uri(@"..\..\win.wav", UriKind.Relative);
                player.Volume = 1;
                player.Play();
            }
            else
            {
                player.Source = new Uri(@"..\..\fail.mp3", UriKind.Relative);
                player.Volume = 1;
                player.Play(); 
            }
        }

        private void Timer()
        {
            bool GanarEsPosible = false;
            while (GanarEsPosible == false)
            {
                TimerEvent.WaitOne();
                Thread.Sleep(8200);
                Rodar.Reset();
                EpicSound();
                isJugando = false;
                TimerEvent.Reset();
            }
        }
    }
}
