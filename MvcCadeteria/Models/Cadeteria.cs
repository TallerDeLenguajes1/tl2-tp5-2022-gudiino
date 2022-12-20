using System;
using System.Collections.Generic;
using System.IO;
namespace MvcCadeteria.Models {
    public class Cadeteria{
        private int id {get; set;}
        private string nombre {get; set;}
        private string direccion {get; set;}
        private string telefono {get; set;}
        private double pago_x_entrega {get; set;}

        public int suc_id { get => id; set => id = value; }
        public string suc_nombre{ get => nombre; set => nombre = value;}
        public string suc_domicilio {get => direccion; set => direccion = value;}
        public string suc_telefono {get => telefono; set => telefono = value;}
        public double suc_pago {get => pago_x_entrega; set => pago_x_entrega = value;}
        
    }
}