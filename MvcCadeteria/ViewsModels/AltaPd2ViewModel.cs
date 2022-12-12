using System;
using System.ComponentModel.DataAnnotations;

namespace MvcCadeteria.ViewsModels
{
    public class AltaPd2ViewModel
    {//para el pedido
        [Required]
        [StringLength(100)]
        [Display(Name="Detalle Pedido")]
        public string? detalle_pedido { get; set;}

        [Required]
        [Display(Name="Estado Inicial Pedido")]
        public string estado_pedido { get; set;}
        //** para el cliente
        [Required]
        [StringLength(100)]
        [Display(Name="Nombre Cliente")]
        public string? nombre_cliente { get; set;}

        [Required]
        [StringLength(100)]
        [Display(Name="Domicilio")]
        public string? Direccion { get; set;}

        [Required]
        [Phone]
        public string? Telefono { get; set;}

        [Required]
        [StringLength(100)]
        [Display(Name="Detalle Direccion")]
        public string? detalle_direccion { get; set;}
        
    }
}