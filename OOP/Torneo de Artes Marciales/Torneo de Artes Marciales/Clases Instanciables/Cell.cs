using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Torneo_de_Artes_Marciales
{
    class Cell: Personaje, iMalvado
    {
        // Cell almacenará las personas que absorba dentro de una lista.
        // Estos mueren inmediatamente al ser absorvidos y a Cell se le suma la vida de sus víctimas.
        private List<Humano> HumanosAbsorvidos;

        public Cell()
            : base("Cell")
        {
            HumanosAbsorvidos = new List<Humano>();
            hp = 1300;
            ki = Program.GeneradorNumerosRandom.Next(0, 100);
        }

        public override bool AtacarYMatar(Personaje p)
        {
            if (p is Humano) // Verificamos si es Persona para ver si podemos absorverla.
            { 
                if (AbsorverHumano((Humano)p) == true) // Intentamos Absorverla, si se logra, se retorna un TRUE que significa que muere de inmediato.
                    return true; // Es importante hacer el casteo' de Persona a Humano. También es importante el primer (if) porque sino es un Humano, el programa se cae.
                else
                    Console.WriteLine("Lo ataca de todos modos");
            }
            int PoderDelAtaque = Fuerza + HumanosAbsorvidos.Count * 10;
            return p.RecibirDanoYMuere(PoderDelAtaque);
        }

        public bool estaVivo()
        {
            return false;
        }

        public bool AbsorverHumano(Humano h)
        {
            bool fueAbsorvido = false;
            // Probabilidad de absorver a un humano depende del ki de Cell
            if (Program.GeneradorNumerosRandom.Next(0,100) < ki)
            {
                Console.WriteLine("Oh no! " + Nombre + " ha absorvido a " + h.Nombre + " y aumentó su vida en " + h.HP);
                HumanosAbsorvidos.Add(h);   // Lo añadimos a nuestra lista.
                hp += h.HP;                 // Le robamos toda su vida.
                h.MorirDeInmediato();       // la victima muere
                ki += 30;
                fueAbsorvido = true;
            }
            else // Fue un fracaso el ataque y perjudica a Cell.
            {
                Console.WriteLine(
                    Nombre + " no pudo absorver a " + h.Nombre + " y ha recibido daño por intentarlo");
                hp -= 200;
                ki = Math.Max(ki - 10, 10); // Cell puede tener un minimo de 10 de Ki
                fueAbsorvido = false;
            }
            return fueAbsorvido;
        }

        public void DestuirElMundo()
        {
            Console.WriteLine("Cell conquistó el planeta tierra y mató a todos sus habitantes!");
        }
    }
}
