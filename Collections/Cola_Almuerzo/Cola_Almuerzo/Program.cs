using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cola_Almuerzo
{
    class Program
    {
        static void Main(string[] args)
        {
            List<Persona> lista = new List<Persona>();

            lista.Add(new Persona("Carlos")); // index 0
            lista.Add(new Persona("Jaime")); 
            lista.Add(new Persona("Maria"));
            lista.Add(new Persona("Miguel"));
            lista.Add(new Persona("Patricio")); // index 4
            lista.Add(new Persona("Iván"));
            lista.Add(new Persona("Belén"));

            LinkedList l = new LinkedList();
            foreach (Persona p in lista)
                l.Add(p);
            LinkedList<string> asd = new LinkedList<string>();
            asd.
            Console.WriteLine((l.getElement(0) as Persona).nombre);
            Console.WriteLine((l.getElement(1) as Persona).nombre);
            Console.WriteLine((l.getElement(4) as Persona).nombre);
           Console.WriteLine((l.getElement(10) as Persona));
            Console.WriteLine((l.getElement(-1) as Persona));

            Stack<string> s = new Stack<string>();
            s.Peek(

            List<string> listaString = new List<string>();
            listaString.Add("item 0");
            listaString.Add("item 1");
            listaString.Add("item 2");
            listaString[2] = "item 2 otra vez";
            //listaString[3] = "item 3";
            string ultimo = listaString.ElementAt(listaString.Count - 1);
            string mismoUltimo = listaString[listaString.Count - 1];
            listaString.Remove(ultimo);
            listaString.Remove("item 1");
            listaString.RemoveAt(0);
            listaString.Clear();


            Console.ReadLine();
            /* 
             * Elejimos que cola vamos a usar:
             */
            AlmuerzoQueue cola = new AlmuerzoQueue();
            //MicroondaQueue cola = new MicroondaQueue(0.5f);
            //AlmuerzoQueueSeguridad cola = new AlmuerzoQueueSeguridad();

            // Por qué no: 
            // AlmuerzoQueue cola = new MicroondaQueue(0.5f);
            // ?

            // Los mandamos a todos a hacer fila
            foreach(Persona p in lista)
                cola.Queue(p);

            Imprimir(cola);

            // Vamos a colar a nuestros amigos
            Persona adrian = new Persona("Adrián");
            Persona vicho = new Persona("Vicho");
            cola.Colar(vicho, lista.ElementAt(4));
            cola.Colar(adrian, lista.ElementAt(4));

            Imprimir(cola);

            // Colao' de colao'
            cola.Colar(new Persona("Pietro"), adrian);

            Imprimir(cola);

            // Salen los tres primeros 
            Console.WriteLine("Sale de la cola: " + cola.Dequeue().nombre);
            Console.WriteLine("Sale de la cola: " + cola.Dequeue().nombre);
            Console.WriteLine("Sale de la cola: " + cola.Dequeue().nombre);

            Imprimir(cola);

            // Entra una persona y salen los primeros dos
            cola.Queue(new Persona("Raul"));
            Console.WriteLine("Sale de la cola: " + cola.Dequeue().nombre);
            Console.WriteLine("Sale de la cola: " + cola.Dequeue().nombre);

            Imprimir(cola);

            // Sale una persona e inmediatamente vuelve a hacer fila 
            Persona hambriento = cola.Dequeue();
            cola.Queue(hambriento);
            Console.WriteLine(hambriento.nombre +" se vuelve a meter a la cola");

            Imprimir(cola);

        }

        static void Imprimir(AlmuerzoQueue almuerzoQ)
        {
            Console.WriteLine("-------------------------");
            Console.WriteLine("Hasta el momento hay " +almuerzoQ.Count +" personas en la cola");
            Console.WriteLine(almuerzoQ.ToString());
            Console.WriteLine("-------------------------");
            Console.ReadKey();
            Console.Clear();
        }
    }
}
