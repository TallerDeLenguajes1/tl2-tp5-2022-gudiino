using System;
namespace MvcCadeteria.Models {
    public class Cadete {
        private int id_cadete {get; set;}
        private string nombre {get; set;}
        private string direccion {get; set;}
        private string telefono {get; set;}
        private int id_cadeteria {get; set;}
        private int cantPd2 {get; set;}

        public int cdt_id { get => id_cadete; set => id_cadete = value; }
        public string cdt_nombre{ get => nombre; set => nombre = value;}
        public string cdt_domicilio {get => direccion; set => direccion = value;}
        public string cdt_telefono {get => telefono; set => telefono = value;}
        public int cdt_id_sucursal {get => id_cadeteria; set => id_cadeteria = value;}
        public int cdt_cant_pd2 {get => cantPd2; set => cantPd2 = value;}
    }
}