using System;
namespace MvcCadeteria.Models {
    public class Pedido{
        protected int IDpedido {get; set;}
        protected string? detalle {get; set;}
        protected Cliente? cliente {get; set;}
        public enum Estados{EnCurso,Asignado,Entregado,Cancelado,Extraviado};
        protected int estadoPedido {get; set;}
        public Pedido(int num, string obs, int estado, Cliente cl){
            IDpedido=num;
            detalle=obs;
            estadoPedido=estado;
            cliente= new Cliente(cl);
        }
        // public void listar_info_pedido(){
        //     Console.WriteLine("DATOS PEDIDO");
        //     Console.WriteLine("Numero de pedido: {0}",IDpedido);
        //     Console.WriteLine("Observaciones: {0}",detalle);
        //     Console.WriteLine("Estado entrega: {0}",Enum.GetName(typeof(Estados),estadoPedido));
        //     Console.WriteLine();
        //     Console.WriteLine("DATOS CLIENTE");
        //     cliente!.listar_info_cliente();
        //     Console.WriteLine();
        // }
        // public void setEstado(){
        //     Console.WriteLine("Seleccione nuevo estado pedido");
        //     foreach(int i in Enum.GetValues(typeof(Pedido.Estados)))
        //     Console.WriteLine("{0} --> {1}",i,Enum.GetName(typeof(Pedido.Estados),i));
        //     Console.Write("Seleccion: ");
        //     int estado=Convert.ToInt32(Console.ReadLine());
        //     Console.WriteLine();
        //     estadoPedido=estado;
        //     listar_info_pedido();
        // }
        // public int getIdPedi2(){
        //     return IDpedido;
        // }
        // public int getEstado(){
        //     return estadoPedido;
        // }
    }
    
}