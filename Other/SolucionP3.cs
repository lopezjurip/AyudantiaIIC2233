using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TipicaP3
{
    class Auto { }

    class Credencial { }

    class NodoTupla<K, V>
    {
        public NodoTupla<K,V> Next { get; set; }
        public K Key { get; private set; }
        public V Value { get; private set; } 

        public NodoTupla(K key, V value) 
        {
            this.Key = key;
            this.Value = value;
        }
    }

    class Registro
    {
        public const int CAPACIDAD = 10;

        private Registro Next { get; set; }
        private NodoTupla<Auto, Credencial> Primero { get; set; }
        private NodoTupla<Auto, Credencial> Ultimo { get; set; }

        public void Registrar(Auto auto, Credencial credencial)
        {
            if (Contiene(auto))
            {
                Console.WriteLine("Llave duplicada, no se agrega al registro.");
            }
            else if (Count >= CAPACIDAD)
            {
                Console.WriteLine("Registro lleno, se pasará la responsabilidad al siguiente.");
                if (Next == null)
                    Next = new Registro();
                Next.Registrar(auto, credencial);
            }
            else
            {
                Console.WriteLine("Registrando...");
                if (Primero == null)
                    Primero = Ultimo = new NodoTupla<Auto, Credencial>(auto, credencial);
                else
                {
                    Ultimo.Next = new NodoTupla<Auto, Credencial>(auto, credencial);
                    Ultimo = Ultimo.Next;
                }
            }
        }

        public bool Contiene(Auto auto)
        {
            for (NodoTupla<Auto, Credencial> temp = Primero; temp != null; temp = temp.Next)
                if (auto.Equals(temp.Key))
                    return true;

            return (Next != null) ? Next.Contiene(auto) : false;

            /* Esa linea equivale a:
            if (Next != null)
                return Next.Contains(auto);
            else
                return false;
             */
        }

        public Credencial GetCredencial(Auto auto)
        {
            for (NodoTupla<Auto, Credencial> temp = Primero; temp != null; temp = temp.Next)
                if (auto.Equals(temp.Key))
                    return temp.Value;

            return (Next != null) ? Next.GetCredencial(auto) : null;
        }

        public void Remover(Auto auto)
        {
            // Propuesto para usted :)
        }

        public int Count
        {
            get
            {
                int count = 0;
                NodoTupla<Auto, Credencial> temp = Primero;
                while (temp != null)
                {
                    temp = temp.Next;
                    count++;
                }
                return count;
            }
        }

        public int TotalCount
        {
            get
            {
                return (Next != null) ? Count + Next.TotalCount : Count;
            }
        }
    }
}