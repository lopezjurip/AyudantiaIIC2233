using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlertaConEvento2
{
    /* Cuando suene una alarma (llamar método Sonar()) en consola se verá:
     * RING!
     * AHHHHHH!
     * ALERTA! _____ t: ____);
     * RING RING!
     */

    /// <summary>
    /// En algún momento sonará.
    /// </summary>
    class Alarma
    {
        public event Action<String, int> Alerta;

        private String ID; // Identificador, como un RUT.

        public void Sonar()
        {
            Random random = new Random();
            int tiempoSonando = random.Next(0, 100); // Da igual, que sea random.

            Console.WriteLine("RING!");
            if (Alerta != null) // Vemos si hay suscriptores.
                Alerta(ID, tiempoSonando);
            Console.WriteLine("RING RING!");
        }
    }

    class Campus
    {
        protected Alarma[] alarmas;
        protected Seguridad seguridad;

        public void Suscribirse()
        {
            foreach (Alarma a in alarmas)
            {
                a.Alerta += HaSonado; // seguridad.HaSonado; también podría cualquier método que respetara la firma.
                a.Alerta += seguridad.HaSonado;
            }
        }

        public void HaSonado(String ID, int tiempo) // Respeta la firma del evento :)
        {
            Console.WriteLine("AHHHHHH!");
        }
    }

    /// <summary>
    /// Clase cualquiera que también puede enterarse de la alarma.
    /// </summary>
    class Seguridad
    {
        public void HaSonado(String ID, int tiempo)
        {
            Console.WriteLine("ALERTA! " + ID + " t: " + tiempo);
        }
    }
}
