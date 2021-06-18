using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API_JWT_NETCORE.Models
{
    public class Rol
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public int Codigo { get; set; }
        public bool Activo { get; set; }
    }

    public enum Roles
    {
        Cliente = 100,
        Empleado = 200,
        Administrador = 300
    }
}
