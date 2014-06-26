using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cola_Almuerzo
{
    class Nodo
    {
        // Referencia al nodo que le sigue
        private Nodo next;

        // Objeto a referenciar, recordemos que todas las clases heredan de object;
        private object objeto;

        // #YoAplicoBuenasPrácticas
        public Nodo Next
        {
            get { return next; }
            set { next = value; }
        }
        public object Objeto
        {
            get { return objeto; }
            set { objeto = value; }
        }

        // Constructor
        public Nodo(object obj)
        {
            objeto = obj; // guardamos la referencia
            next = null; // El nodo siguiente siempre parte como nulo
        }
    }

    class AlmuerzoQueue
    {
        // Guardamos la referencia al primero y último
        private Nodo primero, ultimo;

        public AlmuerzoQueue()
        {
            primero = ultimo = null;
        }

        public void Queue(Persona p)
        {
            if (primero == null)
            {
                primero = new Nodo(p);
                ultimo = primero; // Como hay un solo nodo...
            }
            else
            {
                ultimo.Next = new Nodo(p);
                ultimo = ultimo.Next;
            }
        }

        public Persona Dequeue()
        {
            Nodo temp = primero;
            primero = primero.Next;

            // Recordemos que viene como tipo object
            Persona primeraPersona = (Persona)temp.Objeto; 
            return primeraPersona;
        }

        public void Colar(Persona colado, Persona colador)
        {
            // Un poco de malabares jugando con los Next
            Nodo nodoDeColador = buscarNodoDePersona(colador);
            Nodo nodoDeColado = new Nodo(colado);
            nodoDeColado.Next = nodoDeColador.Next;
            nodoDeColador.Next = nodoDeColado;
        }

        private Nodo buscarNodoDePersona(Persona p)
        {
            // Si, así también puedes usar los for.
            for (Nodo temporal = primero; temporal != null; temporal = temporal.Next)
            {
                // wow
                Persona personaDelNodo = temporal.Objeto as Persona;
                if (personaDelNodo == p)
                    return temporal;
            }
            return null;
        }

        public override string ToString()
        {
            // Nada del otro mundo por aquí...
            String s = "";
            for (Nodo temporal = primero; temporal != null; temporal = temporal.Next)
                s = s + (temporal.Objeto as Persona).nombre + "\n";
            return s;
        }

        public int Count
        {
            get
            {
                // cool, no? 
                // Esto se llama solo con .count;
                Nodo temporal = primero;
                int n = 0;
                while (temporal != null)
                {
                    temporal = temporal.Next;
                    n++;
                }
                return n;
            }
        }
    }

    
}
