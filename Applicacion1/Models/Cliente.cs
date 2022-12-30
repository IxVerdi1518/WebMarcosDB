
namespace Applicacion1.Models
{
    public class Cliente
    {
        private string identificacion;
        private string nombres;
        private string apellidos;
        private string cedula;

        public Cliente()
        {
        }

        public Cliente(string identificacion, string nombres, string apellidos, string cedula)
        {
            this.identificacion = identificacion;
            this.nombres = nombres;
            this.apellidos = apellidos;
            this.cedula = cedula;
        }

        public string Nombres { get => nombres; set => nombres = value; }
        public string Apellidos { get => apellidos; set => apellidos = value; }
        public string Cedula { get => cedula; set => cedula = value; }

        public string Identificacion { get => identificacion; set => identificacion = value; }



    }
}

