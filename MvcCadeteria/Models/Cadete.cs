using System;
namespace MvcCadeteria.Models {
    public class Cadete {
        private int id_cadete {get; set;}
        private string nombre {get; set;}
        private string direccion {get; set;}
        private string telefono {get; set;}
        private int id_cadeteria {get; set;}

        public int cdt_id { get => id_cadete; set => id_cadete = value; }
        public string cdt_nombre{ get => nombre; set => nombre = value;}
        public string cdt_domicilio {get => direccion; set => direccion = value;}
        public string cdt_telefono {get => telefono; set => telefono = value;}
        public int cdt_id_sucursal {get => id_cadeteria; set => id_cadeteria = value;}

        // public Cadete(int id, string nom, string dir, string tel, int cdteria){
        //     this.id_cadete = id;
        //     this.nombre = nom;
        //     this.direccion = dir;
        //     this.telefono = tel;
        //     this.id_cadeteria = cdteria;
        // }
        
        // public float jornalAcobrar(){
        //     return CantidadPedidos()*Cadeteria.pago_x_entrega;
        // }
        // public int CantidadPedidos(){
        //     int cont=0;
        //     foreach (var pd2 in pedidos!)
        //     {
        //         if(pd2.getEstado()==2){//estado 2, entregado
        //             cont++;
        //         }
        //     }
        //     return cont;
        // }
        // public void agregarPedido(Pedido nuevo){
        //     pedidos!.Add(nuevo);
        // }
        // public List<Pedido> getPedidos(){
        //     return pedidos!;
        // }
        // public void eliminarPedido(Pedido item){
        //     pedidos!.Remove(item);
        // }
    }
}