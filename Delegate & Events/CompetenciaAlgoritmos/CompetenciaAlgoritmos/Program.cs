using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

// Por Patricio López Juri (pelopez2@uc.cl)

namespace CompetenciaAlgoritmos
{
    // Palíndromo: http://es.wikipedia.org/wiki/Pal%C3%ADndromo

    class Program
    {
        static void Main(string[] args)
        {
            Profesor profesor = new Profesor();
            String input = "ABCBAHELLOHOWRACECARAREYOUILOVEUEVOLIIAMAIDOINGGOOD";
            String outputEsperado = "ILOVEUEVOLI";
            Alumno ganador = profesor.AlgoritmoGanador(input, outputEsperado);
            Console.WriteLine("El ganador es: " +ganador);
            Console.ReadKey(true);
        }
    }

}
