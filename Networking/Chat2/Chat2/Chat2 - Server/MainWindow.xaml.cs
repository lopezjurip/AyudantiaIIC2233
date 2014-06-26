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
using Backend;

namespace Chat2___Server
{
    /// <summary>
    /// Lógica de interacción para MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Server server;

        public MainWindow()
        {
            InitializeComponent();

            textBox_IP.IsReadOnly = true;
            textBox_Port.IsReadOnly = true;

            server = new Server();
            server.MensajeRecibido += server_MensajeRecibido;
            server.UsuarioConectado += server_UsuarioConectado;

            // #YoPrometíNuncaBloquearElThreadPrincipal
            new Thread(delegate()
            {
                bool exito = server.StartServer();
                if (exito)
                {
                    String ip = Server.GetLocalIP();
                    int port = Server.PUERTO;

                    if (ip != null)
                    {
                        Dispatcher.BeginInvoke(new Action(() =>
                        {
                            textBox_IP.Text = ip;
                            textBox_Port.Text = "" + port;
                            Clipboard.SetText(ip);
                            server_MensajeRecibido("Se ha copiado la dirección IP al portapapeles.");
                        }));
                    }
                }
                else
                {
                    Dispatcher.BeginInvoke(new Action(() =>
                    {
                        // Aquí ocurre un error cuando no se puede volver a utilizar la IPEndPoint del server.
                        // Bastante extraño.
                        MessageBox.Show("No se pudo crear un servidor.");
                    }));
                }
            }).Start();
        }

        void server_Log(string obj)
        {
            Dispatcher.BeginInvoke(new Action(() =>
            {
                stackPanel_log.Children.Add(new TextBlock { Text = obj, Foreground = Brushes.DarkGray });
            }));
        }

        void server_UsuarioConectado()
        {
            Dispatcher.BeginInvoke(new Action(() =>
            {
                stackPanel_log.Children.Add(new TextBlock { Text = "Usuario conectado", Foreground = Brushes.Blue });
            }));
        }

        void server_MensajeRecibido(string obj)
        {
            Dispatcher.BeginInvoke(new Action(() =>
            {
                stackPanel_log.Children.Add(new TextBlock { Text = "-> " +obj });
            }));
        }
    }
}
