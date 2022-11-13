using System;
using System.Collections.Generic;
using System.IO;
//using NLog;
namespace MvcCadeteria.Models {
    class Cadeteria{
        //private static readonly NLog.Logger Logger = NLog.LogManager.GetCurrentClassLogger();
        private string? nombre {get; set;}
        private string? telefono {get; set;}
        public static float pago_x_entrega {get; set;}
        private List<Cadete> cadetes {get; set;}
        private List<Cliente> clientes {get; set;}
        private List<Pedido> pedidos {get; set;}
        
        public string[] getCadeteria(){
            string pago2=pago_x_entrega.ToString();
            string[] nuevo= new string[] {nombre!,telefono!,pago2};
            return nuevo;
        }
        public Cadeteria(){
            cadetes=cargarDatos();
            clientes=new List<Cliente>();
            pedidos=new List<Pedido>();
        }
        private List<Cadete> cargarDatos(){
            List<Cadete> cadetes2=new List<Cadete>();
            string archivo = "listaCadetes.csv";
            bool aux=false;
            if(File.Exists(archivo)){
                List<string[]> lista_cadetes=HelperDeArchivos.LeerCsv(archivo,',');
                
                try
                {
                    foreach (var item in lista_cadetes)
                    {//Cadete(int iden, string nom, string dir,int num, string tel)
                        if(aux==true){
                            int n = Convert.ToInt32(item[0]);
                            int x = Convert.ToInt32(item[3]);
                            cadetes2.Add(new Cadete(n,item[1],item[2],x,item[4]));
                        }else{
                            nombre=item[0];
                            telefono=item[1];
                            pago_x_entrega= Convert.ToSingle(item[2]);
                            aux=true;
                        }
                    }
                    Console.WriteLine("Creacion lista exitosa");
                }
                catch (FormatException ex)
                {
                    Console.WriteLine("======================================================================");
                    Console.WriteLine("VALOR INVALIDO EN PLANILLA CADETERIA, debe ingresar un numeros ENTERO");
                    Console.WriteLine("VERIFIQUE LOS CAMPOS CORREPONDIENTES  A NUMEROS Y CORRIGA EL VALOR");
                    Console.WriteLine("======================================================================");
                    Console.WriteLine(ex);
                    //Logger.Info(ex);
                }
                catch(OverflowException ex)
                {
                    Console.WriteLine("======================================================================");
                    Console.WriteLine("Ingreso un valor demasiado GRANDE EN PLANILLA");
                    Console.WriteLine("VERIFIQUE LOS CAMPOS CORREPONDIENTES  A NUMEROS Y CORRIGA EL VALOR");
                    Console.WriteLine("======================================================================");
                    Console.WriteLine(ex);
                    //Logger.Info(ex);
                }
                catch(Exception ex)
                {
                    Console.WriteLine("OPERACION no valida");
                    Console.WriteLine("+++ MENSAJE EXCEPCION:");
                    Console.WriteLine(ex.Message);
                    //Logger.Warn(ex);
                    //Logger.Error(ex);
                    //Logger.Fatal(ex);
                }
            }else{
                Console.WriteLine("NO SE ENCONTRO EL ARCHIVO CORRESPONDIENTE A LOS DATOS DE LA CADETERIA");
                Console.WriteLine("VERIFIQUE LA PLANILLA CON LOS DATOS DE LA CADETERIA Y LOS CADETES");
            }
            return cadetes2;
        }
        // public int getNumPedido(){
        //     return cantidad_pedidos_dia;
        // }
        // public static void setNumPedido(){
        //     cantidad_pedidos_dia++;
        // }
        public List<Cadete> getCadetes(){
            return cadetes!;
        }
        public List<Cliente> getClientes(){
            return clientes!;
        }
        public List<Pedido> getPedidos(){
            return pedidos!;
        }
    }
}