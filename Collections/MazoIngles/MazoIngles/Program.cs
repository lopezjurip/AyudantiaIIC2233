using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MazoIngles
{
    class Program
    {
        static void Main(string[] args)
        {
            Mazo mazo = new Mazo();
            Console.WriteLine(mazo.ToString());

            Console.WriteLine("Presione cualquier tecla para revolverlo.");
            Console.ReadKey(true);
            Console.Clear();

            mazo.Revolver();
            Console.WriteLine(mazo.ToString());

            Console.WriteLine("Presione cualquier tecla para salir.");
            Console.ReadKey(true);
        }
    }

    /* ¿Qué es una enum?
     * http://msdn.microsoft.com/es-es/library/sbbt4032.aspx
     */
    enum Naipe 
    { 
        Pica, Corazon, Diamante, Trebol 
    };

    enum Numero
    {
        As, Dos, Tres, Cuatro, Cinco,
        Seis, Siete, Ocho, Nueve, Dies,
        Jota, Queen, Rey
    };

    class Carta
    {
        /// <summary>
        /// Representa la pica de la carta.
        /// </summary>
        public Naipe Naipe { get; set; }

        /// <summary>
        /// Representa el número, se usa un Enum para que el J, Q y K tengan un valor númerico
        /// </summary>
        public Numero Numero { get; set; }

        /// <summary>
        /// Carta
        /// </summary>
        /// <param name="naipe">Indica la pica</param>
        /// <param name="numero">El número</param>
        public Carta(Naipe naipe, Numero numero)
        {
            Naipe = naipe;
            Numero = numero;
        }

        /// <summary>
        /// Representación en texto de la carta.
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return "Naipe: " + Naipe + " | Número: " + Numero;
        }
    }

    class Mazo
    {
        /// <summary>
        /// Cartas para cada pica.
        /// </summary>
        public const int CARTAS_POR_PICA = 13;

        /// <summary>
        /// Cartas totales.
        /// </summary>
        public const int MAX_TAMANO_MAZO = CARTAS_POR_PICA * 4;

        /// <summary>
        /// Stack que representa la baraja
        /// </summary>
        protected Stack<Carta> baraja;

        /// <summary>
        /// Mazo inglés de 52 cartas.
        /// </summary>
        public Mazo()
        {
            baraja = new Stack<Carta>(MAX_TAMANO_MAZO);

            Naipe[] picas = { Naipe.Corazon, Naipe.Diamante, Naipe.Pica, Naipe.Trebol };
            foreach (Naipe pica in picas)
                foreach (Carta carta in CrearCartas(pica))
                    baraja.Push(carta);
        }

        /// <summary>
        /// Crear las cartas para una pica.
        /// </summary>
        /// <param name="pica">Pica a usar</param>
        /// <returns>Cartas del 1 al Rey con es pica.</returns>
        protected List<Carta> CrearCartas(Naipe pica)
        {
            List<Carta> cartas = new List<Carta>();
            cartas.Add(new Carta(pica, Numero.As));
            cartas.Add(new Carta(pica, Numero.Dos));
            cartas.Add(new Carta(pica, Numero.Tres));
            cartas.Add(new Carta(pica, Numero.Cuatro));
            cartas.Add(new Carta(pica, Numero.Cinco));
            cartas.Add(new Carta(pica, Numero.Seis));
            cartas.Add(new Carta(pica, Numero.Siete));
            cartas.Add(new Carta(pica, Numero.Ocho));
            cartas.Add(new Carta(pica, Numero.Nueve));
            cartas.Add(new Carta(pica, Numero.Dies));
            cartas.Add(new Carta(pica, Numero.Jota));
            cartas.Add(new Carta(pica, Numero.Queen));
            cartas.Add(new Carta(pica, Numero.Rey));
            return cartas;
        }

        /// <summary>
        /// Una manera más inteligente de crear las cartas para una pica.
        /// </summary>
        /// <param name="pica">Pica a usar</param>
        /// <returns>Cartas del 1 al Rey con es pica.</returns>
        protected List<Carta> CrearCartasInteligente(Naipe pica)
        {
            List<Carta> cartas = new List<Carta>();
            for (int n = 0; n < CARTAS_POR_PICA; n++)
            {
                Numero num = (Numero)n; // Una enumeracion son números (int) ;)
                cartas.Add(new Carta(pica, num));
            }
            return cartas;
        }

        /// <summary>
        /// Revuelve el mazo.
        /// </summary>
        public void Revolver()
        {
            Carta[] cartas = Shuffle(baraja.ToArray());
            baraja.Clear();
            foreach(Carta carta in cartas)
                baraja.Push(carta);
        }

        /// <summary>
        /// Desordena la baraja usando el algoritmo Fisher and Yates
        /// http://en.wikipedia.org/wiki/Fisher%E2%80%93Yates_shuffle
        /// </summary>
        /// <param name="cartas">Cartas a desordenar</param>
        /// <returns>Cartas desordenadas</returns>
        private static Carta[] Shuffle(Carta[] cartas)
        {
            Random r = new Random();
            for (int n = cartas.Length - 1; n > 0; --n)
            {
                int k = r.Next(n + 1);
                Carta temp = cartas[n];
                cartas[n] = cartas[k];
                cartas[k] = temp;
            }
            return cartas;
        }

        /// <summary>
        /// Imprime el contenido del mazo
        /// </summary>
        /// <returns>Contenido</returns>
        public override string ToString()
        {
            StringBuilder builder = new StringBuilder();
            builder.AppendLine("Mazo de " + baraja.Count + " cartas:");
            foreach(Carta c in baraja)
                builder.AppendLine(c.ToString());

            return builder.ToString();
        }
    }
}
