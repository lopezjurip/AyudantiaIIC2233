using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace Backend
{
    public class ApocalipsisZombie
    {
        // Constantes.
        private const int MAX_ZOMBIES = 1000;

        // Cosas importatnes.
        protected List<Zombie> ZombiesActuales;
        public Jugador Player { get; private set; }
        private Random Random;

        // Celdas de la grilla (es cuadrada)
        int DimensionGrilla;

        // Modo jardcore
        bool modoJardcore = false;
        const int jardCoreRate = 50;
        public bool ModoJardcore {
            get { return modoJardcore; }
            set { modoJardcore = value; } 
        }

        // Esto es para que el frontend cree un UserControl para el zombie que le pasamos acá
        public event Action<Zombie> AparecioUnZombie;

        // Constructor
        public ApocalipsisZombie(int dimensionGrilla)
        {
            this.DimensionGrilla = dimensionGrilla;
            Random = new Random();
            Player = new Jugador(generadorAleatorioDeCoordenadas());
            ZombiesActuales = new List<Zombie>();
        }

        public void StartApocalipsis()
        {
            // Thread anonimo, esta es una de las maneras de iniciar un thread.
            Thread threadGeneradorDeZombies = new Thread(() =>
                {
                    #region Subproceso que crea zombies

                    int tiempoDeEspera = 3000;
                    while (ZombiesActuales.Count <= MAX_ZOMBIES)
                    {
                        Zombie z = CrearZombie();

                        // Iniciamos el proceso de vida del zombie.
                        // Cada zombie tiene "vida propia", ya no tenemos que hacer un foreach para cada zombie. Ahora son independientes.
                        z.IniciarThread();

                        // Para evitar llenar de zombies, tenemos un tiempo de espera. 
                        // Este Sleep duerme al thread_generadorDeZombies, 
                        // NO AL THREAD PRINCIPAL COMO LO HACIAMOS ANTES.
                        Thread.Sleep(tiempoDeEspera);

                        // Tiempo de espera random
                        if (modoJardcore)
                            tiempoDeEspera = jardCoreRate;
                        else
                            tiempoDeEspera = Random.Next(500, 4000);

                    #endregion
                    }
                });
            threadGeneradorDeZombies.IsBackground = true;
            threadGeneradorDeZombies.Start();
        }

        protected virtual Zombie CrearZombie()
        {
            Coords temp;
            do
            {
                temp = generadorAleatorioDeCoordenadas();
            }
            while (temp.dentroDeLasDimensiones(DimensionGrilla) == false);

            Zombie z = new Zombie(temp, Random.Next(Zombie.MIN_MULTIPLER, Zombie.MAX_MULTIPLER));

            /* Nos suscribimos a las Func 
             * Es decir, nos comprometemos a entregarlo lo que pide a la clase.
             */
            z.PreguntarDimensionMundo += () =>
            {
                return DimensionGrilla;
            };

            z.PreguntarDondeEstaElJugador += EntregarCoordenadasPlayer; // Aquí nos piden las coords del player.

            z.PreguntarHayUnZombieCoordenada += RevisarZombieEnCoordenada; // Aqui nos preguntan si es que hay un zombie en dada posición

            // Notificamos al frontend que se creó un zombie.
            if (AparecioUnZombie != null)
                AparecioUnZombie(z);

            lock (ZombiesActuales)  
            {
                // Evitamos que el método "Add" modifique la lista cuando se está iterando sobre ella.
                ZombiesActuales.Add(z);
            }

            return z;
        }

        // Método respuesta a la Func: hayUnZombieEnEsaCoordenada
        protected bool RevisarZombieEnCoordenada(Coords arg)
        {
            if(arg.Equals(Player.Coordenadas))
                return true;

            lock (ZombiesActuales) // Si quitamos este Lock vamos a tener problemas, te invito a hacerlo :)
            {
                foreach (Zombie z in ZombiesActuales)
                {
                    if (arg.Equals(z.Coordenadas) == true)
                        return true;
                }
            }
            return false;
        }

        // Método respuesta a la Func: coordenadasDelPlayer
        private Coords EntregarCoordenadasPlayer()
        {
            // Esto se le pasa a la clase que nos lo pidió.
            // Para que el zombie sepa donde está el jugador
            return Player.Coordenadas;
        }

        // Nada mágico por aquí
        private Coords generadorAleatorioDeCoordenadas()
        {
            int x = Random.Next(0, DimensionGrilla);
            int y = Random.Next(0, DimensionGrilla);
            return new Coords(x, y);
        }

    }
}
