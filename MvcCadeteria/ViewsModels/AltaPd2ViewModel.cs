using System;
using System.ComponentModel.DataAnnotations;

namespace MvcCadeteria.ViewModels
{
    public class AltaPd2ViewModel
    {//para el pedido
        [Required]
        [StringLength(100)]
        [Display(Name="Detalle Pedido")]
        public string? detalle_pedido { get; set;}

        [Required]
        [StringLength(10)]
        public string? estado_pedido { get; set;}
        //** para el cliente
        [Required]
        [StringLength(100)]
        [Display(Name="Nombre Cliente")]
        public string? nombre_cliente { get; set;}

        [Required]
        [StringLength(100)]
        [Display(Name="Calle")]
        public string? Direccion { get; set;}

        [Required]
        public int Numero { get; set;}

        [Phone]
        public string? Telefono { get; set;}

        [Required]
        [StringLength(100)]
        [Display(Name="Detalle Direccion")]
        public string? detalle_direccion { get; set;}
        
    }
}