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
using System.Windows.Shapes;
using Backend;

namespace Chat2___Client
{
    /// <summary>
    /// Lógica de interacción para Chat.xaml
    /// </summary>
    public partial class Chat : Window
    {
        private Client Cliente { get; set; }

        public Chat(Client cliente)
        {
            InitializeComponent();
            InputText.Focus();

            this.Cliente = cliente;
            this.Cliente.MensajeRecibido += (texto) =>
            {
                Dispatcher.BeginInvoke(new Action(() =>
                {
                    StackPanelMensajes.Children.Add(new TextBlock { Text = "-> " + texto });
                }));
            };

            SendButton.Click += (s, e) =>
            {
                EnviarMensaje();
            };

            InputText.KeyUp += (s, e) =>
            {
                if (e.Key == Key.Enter)
                {
                    EnviarMensaje();
                }
            };
        }

        private void EnviarMensaje()
        {
            Cliente.EnviarMensaje(InputText.Text);
            InputText.Text = "";
        }
    }
}
