using System;
namespace MvcCadeteria.Models {
    public class Cliente:Persona {
        private string detalle_direccion {get; set;}//sobre la ubicacion de la casa
        public Cliente(int iden, string nom, string dir,int num, string tel, string dirREf):base(iden, nom, dir, num, tel){
            detalle_direccion=dirREf;
        }
        public Cliente(Cliente cl):base(cl.id, cl.nombre, cl.calle, cl.numero, cl.telefono){
            detalle_direccion=cl.detalle_direccion;
        }
        // public void listar_info_cliente(){
        //     Console.WriteLine("Id     |Nombre      |Calle              |Numero       |Telefono");
        //     listar_info_persona();
        //     Console.WriteLine("Detalle Domicilio: {0}",detalle_direccion);
        // }
    }
}