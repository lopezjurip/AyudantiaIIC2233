using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Torneo_de_Artes_Marciales
{
    abstract class Saiyajin: Personaje
    {
        protected bool isSuperSaiyajin; // Los Saiyajines pueden transformarse en SuperSaiyajines

        public Saiyajin(String Nombre)
            : base(Nombre)
        {
            isSuperSaiyajin = false;
        }

        public bool IntentarTransformarEnSuperSaiyajin()
        {
            if (ki >= 4000)
            {
                Console.WriteLine(Nombre +" ahora es SuperSaiyajin!");
                isSuperSaiyajin = true;
                hp += 200;
            }
            else
                ki += 500;

            return isSuperSaiyajin;
        }

        public int Kakamehameha()
        {
            // Si tiene poco Ki, solo ejecuta un ataque normal
            if (ki < 1000)
                return Fuerza;

            int BonusDelKi = Program.GeneradorNumerosRandom.Next(100, ki);

            ki = Math.Max(0, ki - 1000); // Con esto nos aseguramos que el Ki no sea negativo.
            Console.WriteLine(Nombre +" usa Kakamehameha!");
            return BonusDelKi + Fuerza;
        }
    }
}
