using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Torneo_de_Artes_Marciales
{
    abstract class Personaje // Esta clase es abstracta, no se puede instanciar directamente.
    {
        protected String nombre;
        protected int hp;
        protected int ki;
        protected int Fuerza;

        // Properties
        // Es una buena práctica.
        public String Nombre { 
            get { return nombre; } 
            set { nombre = value; } }

        public int KI { 
            get { return ki; } }

        public int HP { 
            get { return hp; } }

        // Este es el constructor.
        public Personaje(String Nombre)
        {
            nombre = Nombre;
            // Esto se pudo haber hecho de otra manera pero lo hice ahí para mostrar como comparar clases por herencia.
            if(this is Saiyajin)
                Fuerza = Program.GeneradorNumerosRandom.Next(0, 200);
            else if (this is iMalvado)
                Fuerza = Program.GeneradorNumerosRandom.Next(0, 300);
            else
                Fuerza = Program.GeneradorNumerosRandom.Next(0, 80);
        }

        // Este es un método abstracto que cada clase que herede de esta debe implementar a su manera.
        public abstract bool AtacarYMatar(Personaje p);

        public virtual bool RecibirDanoYMuere(int Dano)
        {
            Console.WriteLine(Nombre +" [" +hp +"] recibe " +Dano +" y queda en [" +(hp-Dano)+"]");
            hp -= Dano;
            if (hp <= 0)
                return true;
            return false;
        }

        public void MorirDeInmediato()
        {
            hp = 0;
        }
    }
}
