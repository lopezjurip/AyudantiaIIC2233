using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace HAL9000
{
    public delegate void Movimiento();

    public class Tron
    {
        public event Action<Coord, bool> Pintar;
        public event Action Perder;
        public event Action<Coord> CrearPelotaAqui;

        ManualResetEvent CrearPelotitaResetEvent = new ManualResetEvent(true);
        public Movimiento Moverse; // Delegate
        public Cell Cabeza = null;
        public bool isRed;
        public bool InvalidarDerecha, InvalidarIzquierda, InvalidarAbajo, InvalidarArriba = false;
        Random RANDOM = new Random();
        Coord PelotitaActual;

        public Tron(Cell Inicio, bool isRed)
        {
            Cabeza = Inicio;
            this.isRed = isRed;
            Moverse = Nada;
        }

        public void EmpezarAPonerPelotitas(int TamanoCuadricula)
        {
            Thread Pelotitas = new Thread(CrearPelotitas);
            Pelotitas.IsBackground = true;
            Pelotitas.Start(TamanoCuadricula + ""); // un String es un Objeto
        }

        private void CrearPelotitas(object input) // OJO, todos los Threads parametrizados reciben OBJECT
        {
            int n = Int32.Parse(input as String);
            while (true)
            {
                CrearPelotitaResetEvent.WaitOne();
                PelotitaActual = new Coord(RANDOM.Next(0, n), RANDOM.Next(0, n));
                if(CrearPelotaAqui != null)
                    CrearPelotaAqui(PelotitaActual);
                CrearPelotitaResetEvent.Reset();
            }
        }

        public void Avanzar(Cell Objetivo)
        {
            // Vemos que no nos comimos a nosotros mismos -------------
            Cell temp = Cabeza;
            do
            {
                if(temp.Equals(Objetivo))
                {
                    Perder();
                    Moverse = Nada;
                    return;
                }
                else
                    temp = temp.next;
            }
            while(temp != null);
            // --------------------------------------------------------

            if (Objetivo.XY.Equals(PelotitaActual)) // Se supone que eran por valor :S
                CrearPelotitaResetEvent.Set(); // Esto verifica si comimos la pelotita vigente

            Objetivo.next = Cabeza;
            Cabeza = Objetivo;
            if (Pintar != null)
                Pintar(Cabeza.XY,isRed); // Damos la orden al Main para que pinte la celda.
        }


        // A TODOS LOS MÉTODOS SIGUIENES LOS ENCAPSULARÁ EL DELEGATE.
        // Son públicos y lo hice muy flaite en el Main.
        public void Nada()
        { }

        public void Derecha()
        {
            if (InvalidarDerecha)
                return;

            InvalidarIzquierda = true;
            InvalidarAbajo = false;
            InvalidarArriba = false;
            InvalidarDerecha = false;

            if (Cabeza.Derecha != null)
                Avanzar(Cabeza.Derecha);
            else
                Perder();
        }
        public void Izquierda()
        {
            if (InvalidarIzquierda)
                return;

            InvalidarIzquierda = false;
            InvalidarAbajo = false;
            InvalidarArriba = false;
            InvalidarDerecha = true;

            if(Cabeza.Izquierda != null)
                Avanzar(Cabeza.Izquierda);
            else
                Perder();
        }
        public void Arriba()
        {
            if (InvalidarArriba)
                return;

            InvalidarIzquierda = false;
            InvalidarAbajo = true;
            InvalidarArriba = false;
            InvalidarDerecha = false;

            if (Cabeza.Arriba != null)
                Avanzar(Cabeza.Arriba);
            else
                Perder();
        }
        public void Abajo()
        {
            if (InvalidarAbajo)
                return;

            InvalidarIzquierda = false;
            InvalidarAbajo = false;
            InvalidarArriba = true;
            InvalidarDerecha = false;

            if (Cabeza.Abajo != null)
                Avanzar(Cabeza.Abajo);
            else
                Perder();
        }
    }
}
