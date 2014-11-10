using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Diagnostics;

namespace Backend
{
    public class Zombie : ElementoDeMapa
    {
        // en milisegundos
        private const int MOVEMENT_DELAY = 100;

        public const int MAX_MULTIPLER = 5;
        public const int MIN_MULTIPLER = DEFAULT_MULTIPLER;
        public const int DEFAULT_MULTIPLER = 1; // 1 = velocidad normal

        // Nuestro thread ;D
        private Thread ThreadMoverse;

        // Mientras más alto sea, más lento serán los zombies.
        private int Multiplicador = DEFAULT_MULTIPLER;

        // Acition
        public event Action<Coords> CambioDePosicion;

        // Function
        // Notemos que el último parámetro dentro de los "< >" es el retorno, en este casi el último es el único.
        // Todos los demás parámetros son de input.
        public event Func<int> PreguntarDimensionMundo;
        public event Func<Coords> PreguntarDondeEstaElJugador;

        // en esta Function pot ejemplo,
        public event Func<Coords, bool> PreguntarHayUnZombieCoordenada;
        // El método que sea asociado a esta Func debe tener de parámetro un Coord, y debe retornar un bool.

        // Constructor
        public Zombie(Coords cordenadas, int multiplicadorVelocidad)
            : base(cordenadas)
        {
            ThreadMoverse = new Thread(Vivir);
            ThreadMoverse.IsBackground = true;
            Multiplicador = multiplicadorVelocidad;
        }

        public void IniciarThread()
        {
            ThreadMoverse.Start();
        }

        private void Vivir()
        {
            while (true)
            {
                if (PreguntarDondeEstaElJugador != null) //Verificamos si hay alguien suscrito que nos diga.
                {
                    Coords coordenadasObjetivo = PreguntarDondeEstaElJugador();
                    Stack<Coords> posiblesPosiciones = this.PosiblesCordenadas();
                    Coordenadas = SeleccionarMejorPosicion(posiblesPosiciones, coordenadasObjetivo);


                    if (CambioDePosicion != null)
                        CambioDePosicion(Coordenadas);
                }

                Thread.Sleep(MOVEMENT_DELAY * Multiplicador);
            }
        }

        private Stack<Coords> PosiblesCordenadas()
        {
            Stack<Coords> posiblesPosiciones = new Stack<Coords>(4);
            Coords[] coordenadas = {   new Coords(this.Coordenadas.X + 1, this.Coordenadas.Y),
                                       new Coords(this.Coordenadas.X - 1, this.Coordenadas.Y),
                                       new Coords(this.Coordenadas.X, this.Coordenadas.Y + 1),   
                                       new Coords(this.Coordenadas.X, this.Coordenadas.Y - 1)};

            int tamanoDelMundo = 0;
            if (PreguntarDimensionMundo != null)
            {
                // Preguntamos de qué tamaño es el mundo.
                // Así se invoca una funcción, igual que al Action solo que esta retorna algo.
                tamanoDelMundo = PreguntarDimensionMundo(); 
            }
            else
            {
                // Nuestra propia exception
                throw new Exception("No se puede conocer al mundo y podemos hacer OutOfBoundException con una coordenada fuera del grid");
            }

            for (int i = 0; i < coordenadas.Length; i++)
            {
                Coords c = coordenadas[i];
                bool posicionValida = c.dentroDeLasDimensiones(tamanoDelMundo);

                bool hayUnZombieEnEsaPosicion = PreguntarHayUnZombieCoordenada(c);
                if (c.dentroDeLasDimensiones(tamanoDelMundo))       // No nos salimos del mapa
                    if (PreguntarHayUnZombieCoordenada != null)         // Hay alguien que nos diga si hay un zombie en "c"
                        if(PreguntarHayUnZombieCoordenada(c) == false)  // Nos dijeron que no hay un zombie en "c" 
                            posiblesPosiciones.Push(c);             // "c" es una posición válida
            }

            return posiblesPosiciones;
        }

        private Coords SeleccionarMejorPosicion(Stack<Coords> posibles, Coords objetivo)
        {
            double mejorDistancia;
            Coords mejorCoord;

            mejorCoord = new Coords(Coordenadas.X, Coordenadas.Y);
            mejorDistancia = distanciaEntreCoordenadas(mejorCoord, objetivo);

            while (posibles.Count != 0)
            {
                Coords candidato = posibles.Pop();
                double distancia = distanciaEntreCoordenadas(objetivo, candidato);
                if (distancia < mejorDistancia)
                {
                    mejorDistancia = distancia;
                    mejorCoord = candidato;
                }
            }

            return mejorCoord;
        }

        private double distanciaEntreCoordenadas(Coords c1, Coords c2)
        {
            return Math.Sqrt(Math.Pow(c1.X - c2.X, 2) + Math.Pow(c1.Y - c2.Y, 2));
        }

    }
}
