using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace MvcCadeteria.ViewsModels
{
    public class InUsuario
    {
        [Required]
        [StringLength(60)]
        [Display(Name="Nombre Usuario")]
        public string? nom_u { get; set;}

        [Required]
        [StringLength(16)]
        [Display(Name="Contraseña")]
        public string? pass_u { get; set;}
    }
}