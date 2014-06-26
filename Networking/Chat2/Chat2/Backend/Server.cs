using System;
using System.Collections.Generic;
using System.Linq;
using System.Net; // Importado
using System.Net.Sockets; // Importado
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Backend
{
    public class Server
    {
        // Constantes
        private const int CONEXIONESMAXIMAS = Int32.MaxValue;   // El servidor soportará 2.147.483.647 conexiones.
        private const int SLEEPTIME = 10;                       // Tiempo de sleep para el thread que envía mensajes para no saturarlo.
        public const int PUERTO = 8000;                         // Puerto que usaremos para nuestro programa. Puede ser un número hasta el 65.535

        
        // Campos principales.
        private Socket serverSocket;                            // El socket principal del servidor.
        private List<Socket> clientes;                          // Lista de los Sockets de los clientes.
        private Queue<String> mensajesQueue;                    // Cola de mensajes pendientes para ser enviados. 


        // Eventos
        public event Action UsuarioConectado;                   // Se llama cuando se conecta un cliente y es añadido a la lista de sockets
        public event Action<String> MensajeRecibido;            // Se llama cuando se recibe un mensaje.


        // Constructor
        public Server()
        {
            clientes = new List<Socket>();
            mensajesQueue = new Queue<String>();
        }

        // Intentamos iniciar el servidor. 
        public bool StartServer()
        {
            // Creamos nuestro punto de conexión. 
            IPEndPoint Ep = null;
            try
            {
                Ep = new IPEndPoint(IPAddress.Any, PUERTO);
                serverSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                serverSocket.Bind(Ep);
                serverSocket.Listen(CONEXIONESMAXIMAS);
            }
            catch (SocketException)
            {
                return false;
            }

            Thread RecibirConexionesThread = new Thread(RecibirConexiones);
            RecibirConexionesThread.IsBackground = true;
            RecibirConexionesThread.Start();

            Thread ProcesadorDeMensajesThread = new Thread(ProcesadorDeMensajes);
            ProcesadorDeMensajesThread.IsBackground = true;
            ProcesadorDeMensajesThread.Start();

            return true;
        }

        // Difundimos un mensaje a todos los clientes. 
        private void DifundirMensaje(String mensaje)
        {
            lock (clientes)
            {
                foreach (Socket socket in clientes)
                {
                    byte[] mensajeBytes = Encoding.UTF8.GetBytes(mensaje);
                    socket.Send(mensajeBytes);
                }
            }
        }

        // Se encarga de repartir los mensajes en cola.
        private void ProcesadorDeMensajes()
        {
            while (mensajesQueue != null)
            {
                if (mensajesQueue.Count != 0)
                {
                    String mensaje = mensajesQueue.Dequeue();
                    DifundirMensaje(mensaje);
                    if (MensajeRecibido != null)
                        MensajeRecibido(mensaje);
                }
                Thread.Sleep(SLEEPTIME);
            }
            // Sería más bonito utilizar un ResetEvent. 
        }

        private void RecibirConexiones()
        {
            while (clientes.Count != CONEXIONESMAXIMAS)
            {
                // Intentaremos encontrar a un cliente.
                Socket client = null;

                try
                {
                    // Intentando encontrar alguna conexión entrante...

                    // Si no ejecutamos esta linea en un thread separado vamos a bloquear el programa.
                    client = serverSocket.Accept();

                    // Cliente conectado!

                    // Lo agregamos a nuestra lista.
                    lock (clientes) // Quizás este lock esté de más.
                    {
                        clientes.Add(client);
                    }

                    // Avisamos
                    if (UsuarioConectado != null)
                        UsuarioConectado();

                    // Iniciamos un Thread parametrizado que se dedicará a escuchar por este socket.
                    Thread EscucharClienteThread = new Thread(EscucharCliente);
                    EscucharClienteThread.IsBackground = true;
                    EscucharClienteThread.Start(client);

                }
                catch (SocketException)
                {
                   
                }
            }
        }

        private void EscucharCliente(object obj)
        {
            Socket socket = obj as Socket;

            while (socket != null)
            {
                string mensaje;
                byte[] dataBuffer;
                int largo;

                try
                {
                    dataBuffer = new byte[256];
                    // Debemos ejecutar esto en un thread o bloquearemos el thread principal (el que tiene la GUI).
                    largo = socket.Receive(dataBuffer);  // Este método se queda esperando hasta recibir algo.
                    mensaje = Encoding.UTF8.GetString(dataBuffer, 0, largo);

                    // Mandamos el mensaje a la cola de salida.
                    mensajesQueue.Enqueue(mensaje);
                }
                catch (SocketException)
                {
                    
                }
            }
        }


        public static String GetLocalIP()
        {
            // Esto es como ir a la consola (cmd.exe) y ejecutar:
            // ipconfig 
            // Y ver cuál es nuestra dirección IP local.

            IPHostEntry host;
            string localIP = "";
            host = Dns.GetHostEntry(Dns.GetHostName());
            foreach (IPAddress ip in host.AddressList)
            {
                if (ip.AddressFamily == AddressFamily.InterNetwork)
                {
                    localIP = ip.ToString();
                    break;
                }
            }
            return localIP;
        }
    }
}
