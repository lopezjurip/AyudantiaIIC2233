using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlarmaConEvento
{
    /* Cuando suene una alarma (llamar método Sonar()) en consola se verá:
     * RING!
     * AHHHHHH!
     * RING RING!
     */

    /// <summary>
    /// En algún momento sonará.
    /// </summary>
    class Alarma
    {
        public event Action Alerta;

        public void Sonar()
        {
            Console.WriteLine("RING!");
            if (Alerta != null) // Vemos si hay suscriptores.
                Alerta();
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
                a.Alerta += HaSonado; // seguridad.HaSonado; también podría cualquier método que respetara la firma.
        }

        public void HaSonado() // Respeta la firma del evento :)
        {
            Console.WriteLine("AHHHHHH!");
        }
    }

    /// <summary>
    /// Clase cualquiera que también puede enterarse de la alarma.
    /// </summary>
    class Seguridad
    {
        public void HaSonado()
        {
            Console.WriteLine("ALERTA!");
        }
    }
}
