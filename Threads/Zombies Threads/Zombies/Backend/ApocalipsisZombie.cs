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
        // #iLoveBuenasPrácticas
        private const int VidasIniciales = 5;
        private const int maximoDeZombies = 1000;

        // Cosas importatnes.
        protected List<Zombie> zombiesActuales;
        private Jugador player;
        private Random random;

        // Celdas de la grilla (es cuadrada)
        int dimensionGrilla;

        // Modo jardcore
        bool modoJardcore = false;
        const int jardCoreRate = 50;
        public bool ModoJardcore {
            get { return modoJardcore; }
            set { modoJardcore = value; } 
        }

        // Player actual
        public Jugador jugadorActual{
            get { return player; }
        }

        // Esto es para que el frontend cree un UserControl para el zombie que le pasamos acá
        public event Action<Zombie> aparecioUnZombie;

        // Constructor
        public ApocalipsisZombie(int dimensionGrilla)
        {
            this.dimensionGrilla = dimensionGrilla;
            random = new Random();
            player = new Jugador(generadorAleatorioDeCoordenadas(), VidasIniciales);
            zombiesActuales = new List<Zombie>();
        }

        public void StartApocalipsis()
        {
            // Thread anonimo, esta es una de las maneras de iniciar un thread.
            Thread thread_generadorDeZombies = new Thread(delegate()
                {
                #region Subproceso que crea zombies
                
                int tiempoDeEspera = 3000;
                while (zombiesActuales.Count <= maximoDeZombies)
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
                        tiempoDeEspera = random.Next(500, 4000);

                #endregion
                }
            });
            thread_generadorDeZombies.Start();
        }

        protected virtual Zombie CrearZombie()
        {
            Coords temp;
            do
            {
                temp = generadorAleatorioDeCoordenadas();
            }
            while (temp.dentroDeLasDimensiones(dimensionGrilla) == false);

            Zombie z = new Zombie(temp, random.Next(1, 10));
            lock (z) // Puede que en el do while uno se atrase y entre otro zombie.
            {
                /* Nos suscribimos a las Func 
                 * Es decir, nos comprometemos a entregarlo lo que pide a la clase.
                 */
                z.dimensionDelMundo += z_dimensionDelMundo; // Aquí nos pide un int
                z.coordenadasDelPlayer += z_coordenadasDelPlayer; // Aquí nos piden las coords del player.
                z.hayUnZombieEnEsaCoordenada += z_hayUnZombieEnEsaCoordenada; // Aqui nos preguntan si es que hay un zombie en dada posición

                // Notificamos al frontend que se creó un zombie.
                if (aparecioUnZombie != null)
                    aparecioUnZombie(z);

                zombiesActuales.Add(z);
            }

            return z;
        }

        // Método respuesta a la Func: hayUnZombieEnEsaCoordenada
        protected virtual bool z_hayUnZombieEnEsaCoordenada(Coords arg)
        {
            if(arg.Equals(jugadorActual.Coordenadas))
                return true;

            #region Explicación de la exception:
            /* Como nuestro programa corre ahora con multiples subprocesos, tenemos un problema de concurrencia en este método.
             * Sucede que al leer la colección de zombies, está prohibido agregar, remover o reordear la dicha colección.
             * El problema es generado cuando el "thread_generadorDeZombies" agrega justo un zombie cuando estamos recorriendo la lista.
             * Esto causa que el sistema arroje una exception.
             * 
             * Una solución podria ser usando ResetEvents para controlar las operaciones que involucren a la lista.
             * El ejercicio queda propuesto para ti, joven padawan.
             * 
             * No puse try catch para que se aprecie aquí lo delicado que es trabajar con threads.
             * Ámalos y trátalos con respeto, cuidado y paciencia. 
             * 
             * Para solucionarlo, usar ResetEvents. Queda propuesto como ejercicio para usted, jovén padawan.
             */
            #endregion
            // Si no nos importa que hayan 2 o más zombies en la misma celda:
            // return false;

            foreach (Zombie z in zombiesActuales)
            {
                if (arg.Equals(z.Coordenadas) == true)
                    return true;
            }
            return false;
        }

        // Método respuesta a la Func: coordenadasDelPlayer
        private Coords z_coordenadasDelPlayer()
        {
            // Esto se le pasa a la clase que nos lo pidió.
            // Para que el zombie sepa donde está el jugador
            return jugadorActual.Coordenadas;
        }

        // Método respuesta a la Func: dimensionDelMundo
        // Miren, el método generado retorna un int :O
        private int z_dimensionDelMundo()
        {
            // En este caso al zombie, para que no camine fuera del mapa.
            return dimensionGrilla;
        }

        // Nada mágico por aquí
        private Coords generadorAleatorioDeCoordenadas()
        {
            int x = random.Next(0, dimensionGrilla);
            int y = random.Next(0, dimensionGrilla);
            return new Coords(x, y);
        }

    }
}
