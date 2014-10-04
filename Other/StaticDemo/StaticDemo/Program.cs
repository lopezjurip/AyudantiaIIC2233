using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

// Patricio López
namespace StaticDemo
{
    class Program
    {
        private static Random random = new Random();
        private static String[] nombres = new String[] {
            "Pedro",
            "Pablo",
            "Juan",
            "Raul",
            "Patricio"
        };

        static void Main(string[] args)
        {
            Console.WriteLine("Presione ESC para salir.");
            Console.WriteLine("Presione cualquier tecla para incribir a una persona.");

            for (ConsoleKeyInfo tecla = Console.ReadKey(true); tecla.Key != ConsoleKey.Escape; tecla = Console.ReadKey(true))
            {
                String nombre = NombreAleatorio();
                Persona persona = Persona.InscribirPersona(nombre);
                if (persona != null)
                {
                    Console.WriteLine("Se ha creado a: " + persona.Nombre + " [" + persona.RUT + "]");
                }
                else
                {
                    Console.WriteLine("Se ha sobrepasado el límite de RUTs disponibles (" + Persona.NUMERO_MAXIMO_PERSONAS + ")");
                }
            }
        }

        private static String NombreAleatorio()
        {
            return nombres[random.Next(0, nombres.Length)];
        }
    }
}
