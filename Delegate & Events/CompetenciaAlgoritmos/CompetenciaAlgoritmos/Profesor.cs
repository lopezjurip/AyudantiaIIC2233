using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompetenciaAlgoritmos
{
    /// <summary>
    /// Clase que evaluará y compará los algoritmos.
    /// </summary>
    public class Profesor
    {
        Alumno[] alumnos;

        public Profesor()
        {
            alumnos = new Alumno[] { new Pedro(), new Juan(), new Diego() };
        }

        /// <summary>
        /// Elige al alunmo que entregue la solución más rápida.
        /// </summary>
        /// <param name="input">String con palíndromos.</param>
        /// <param name="outputEsperado">El palíndromo más largo esperado.</param>
        /// <returns>El alumno que haga el mejor algoritmo.</returns>
        public Alumno AlgoritmoGanador(string input, string outputEsperado)
        {
            Alumno alumnoGanador = null;
            long tiempoGanador = long.MaxValue;

            foreach (Alumno a in alumnos)
            {
                // Iniciar cronometro
                System.Diagnostics.Stopwatch stopwatch = new System.Diagnostics.Stopwatch();
                stopwatch.Start();

                // Usar algoritmo del alumno.
                AlgoritmoPalindromo algoritmo = a.EntregarMiAlgoritmo();
                String propuestaAlumno = algoritmo(input);

                //Terminar cronometro
                stopwatch.Stop();
                long tiempoDemorado = stopwatch.ElapsedTicks;

                // Cumple con el output esperado.
                if (propuestaAlumno != null && propuestaAlumno.Equals(outputEsperado))
                {
                    Console.WriteLine(a + " lo hizo en :" +tiempoDemorado +" ticks.");

                    // si tiene un mejor tiempo que el mejor hasta el momento.
                    if (tiempoDemorado < tiempoGanador)
                    {
                        tiempoGanador = tiempoDemorado;
                        alumnoGanador = a;
                    }
                }
                else
                    Console.WriteLine(a + " ouput malo");
            }
            return alumnoGanador;
        }
    }
}
