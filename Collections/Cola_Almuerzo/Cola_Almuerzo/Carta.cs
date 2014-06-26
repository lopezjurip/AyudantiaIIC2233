using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cola_Almuerzo
{
    enum Naipe { Pica, Corazon, Diamante, Trebol };
    enum Numero { 
        As, Uno, Dos, Tres, Cuatro, Cinco, 
        Seis, Siete, Ocho, Nueve, Dies, 
        Jota, Queen, Rey };

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

            bool usarInteligenia = false;
            foreach (Stack<Carta> pila in barajasDeNaipes)
            {
                Naipe naipe = tipos.Pop();
                if (usarInteligenia == false)
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
                    for (int i = 0; i < 13; i++)
                    {
                        Numero n = (Numero)i;
                        pila.Push(new Carta(naipe, n));
                    }
                }
            }
        }

        public void Revolver()
        {
            Random r = new Random();
            while (baraja.Count != capacidad)
            {
                Stack<Carta> temporal = barajasDeNaipes[r.Next(0, 4)];
                temporal.Reverse();
                baraja.Push(temporal.Pop());
            }
        }
    }
}
