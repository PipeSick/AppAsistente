using System;
using System.Collections.Generic;
using System.Text;

namespace AppAsistente.Models
{
    public class UserModel
    {

        public string Id { get; set; }

        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string Correo { get; set; }
        public string Password { get; set; }
        public string VehiculoId { get; set; }
        public string AlumnoRut { get; set; }

        public static implicit operator List<object>(UserModel v)
        {
            throw new NotImplementedException();
        }
    }
}
