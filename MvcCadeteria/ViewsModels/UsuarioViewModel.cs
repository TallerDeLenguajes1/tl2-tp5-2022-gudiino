using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace MvcCadeteria.ViewsModels
{
    public class UsuarioViewModel
    {
        [Required]
        [StringLength(50)]
        [Display(Name="Nombre Usuario")]
        public string? nom_u { get; set;}

        [Required]
        [StringLength(16)]
        [DataType(DataType.Password)]
        [Display(Name="Contraseña")]
        public string? pass_u { get; set;}
    }
}