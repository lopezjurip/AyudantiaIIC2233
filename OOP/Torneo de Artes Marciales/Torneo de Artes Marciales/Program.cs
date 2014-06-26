using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Torneo_de_Artes_Marciales
{
    class Program
    {
        public static Random GeneradorNumerosRandom = new Random();

        static void Main(string[] args)
        {
            /////
            // Concursantes Humanos
            // Hacemos un arreglo con los nombres de los humanos solo por comodidad.
            String[] NombresHumanos = {   "Miguel",
                                          "Adrián",
                                          "Pietro",
                                          "Vicho", 
                                          "María",
                                          "Iván",
                                          "Patricio",
                                          "Carlos",
                                          "Belén",
                                          "Jaime" };

            // Generamos los humanos instanciando a cada uno en una posición del arreglo Humanos
            Humano[] Humanos = new Humano[NombresHumanos.Length];
            for (int i = 0; i < NombresHumanos.Length; i++)
                Humanos[i] = new Humano(NombresHumanos[i]); // Para instanciar un humano solo basta un nombre

            /////
            // Concursantes malvados
            iMalvado[] Villanos = new iMalvado[2];
            Villanos[0] = new Freezer(); // Creamos a Freezer directamente dentro del array

            Cell cell = new Cell();
            Villanos[1] = cell; // O podemos crearlo fuera y luego agregarlo.

            /////
            // Concursantes Saiyajines
            Saiyajin[] Saiyajines = { new Goku(), new Vegeta() };

            //////
            // Juntamos todos los concursantes dentro de una lista de Personajes
            // Esto es posible gracias a que todos estos heredan de Personaje.
            List<Personaje> ListaDeConcursantes = new List<Personaje>();
            foreach (Personaje p in Humanos) // Esto Significa, a cada Personaje dentro de Humanos, añádelo a la lista.
                ListaDeConcursantes.Add(p);
            foreach (Personaje p in Villanos)
                ListaDeConcursantes.Add(p);
            foreach (Personaje p in Saiyajines)
                ListaDeConcursantes.Add(p);
            // Una lista es como un arreglo pero donde no importan las posiciones de sus componentes. 
            // Aprenderás más de esto en el capítulo Estructura de Datos.

            // Si queremos crear humanos para aumentar la cantidad de concursantes, cambia el siguiente valor por alguno mayor a 0.
            int CrearHumanosDeRelleno = 0; 
            for (int i = 0; i < CrearHumanosDeRelleno; i++)
                ListaDeConcursantes.Add(new Humano("Humano " + i));

            // Tambien se pueden crear multiples heroes así como cualquier cosa que te le ocurra
            int CantidadDeGokusAdicionales = 0;
            for (int i = 0; i < CantidadDeGokusAdicionales; i++)
                ListaDeConcursantes.Add(new Goku());

            ListaDeConcursantes.Add(new Yamcha());
            TorneoMundial.IniciarCombate(ListaDeConcursantes);
            Console.WriteLine("-----------------------------------------");
            Console.WriteLine("Presiona Enter para salir");
            Console.ReadLine();
        }

        // Solo para mostrar cómo se crean y se usan las clases estáticas, el torneo será Estático.
        // Esto quiere decir, que no requiere ser instanciado y puede ser accesible por todo el Assembly si es que es público.
        // Por seguridad lo haremos privado. Si quieres puedes cambiarlo a "public static class TorneoMundial" 
        // y escribir en cualquier otra clase "Program.TorneoMundial.SoyUnMetodoEstático();"
        private static class TorneoMundial
        {
            // En una clase estática no se pueden declarar instancias de otras clases NO-estáticas.
            // Personaje[] ArregloDePersonajes; // Descomenta está linea y tendrás un error.

            // El siguente método debe ser estático.
            public static void IniciarCombate(List<Personaje> Concursantes)
            {
                Console.WriteLine("Comienza el torneo!");
                Personaje GanadorDelTorneo = null;
                do // Hacemos un Do-While, que repetirá lo que hay dentro de do{} hasta que deje de cumplirse lo escrito en while(<condición>).
                {
                    /* Elegimos un Personaje al azar de los participantes, lo removemos de la lista y elegimos a otro personaje.
                     * Volemos a agregar al primer personaje a la lista. Esto es para evitar que se elija al mismo 2 veces.
                     */
                    Personaje Atacante = Concursantes[GeneradorNumerosRandom.Next(0, Concursantes.Count)];
                    Concursantes.Remove(Atacante);
                    Personaje Atacado = Concursantes[GeneradorNumerosRandom.Next(0, Concursantes.Count)];
                    Concursantes.Add(Atacante);

                    // El bool es para indicar si muere el atacado.
                    // Cada clase implementa su propio método de ataque. 
                    // Gracias a la herencia y polimorfismo podemos hacer esto. En caso contrario este método estaría lleno de if's asquerosos.
                    // 
                    // Entonces, por ejemplo, si le toca atacar a Cell, este tiene la probabildiad de Absorver a un humano y matarlo de inmediato.
                    // Los Sayajines por ejemplo, tienen la probabiidad de convertirse en SuperSaiyajines y mejorar sus ataques.
                    // El humano está condenado a usar ataques simples.
                    Console.WriteLine(Atacante.Nombre +" ataca a " +Atacado.Nombre);

                    bool MuereElAtacado;
                    if (Atacante is Yamcha)
                    {
                        if (GeneradorNumerosRandom.Next(0, 100) < 50)
                            MuereElAtacado = ((Humano)Atacante).AtacarYMatar(Atacado); // Yamcha ataca como un humano normal
                        else
                            MuereElAtacado = ((Yamcha)Atacante).AtacarYMatar(Atacado); // Yamcha ataca con su propio ataque.
                    }
                    else
                        MuereElAtacado = Atacante.AtacarYMatar(Atacado);

                    if (MuereElAtacado == true)
                    {
                        Console.WriteLine(Atacante.Nombre + " a vencido a " + Atacado.Nombre +"!");
                        Concursantes.Remove(Atacado);
                        if (Concursantes.Count == 1 && Concursantes[0] == Atacante) // Si queda un solo Personaje, este es el ganador.
                            GanadorDelTorneo = Atacante; // Con esto se detendrá el do-while.
                    }
                    Console.WriteLine("-------------------------------------");

                    // Remover esto para que sea automático el scroll.
                    //Console.ReadKey(true);

                    // Aquí justo al final del do{} se verifica si debe seguir la iteración, 
                    // si GanadorDelTorneo deja de ser nulo entonces termina de repetirse el do{}.
                } 
                while (GanadorDelTorneo == null);

                if (GanadorDelTorneo is iMalvado) // Con esto podemos saber si una clase implementa o herada de otra.
                {
                    // Debemos castear el Personaje a uno que implemente iMalvado y por consiguiente que tenga el método DestruirElMundo().
                    // Si intentas hacer esto no se podrá, descoméntalo para probar.
                    //
                    // GanadorDelTorneo.DestuirElMundo();
                    //
                    // Si no te quedó claro esto también es válido:
                    /*
                        iMalvado SujetoMalvado = (iMalvado)GanadorDelTorneo;
                        SujetoMalvado.DestuirElMundo();
                    */
                    // Notarás que NO estamos instanciando una interfaz, solo estamos diciendo que SujetoMalvado implementa esa interfaz.
                    // Por ejemplo, no se puede hacer esto:
                    //  iMalvado SujetoMalvadoQueNoFuncionara = new iMalvado();

                    ((iMalvado)GanadorDelTorneo).DestuirElMundo();
                }
                else
                {
                    Console.WriteLine("El ganador del torneo es: " + GanadorDelTorneo.Nombre);
                }
            }

            public static void SoyUnMetodoEstático()
            {
                Console.WriteLine("hola");
            }
        }
    }
}
