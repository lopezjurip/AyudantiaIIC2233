using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompetenciaAlgoritmos
{

    /// <summary>
    /// Delegate que usaremos.
    /// </summary>
    /// <param name="input">String muy largo que contiene palíndromos.</param>
    /// <returns>El palíndromo más largo.</returns>
    public delegate String AlgoritmoPalindromo(String input);

    /// <summary>
    /// Clase alumno, las subclases tienen que implementar su propia algoritmo.
    /// </summary>
    public abstract class Alumno
    {
        /// <summary>
        /// Debe retornar el algoritmo propuesto por el alumno. 
        /// </summary>
        /// <returns></returns>
        public AlgoritmoPalindromo EntregarMiAlgoritmo()
        {
            return MiAlgoritmo;
        }

        /// <summary>
        /// Algoritmo propuesto por el alumno.
        /// </summary>
        /// <param name="s">String muy largo que contiene palíndromos.</param>
        /// <returns>El palíndromo más largo.</returns>
        protected abstract String MiAlgoritmo(String s);
    }
}
