namespace Feudo {
    public abstract class Persona {
        protected String nombre;
        private bool vivo;

        public Persona(String nombre) {
            this.nombre = nombre;
            this.vivo = true;
        }

        public abstract void Vivir();

        public virtual void Morir() {
            vivo = false;
        }

        public String Nombre {
            get { return nombre; }
        }
    }

    abstract class Noble : Persona {
        protected Persona[] servidores;
        // protected IServidor[] servidores;

        public Noble(String nombre) : base(nombre)  {
            // Constructor
        }

        private void UsarLujos() {
            // … 
        }
    }

    class Rey : Noble {
        public Rey() : base("EL REY DEL FLOW") {
            servidores = new Vasallo[4];
            for (int i = 0; i < servidores.Length; i++) {
                Vasallo vasallo = new Vasallo("Esclavo " + i);
                vasallo.Servir(this);
                servidores[i] = vasallo;
            }
        }

        public override void Morir() {
            // base.Morir();
        }

        public override void Vivir() {
            // YOLO   
        }
    }

    class Vasallo : Persona, IServidor {
        public Vasallo(String nombre) : base(nombre) {

        }

        public override void Vivir() {

        }

        public void Servir(Rey rey) {
            // ...
        }
    }

    class VasalloRebelde : Vasallo {
        public VasalloRebelde(String nombre) : base(nombre + " rebelde") {
            // ...
        }

        public new void Vivir()  {

        }

        public new void Servir(Rey rey) {
            // Ignorar rey
            rey.Morir();
        }
    }

    class Castillo{
        private List<Persona> habitantes;

        public void RecibirVisitante(Persona persona){
            if (persona is Rey){
                Rey rey = persona as Rey;
                Console.WriteLine("Ha llegado el rey " + rey.Nombre);
                foreach (Persona p in habitantes){
                    p.Morir();
                }
            }
            habitantes.Add(persona);
        }
    }

    interface IServidor {
        void Servir(Rey rey);
    }

    interface IEsclavizable : IServidor {
        void SerEsclavizadoPor(Persona amo);
    }
}
