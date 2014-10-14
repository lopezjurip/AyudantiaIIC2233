using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlarmaConFunc
{

    /* Cuando suene una alarma (llamar método Sonar()) en consola se verá:
     * RING!
     * Solucionando problema en _____
     * Yay!
     */

    /// <summary>
    /// En algún momento sonará.
    /// </summary>
    class Alarma
    {
        public event Func<String, int, bool> Alerta;

        private String ID; // Identificador, como un RUT.

        public void Sonar()
        {
            Random random = new Random();
            int tiempoSonando = random.Next(0, 100); // Da igual, que sea random.

            Console.WriteLine("RING!");
            if (Alerta != null) // Vemos si hay suscriptores.
            {
                bool solucionada = Alerta(ID, tiempoSonando); //Retorna un bool, ver la firma del Func<..., ..., bool>
                if(solucionada)
                    Console.WriteLine("Yay!");
                else
                    Console.WriteLine("RING RING!");
            }
        }
    }

    class Campus
    {
        protected Alarma[] alarmas;

        public void Suscribirse()
        {
            foreach (Alarma a in alarmas)
                a.Alerta += Solucionar;
        }

        public bool Solucionar(String ID, int tiempo) // Respeta la firma del evento. Ojo, retorna bool
        {
            // Solucionamos todos los problemas :)
            Console.WriteLine("Solucionando problema en " +ID);
            return true;
        }
    }
}
