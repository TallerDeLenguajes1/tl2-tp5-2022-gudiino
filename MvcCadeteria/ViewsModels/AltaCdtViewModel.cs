using System;
using System.ComponentModel.DataAnnotations;

namespace MvcCadeteria.ViewModels
{
    public class AltaCdtViewModel
    {
        [Required]
        [StringLength(100)]
        [Display(Name="Nombre Cadete")]
        public string? NombreCadete { get; set;}

        [Required]
        [StringLength(100)]
        [Display(Name="Calle")]
        public string? Direccion { get; set;}

        [Required]
        public int Numero { get; set;}

        [Phone]
        public string? Telefono { get; set;}
        
    }
}