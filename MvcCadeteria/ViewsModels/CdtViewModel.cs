
using MvcCadeteria.Models;

public class CdtViewModel
{
    public string? id { get; set; }
 
    public string? nombre { get; set; }
 
    public string? domicilio { get; set; }

    public string? telefono { get; set; }

    public List<Pedido> lista_pd2 {get; set;}
}