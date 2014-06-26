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
        private const int delayEntreMovimiento = 150;

        // Nuestro thread ;D
        private Thread thread_Moverse;

        // Mientras más alto sea, más lento serán los zombies.
        private int multiplicador = 1; // 1 = velocidad normal

        // Acition
        public event Action<Coords> cambioDePosicion;

        // Function
        // Notemos que el último parámetro dentro de los "< >" es el retorno, en este casi el último es el único.
        // Todos los demás parámetros son de input.
        public event Func<int> dimensionDelMundo;
        public event Func<Coords> coordenadasDelPlayer;

        // en esta Function pot ejemplo,
        public event Func<Coords, bool> hayUnZombieEnEsaCoordenada;
        // El método que sea asociado a esta Func debe tener de parámetro un Coord, y debe retornar un bool.

        // Constructor
        public Zombie(Coords cordenadas, int multiplicadorVelocidad)
            : base(cordenadas)
        {
            thread_Moverse = new Thread(Vivir);
            multiplicador = multiplicadorVelocidad;
        }

        public void IniciarThread()
        {
            thread_Moverse.Start();
        }

        private void Vivir()
        {
            while (true)
            {
                if (coordenadasDelPlayer != null)
                {
                    Coords coordenadasObjetivo = coordenadasDelPlayer();
                    Stack<Coords> posiblesPosiciones = this.cuatroPosiblesNuevasPosicionesParaMi();
                    coordenadas = seleccionarMejorPosicion(posiblesPosiciones, coordenadasObjetivo);


                    if (cambioDePosicion != null)
                        cambioDePosicion(coordenadas);
                }

                Thread.Sleep(delayEntreMovimiento * multiplicador);
            }
            // El thread termina aquí (cuando termina el while), sin embargo lo puedo terminar en cualquier momento con:
            //thread_Moverse.Abort();
        }

        private Stack<Coords> cuatroPosiblesNuevasPosicionesParaMi()
        {
            Stack<Coords> posiblesPosiciones = new Stack<Coords>(4);
            Coords[] coordenadas = {   new Coords(this.coordenadas.X + 1, this.coordenadas.Y),
                                       new Coords(this.coordenadas.X - 1, this.coordenadas.Y),
                                       new Coords(this.coordenadas.X, this.coordenadas.Y + 1),   
                                       new Coords(this.coordenadas.X, this.coordenadas.Y - 1)};

            int tamanoDelMundo = 0;
            if (dimensionDelMundo != null)
            {
                // Preguntamos de qué tamaño es el mundo.
                // Así se invoca una funcción, igual que al Action solo que esta retorna algo.
                tamanoDelMundo = dimensionDelMundo(); 
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

                bool hayUnZombieEnEsaPosicion = hayUnZombieEnEsaCoordenada(c);
                if (c.dentroDeLasDimensiones(tamanoDelMundo))       // No nos salimos del mapa
                    if (hayUnZombieEnEsaCoordenada != null)         // Hay alguien que nos diga si hay un zombie en "c"
                        if(hayUnZombieEnEsaCoordenada(c) == false)  // Nos dijeron que no hay un zombie en "c" 
                            posiblesPosiciones.Push(c);             // "c" es una posición válida
            }

            return posiblesPosiciones;
        }

        private Coords seleccionarMejorPosicion(Stack<Coords> posibles, Coords objetivo)
        {
            double mejorDistancia;
            Coords mejorCoord;                

            bool zombiesTontos = true;
            if (zombiesTontos)
            {
                mejorDistancia = Double.MaxValue;
                mejorCoord = new Coords(coordenadas.X, coordenadas.Y);
            }
            else
            {
                mejorCoord = new Coords(coordenadas.X, coordenadas.Y);
                mejorDistancia = distanciaEntreCoordenadas(mejorCoord, objetivo);
            }

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
