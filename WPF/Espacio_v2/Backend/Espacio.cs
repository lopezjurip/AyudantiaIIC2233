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
        private double cuantoMeMuevoReset;

        public double LargoEspacio { get; set; }
        public double AltoEspacio { get; set; }


        /* Evento */
        public event Action<ObjetoEspacial> NaceUnObjeto;

        /* Constructor */
        public Espacio(double spaceWidth, double spaceHeight)
        {
            this.AltoEspacio = spaceHeight;
            this.LargoEspacio = spaceWidth;
            Rand = new Random();
            ObjetosDelFirmamento = new Queue<ObjetoEspacial>();
        }

        /* Se llama cada vez que se mueve el mouse */
        public void Tickear(double valor)
        {
            cuantoMeMuevoReset += valor;
            foreach (ObjetoEspacial esp in ObjetosDelFirmamento)
                esp.Moverse(valor);

            /* Cuando llevamos más de "n" pixeles añadimos un objeto espacial. */
            if (cuantoMeMuevoReset < TICKS_TO_CREATE_OBJECT)
            {
                double inicioX = Rand.NextDouble() * LargoEspacio;
                double inicioY = Rand.NextDouble() * AltoEspacio;

                /* Alocacion del nuevo objeto */
                cuantoMeMuevoReset = 0;
                ObjetoEspacial nuevo;
                int numero = Rand.Next(0,101);
                if (numero <= PROBABILIDAD_PLANETA)
                    nuevo = new Planeta(Rand, inicioX, inicioY);
                else if (numero > PROBABILIDAD_PLANETA && numero <= PROBABILIDAD_METEORITO + PROBABILIDAD_PLANETA)
                    nuevo = new Meteorito(Rand, inicioX, inicioY);
                else 
                    nuevo = new Estrella(Rand, inicioX, inicioY);

                /* Por cosas de rendimiento, 
                 * hay un límite de objetos.
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
