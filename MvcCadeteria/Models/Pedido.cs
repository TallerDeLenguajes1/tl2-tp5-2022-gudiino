using System;
namespace MvcCadeteria.Models {
    public class Pedido{
        private int id_pedido {get; set;}
        private string obs_pedido {get; set;}
        private int id_cliente {get; set;}
        private int id_cadete {get; set;}
        public enum Estados{EnCurso,Asignado,Entregado,Cancelado};
        private int estado {get; set;}
        private Cliente cli;
        private Cadete cdt;
        //metodos de acceso a propiedades
        public int id_pd2 { get => id_pedido; set => id_pedido = value; }
        public string detalle_pedido { get => obs_pedido; set => obs_pedido=value;}
        public int id_cli { get => id_cliente; set => id_cliente = value; }
        public int id_cdt { get => id_cadete; set => id_cadete = value; }
        public int estado_pedido {get => estado; set => estado=value;}
        public Cliente cli_pd2 {get => cli; set => cli=value;}
        public Cadete cdt_pd2 {get => cdt; set => cdt=value;}
        
        // public Pedido(int numPd2, string obs, int numCli, int numCdt, string estado){
        //     this.id_pedido=numPd2;
        //     this.obs_pedido=obs;
        //     this.id_cliente=numCli;
        //     this.id_cadete=numCdt;
        //     this.estado=estado;
        // }
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