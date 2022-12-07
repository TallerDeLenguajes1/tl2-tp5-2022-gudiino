using System;
namespace MvcCadeteria.Models {
    public class Cliente{
        private int id {get; set;}
        private string nombre {get; set;}
        private string direccion {get; set;}
        private string telefono {get; set;}
        private string ref_domicilio {get; set;}

        public int cli_id { get => id; set => id = value; }
        public string cli_nombre{ get => nombre; set => nombre = value;}
        public string cli_domicilio {get => direccion; set => direccion = value;}
        public string cli_telefono {get => telefono; set => telefono = value;}
        public string detalle_direccion {get => ref_domicilio; set => ref_domicilio = value;}//sobre la ubicacion de la casa

        public Cliente(int iden, string nom, string dir, string tel, string dirREf){
            this.id = iden;
            this.nombre = nom;
            this.direccion = dir;
            this.telefono = tel;
            this.ref_domicilio=dirREf;
        }
    }
}