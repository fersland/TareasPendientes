using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Datos.DTO
{
    public class UsuarioDTO
    {
        public string UserName { get; set; }

        public string Passw { get; set; }

        public string Department { get; set; }
    }
}
