namespace PlantillaPais
{
    public class RegistroPaises
    {
        // Campos
        private string nombre;
        private string continente;
        private short codigoInternacional;
        private string poblacion;
        private float superficie;

        // Propiedades
        public string Nombre
        {
            get { return nombre; }
            set { nombre = value; }
        }

        public string Continente
        {
            get { return continente; }
            set { continente = value; }
        }

        public short CodigoInternacional
        {
            get { return codigoInternacional; }
            set { codigoInternacional = value;}
        }

        public string Poblacion
        {
            get { return poblacion; }
            set { poblacion = value; }
        }

        public float Superficie
        {
            get { return superficie; }
            set { superficie = value; }
        }
    }
}