using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Torneo_de_Artes_Marciales
{
    class Humano : Personaje
    {
        public Humano(String Nombre)
            : base(Nombre)
        {
            hp = 100;
            ki = 0;
        }

        public override bool AtacarYMatar(Personaje p)
        {
            SanarHeridas();
            return p.RecibirDanoYMuere(Fuerza);
        }

        private void SanarHeridas()
        {
            if (hp >= 90)
                return;
            hp += 10;
            Console.WriteLine(Nombre +" sana sus heridas y ahora tiene [" +hp +"]");
        }
    }
}
