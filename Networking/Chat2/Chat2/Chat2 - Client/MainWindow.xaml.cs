using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
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

namespace Chat2___Client
{
    /// <summary>
    /// Lógica de interacción para MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            button_connect.Click += button_connect_Click;

            // Con esto nos aseguramos que el TextBox esté listo para escribir.
            textBox_IP.Focus();
        }

        void button_connect_Click(object sender, RoutedEventArgs e)
        {
            Client cliente = null;
            Button button = sender as Button;
            String IP = textBox_IP.Text;

            textBox_IP.IsEnabled = button.IsEnabled = false;

            // Hacemos que la tarea de intentar conectarse se haga en un proceso por separado.
            // Esto para no bloquear el thread principal.
            // Una manera más correcta de hacer esto es utilizando Task:
            // http://msdn.microsoft.com/es-es/library/system.threading.tasks.task(v=vs.110).aspx
            //
            // Pero no nos compliquemos más, usemos solo un Thread:
            new Thread(delegate()
            {
                // Tareas en backbround.
                cliente = new Client();
                bool exito = cliente.Conectar(IP);

                // Una vez terminada las tareas volvemos al thread principal. 
                Dispatcher.BeginInvoke(new Action(() =>
                {
                    if (exito)
                    {
                        Chat chat = new Chat(cliente);
                        chat.Show();
                        this.Close();
                    }
                    else
                    {
                        MessageBox.Show("No se pudo conectar");
                        textBox_IP.IsEnabled = button.IsEnabled = true;
                    }
                }));
            }).Start();
        }
    }
}
