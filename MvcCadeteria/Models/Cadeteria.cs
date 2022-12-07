using System;
using System.Collections.Generic;
using System.IO;
namespace MvcCadeteria.Models {
    class Cadeteria{
        private int id {get; set;}
        private string nombre {get; set;}
        private string direccion {get; set;}
        private string telefono {get; set;}
        public decimal pago_x_entrega {get; set;}
        
        public Cadeteria(int idin, string nom, string dir, string tel, decimal pago){
            this.id=idin;
            this.nombre=nom;
            this.direccion = dir;
            this.telefono = tel;
            this.pago_x_entrega = pago;
        }
    }
}