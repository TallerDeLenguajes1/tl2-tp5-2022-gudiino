using System;
namespace MvcCadeteria.Models {
    public class Cadete:Persona {
        private List<Pedido>? pedidos {get; set;}
        public Cadete(int iden, string nom, string dir,int num, string tel):base(iden, nom, dir, num, tel){
            pedidos=new List<Pedido>();
        }
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