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
        Client cliente;

        public Chat(Client cliente)
        {
            InitializeComponent();
            textBox_input.Focus();

            this.cliente = cliente;
            this.cliente.MensajeRecibido += cliente_MensajeRecibido;

            button_send.Click += button_send_Click;
            textBox_input.KeyUp += textBox_input_KeyUp;
        }

        void textBox_input_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                TextBox t = (TextBox)sender;
                cliente.EnviarMensaje(t.Text);
                t.Text = "";
            }
        }

        void button_send_Click(object sender, RoutedEventArgs e)
        {
            cliente.EnviarMensaje(textBox_input.Text);
            textBox_input.Text = "";
        }

        void cliente_MensajeRecibido(string obj)
        {
            Dispatcher.BeginInvoke(new Action(() =>
            {
                stackPanel_mensajes.Children.Add(new TextBlock { Text = "-> " +obj});
            }));
        }
    }
}
