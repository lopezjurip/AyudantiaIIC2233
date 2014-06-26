using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cola_Almuerzo
{
    public class LinkedList
    {
        // Guardamos la referencia al primero y último
        private Nodo primero, ultimo;

        public LinkedList()
        {
            primero = ultimo = null;
        }

        public int Count;

        public void Add(object obj)
        {
            if (primero == null)
            {
                primero = new Nodo(obj);
                ultimo = primero; // Como hay un solo nodo...
            }
            else
            {
                ultimo.Next = new Nodo(obj);
                ultimo = ultimo.Next;
            }
        }

        public object getElement(int index)
        {
            int n;
            Nodo temp;
            for (temp = primero, n = 0;
                temp != null && n != index;
                temp = temp.Next, n++) ;

            // Si temp es null, temp.Objeto tira exception
            if (temp == null)
                return temp; 
            return temp.Objeto;
        }
    }

    class Arbol
    {
        public Object obj;
        public LinkedList ramas;

        public Arbol(Object obj)
        {
            this.obj = obj;
            ramas = new LinkedList();
        }

        public void AddRama(Arbol a)
        {
            ramas.Add(a);
        }

        public Arbol Buscar(Object o)
        {
            Arbol buscado = null;
            if (o.Equals(obj))
                buscado = this;
            else
            {
                for (int i = 0; i < ramas.Count; i++)
                {
                    buscado = (Arbol)ramas.Buscar(o);
                    if (buscao != null) break;
                }
            }
            return buscado;
        }
    }
}
