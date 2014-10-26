using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Backend
{
    public class Espacio
    {
        /* Parametros constantes de configuracion */
        private const int MAX_SPACE_OBJECTS = 200;
        private const int TICKS_TO_CREATE_OBJECT = 300;
        
        /* Probabilidades, sumados dan 100 */
        private const int PROBABILIDAD_PLANETA = 10;
        private const int PROBABILIDAD_METEORITO = 40;
        private const int PROBABILIDAD_ESTRELLA = 50;

        /* Variables importantes. */
        private Random Rand { get; set; }
        private Queue<ObjetoEspacial> ObjetosDelFirmamento { get; set; }
        private double ContadorTicks { get; set; }

        public double LargoEspacio { get; set; }
        public double AltoEspacio { get; set; }


        /// <summary>
        /// Evento que avisa que se ha creado un objeto espacial.
        /// </summary>
        public event Action<ObjetoEspacial> NaceUnObjeto;

        /// <summary>
        /// Constructor del espacio.
        /// </summary>
        /// <param name="spaceWidth">Largo actual de la ventana</param>
        /// <param name="spaceHeight">Alto actual de la ventana</param>
        public Espacio(double spaceWidth, double spaceHeight)
        {
            this.AltoEspacio = spaceHeight;
            this.LargoEspacio = spaceWidth;
            Rand = new Random();
            ObjetosDelFirmamento = new Queue<ObjetoEspacial>();
        }

        /// <summary>
        /// Llamar en cada tick con un valor relativo de cuanto se quiera mover.
        /// </summary>
        /// <param name="valor"></param>
        public void Tickear(double valor)
        {
            ContadorTicks += valor;
            foreach (ObjetoEspacial esp in ObjetosDelFirmamento)
                esp.Moverse(valor);

            /* Cada ciertos ticks creamos un nuevo objeto espacial. */
            if (ContadorTicks < TICKS_TO_CREATE_OBJECT)
            {
                double inicioX = Rand.NextDouble() * LargoEspacio;
                double inicioY = Rand.NextDouble() * AltoEspacio;

                /* Alocacion del nuevo objeto */
                ContadorTicks = 0;
                ObjetoEspacial nuevo;
                int numero = Rand.Next(0,101);

                if (numero <= PROBABILIDAD_PLANETA)
                    nuevo = new Planeta(Rand, inicioX, inicioY);
                else if (numero > PROBABILIDAD_PLANETA && numero <= PROBABILIDAD_METEORITO + PROBABILIDAD_PLANETA)
                    nuevo = new Meteorito(Rand, inicioX, inicioY);
                else 
                    nuevo = new Estrella(Rand, inicioX, inicioY);

                /* Por cosas de rendimiento, 
                 * ponemos un límite de objetos.
                 */
                if (ObjetosDelFirmamento.Count >= MAX_SPACE_OBJECTS)
                {
                    ObjetoEspacial temp = ObjetosDelFirmamento.Dequeue();
                    temp.PrepararParaBorrar();
                    temp = null;
                }

                /* Guardamos la referencia */
                ObjetosDelFirmamento.Enqueue(nuevo);

                /* Avisamos al MainWindow que nace un objeto */
                if (NaceUnObjeto != null) // check
                    NaceUnObjeto(nuevo);
            }
        }
    }
}
