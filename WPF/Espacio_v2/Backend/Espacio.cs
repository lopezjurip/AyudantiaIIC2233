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
        private const int maximosObjetosEnElEspacio = 200;
        private const int crearObjetosCuandoSeItere = 100000000;
        
        /* Probabilidades */
        private const int probabilidadPlaneta = 10;
        private const int probabilidadMeteorito = 40;
        private const int probabilidadEstrella = 40;
        private const int probabilidadQuazar = 10;

        /* Variables importantes. */
        private Random random;
        private Queue<ObjetoEspacial> objetosDelFirmamento;
        private double cuantoMeMuevoReset;
        internal double spaceWidth, spaceHeight;

        /* Evento */
        public event Action<ObjetoEspacial> naceUnObjeto;

        /* Constructor */
        public Espacio(double spaceWidth, double spaceHeight)
        {
            this.spaceHeight = spaceHeight;
            this.spaceWidth = spaceWidth;
            random = new Random();
            objetosDelFirmamento = new Queue<ObjetoEspacial>();
        }

        /* Se llama cada vez que se mueve el mouse */
        public void entregarPixelesViajados(double p)
        {
            cuantoMeMuevoReset += p;
            foreach (ObjetoEspacial esp in objetosDelFirmamento)
                esp.Moverse(p);

            /* Cuando llevamos más de "n" pixeles añadimos un objeto espacial. */
            if (cuantoMeMuevoReset < crearObjetosCuandoSeItere)
            {
                /* Alocacion del nuevo objeto */
                cuantoMeMuevoReset = 0;
                ObjetoEspacial nuevo;
                int numero = random.Next(0,101);
                if (numero <= probabilidadPlaneta)
                    nuevo = new Planeta(random);
                else if (numero > probabilidadPlaneta && numero <= probabilidadMeteorito + probabilidadPlaneta)
                    nuevo = new Meteorito(random);
                else if (numero > probabilidadMeteorito + probabilidadPlaneta && numero <= probabilidadPlaneta + probabilidadMeteorito + probabilidadEstrella)
                    nuevo = new Estrella(random);
                else
                    nuevo = new BlackHole(random);

                /* Por cosas de rendimiento. 
                 * Hay un límite de objetos.
                 */
                if (objetosDelFirmamento.Count >= maximosObjetosEnElEspacio)
                {
                    ObjetoEspacial temp = objetosDelFirmamento.Dequeue();
                    temp.prepararParaBorrar();
                    temp = null;
                }
                /* Guardamos la referencia */
                objetosDelFirmamento.Enqueue(nuevo);

                /* Reset de las coords */
                double inicioX = random.NextDouble() * spaceWidth;
                double inicioY = random.NextDouble() * spaceHeight;
                nuevo.setInicio(inicioX, inicioY);

                /* Avisamos al MainWindow que nace un objeto */
                if (naceUnObjeto != null) // check
                    naceUnObjeto(nuevo);
            }
        }

        public void cambiarTamano(double W, double H)
        {
            spaceHeight = H;
            spaceWidth = W;
        }
    }
}
