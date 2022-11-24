using System;
using System.ComponentModel.DataAnnotations;

namespace MvcCadeteria.ViewModels
{
    public class EditarCdtViewModel
    {
        [Required]
        public int id { get; set;}
        
        [Required]
        [StringLength(100)]
        [Display(Name="Nombre Cadete")]
        public string? nombre { get; set;}

        [Required]
        [StringLength(100)]
        [Display(Name="Calle")]
        public string? calle { get; set;}

        [Required]
        public int numero { get; set;}

        [Phone]
        public string? telefono { get; set;}
        
    }
}