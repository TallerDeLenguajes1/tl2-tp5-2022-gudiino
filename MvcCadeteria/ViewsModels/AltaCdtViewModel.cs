using System;
using System.IO;
using System.ComponentModel.DataAnnotations;

namespace MvcCadeteria.ViewsModels
{
    public class AltaCdtViewModel
    {
        [Required]
        [StringLength(100)]
        [Display(Name="Nombre Cadete")]
        public string? Nombre { get; set;}

        [Required]
        [StringLength(100)]
        [Display(Name="Domicilio")]
        public string? Direccion { get; set;}

        [Phone]
        public string? Telefono { get; set;}

        [Required]
        public int Sucursal { get; set;}
        
    }
}