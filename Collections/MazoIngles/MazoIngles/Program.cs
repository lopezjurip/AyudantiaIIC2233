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
            Mazo m = new Mazo();
            Mazo m_revuelto = m.Revolver();
            Console.WriteLine(m_revuelto.ToString());
            Console.ReadKey();
        }
    }

    enum Naipe { Pica, Corazon, Diamante, Trebol };
    enum Numero
    {
        As, Dos, Tres, Cuatro, Cinco,
        Seis, Siete, Ocho, Nueve, Dies,
        Jota, Queen, Rey
    };

    class Carta
    {
        public Naipe naipe;
        public Numero numero;
        public Carta(Naipe naipe, Numero numero)
        {
            this.naipe = naipe;
            this.numero = numero;
        }
    }

    class Mazo
    {
        // Baraja inglesa. 
        // PD: No es necesario ponerle una capacidad.
        static int capacidad = 52;
        Stack<Carta> baraja = new Stack<Carta>(capacidad);
        Stack<Carta>[] barajasDeNaipes = new Stack<Carta>[4];

        public Mazo()
        {
            Stack<Naipe> tipos = new Stack<Naipe>();
            tipos.Push(Naipe.Pica);
            tipos.Push(Naipe.Corazon);
            tipos.Push(Naipe.Diamante);
            tipos.Push(Naipe.Trebol);

            bool usarInteligencia = false;
            for (int i = 0; i < barajasDeNaipes.Length; i++)
            {
                Stack<Carta> pila = barajasDeNaipes[i] = new Stack<Carta>();
                Naipe naipe = tipos.Pop();
                if (usarInteligencia == false)
                {
                    pila.Push(new Carta(naipe, Numero.As));
                    pila.Push(new Carta(naipe, Numero.Dos));
                    pila.Push(new Carta(naipe, Numero.Tres));
                    pila.Push(new Carta(naipe, Numero.Cuatro));
                    pila.Push(new Carta(naipe, Numero.Cinco));
                    pila.Push(new Carta(naipe, Numero.Seis));
                    pila.Push(new Carta(naipe, Numero.Siete));
                    pila.Push(new Carta(naipe, Numero.Ocho));
                    pila.Push(new Carta(naipe, Numero.Nueve));
                    pila.Push(new Carta(naipe, Numero.Dies));
                    pila.Push(new Carta(naipe, Numero.Jota));
                    pila.Push(new Carta(naipe, Numero.Queen));
                    pila.Push(new Carta(naipe, Numero.Rey));
                }
                else
                {
                    for (int n = 0; n < 13; n++)
                    {
                        Numero num = (Numero)n; // Una enumeracion son números (int) ;)
                        pila.Push(new Carta(naipe, num));
                    }
                }
            }
        }

        public Mazo Revolver()
        {
            Random r = new Random();
            while (baraja.Count != capacidad)
            {
                Stack<Carta> temporal = barajasDeNaipes[r.Next(0, 4)];
                if (temporal.Count == 0)
                    continue;
                baraja.Push(temporal.Pop());
            }
            return this;
        }

        public override string ToString()
        {
            string s = "";
            foreach(Carta c in baraja)
            {
                s = s + "Naipe: " + c.naipe + " | Número: " + c.numero +"\n";
            }
            return s;
        }
    }
}
