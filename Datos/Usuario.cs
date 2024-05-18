using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Datos
{
    public class Usuario
    {        
        public int Id { get; set; }

        [DisplayName("Nombre Usuario")]
        [Required(ErrorMessage = "El nombre de usuario es obligatorio")]
        public string UserName { get; set; }

        [DisplayName("Contraseña")]
        [Required(ErrorMessage = "La clave es obligatorio")]
        public string Passw { get; set; }

        [DisplayName("Nombre Departamento")]
        public string Department { get; set; }
    }
}
