using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logica
{
    // Necesitamos una clase abstracta que tenga los campos: Tipo y Color.
    // Además, un método abstracto validador de movimientos que reciba 2 coordenadas y devuelva un bool.
              
    #region Solución clase abstracta
    public abstract class PiezaAjedrez
    {
        public const string REY_NEGRO           = "♛";
        public const string CABALLO_NEGRO       = "♞";
        public const string REINA_NEGRA         = "♚";
        public const string PEON_NEGRO          = "♟";
        public const string TORRE_NEGRA         = "♜";
        public const string ALFIL_NEGRO         = "♝";

        public const string REY_BLANCO          = "♕";
        public const string CABALLO_BLANCO      = "♘";
        public const string REINA_BLANCA        = "♔";
        public const string PEON_BLANCO         = "♙";
        public const string TORRE_BLANCA        = "♖";
        public const string ALFIL_BLANCO        = "♗";

        public string Tipo { get; protected set; }
        public Color Color { get; protected set; }
        public Coordenada CoordenadaActual { get; internal set; }

        public PiezaAjedrez(String tipo, Color color, Coordenada inicio)
        {
            this.Tipo = tipo;
            this.Color = color;
            this.CoordenadaActual = inicio;
        }

        public abstract bool Movimiento(Coordenada coordenadaNueva);
        public abstract string Icono { get; }

    }
    #endregion
}
