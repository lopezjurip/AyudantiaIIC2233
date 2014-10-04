using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StaticDemo
{
    class Persona
    {
        public const long NUMERO_MAXIMO_PERSONAS = 10000050;
        private static long ultimo_rut_inscrito = 10000000;

        protected String nombre, rut;

        public static Persona InscribirPersona(String nombre)
        {
            if (ultimo_rut_inscrito < NUMERO_MAXIMO_PERSONAS)
            {
                long nuevoRut = ++ultimo_rut_inscrito;
                String rut = nuevoRut + "-" + DigitoVerificador(nuevoRut);
                return new Persona(nombre, rut);
            }
            else
            {
                return null;
            }
        }

        // Constructor privado
        private Persona(String nombre, String rut)
        {
            this.nombre = nombre;
            this.rut = rut;
        }

        public String RUT
        {
            get { return rut; }
        }

        public String Nombre
        {
            get { return nombre; }
        }

        /// <summary>
        /// Fuente:
        /// http://social.msdn.microsoft.com/Forums/es-ES/7065cbde-b5ef-4860-a012-bf2d6bc5288b/digito-verificador?forum=vcses
        /// </summary>
        /// <param name="rut">Rut en formato número</param>
        /// <returns>El dígito verificador correspondiente.</returns>
        private static String DigitoVerificador(long rut)
        {
            long Digito;
            long Contador;
            long Multiplo;
            long Acumulador;
            String RutDigito;

            Contador = 2;
            Acumulador = 0;

            while (rut != 0)
            {
                Multiplo = (rut % 10) * Contador;
                Acumulador = Acumulador + Multiplo;
                rut = rut / 10;
                Contador = Contador + 1;
                if (Contador == 8)
                {
                    Contador = 2;
                }
            }

            Digito = 11 - (Acumulador % 11);
            RutDigito = Digito.ToString().Trim();
            if (Digito == 10)
            {
                RutDigito = "K";
            }
            if (Digito == 11)
            {
                RutDigito = "0";
            }
            return (RutDigito);
        }
    }
}
