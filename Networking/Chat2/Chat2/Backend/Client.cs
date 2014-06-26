using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Backend
{
    public class Client
    {
        // Campos importantes.
        private Socket clientSocket;
        private Thread EscucharServidorThread;

        // Eventos.
        public event Action<String> MensajeRecibido;

        // Constructor
        public Client()
        {

        }

        // Nos intentamos conectar a una IP
        public bool Conectar(String IP)
        {
            IPEndPoint Ep = null;
            int Puerto = Server.PUERTO;

            try
            {
                Ep = new IPEndPoint(IPAddress.Parse(IP), Puerto);
                clientSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                clientSocket.Connect(Ep);
            }
            catch (ArgumentOutOfRangeException)
            {
                // El número del puerto está fuera de los límites.
                return false;
            }
            catch (FormatException)
            {
                // El formato de la IP no es válido.
                return false;
            }
            catch (SocketException)
            {
                // Error de conexión.
                return false;
            }

            // Conexión satisfactoria.

            // Creamos un thread para escuchar al servidor.
            EscucharServidorThread = new Thread(EscucharServidor);
            EscucharServidorThread.IsBackground = true;
            EscucharServidorThread.Start();

            return true;
        }

        private void EscucharServidor()
        {
            while (clientSocket != null)
            {
                string mensaje;
                byte[] dataBuffer;
                int largo;

                try
                {
                    dataBuffer = new byte[256];
                    // No queremos bloquear el thread principal, por eso esta linea se ejecuta en un thread separado.
                    largo = clientSocket.Receive(dataBuffer);
                    mensaje = Encoding.UTF8.GetString(dataBuffer, 0, largo);

                    // El servidor nos mandó un mensaje.

                    if (MensajeRecibido != null && mensaje.Length != 0)
                        MensajeRecibido(mensaje);

                }
                catch (SocketException)
                {
                    
                }
            }
        }

        public void EnviarMensaje(String s)
        {
            try
            {
                byte[] data = Encoding.UTF8.GetBytes(s);
                clientSocket.Send(data);
            }
            catch (SocketException)
            {
                // No se pudo enviar el mensaje.
            }
        }
    }
}
