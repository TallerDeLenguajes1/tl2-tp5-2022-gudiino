using System;
using System.ComponentModel.DataAnnotations;

namespace MvcCadeteria.ViewsModels
{
    public class EditarPd2ViewModel
    {
        //datos del pedido
        [Required]
        public int id_pd2 { get; set;}

        [Required]
        [StringLength(100)]
        [Display(Name="Detalle Pedido")]
        public string? obs { get; set;}

        public int estado { get; set;}

        //** para el cliente
        [Required]
        public int id_cli { get; set;}

        [Required]
        [StringLength(100)]
        [Display(Name="Nombre Cliente")]
        public string? cli_nom { get; set;}

        [Required]
        [StringLength(100)]
        [Display(Name="Domicilio")]
        public string? Direccion { get; set;}

        [Required]
        [Display(Name="Teléfono")]
        [Phone]
        public string? Telefono { get; set;}

        [Required]
        [StringLength(100)]
        [Display(Name="Detalle Direccion")]
        public string? detalle_direccion { get; set;}

        //** para el cadete
        [Required]
        public int id_cdt { get; set;}
        
        [Required]
        [StringLength(100)]
        [Display(Name="Nombre Cadete")]
        public string? cdt_nom { get; set;}
    }
}