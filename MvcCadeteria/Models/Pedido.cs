using System;
namespace MvcCadeteria.Models {
    public class Pedido{
        private int id {get; set;}
        private string obs_pedido {get; set;}
        private Cliente cliente {get; set;}
        //public Cadete cadete {get; set;}
        //public enum Estados{EnCurso,Asignado,Entregado,Cancelado,Extraviado};
        private string estado {get; set;}
        //metodos de acceso a propiedades
        public int id_pedido { get => id; set => id = value; }
        public string detalle_pedido { get => obs_pedido; set => obs_pedido=value;}
        public string estado_pedido {get => estado; set => estado=value;}
        public Cliente cliente_pedido {get => cliente; set => cliente= new Cliente(value);}

        public Pedido(int num, string obs, string estado, Cliente cl){
            id=num;
            obs_pedido=obs;
            this.estado=estado;
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