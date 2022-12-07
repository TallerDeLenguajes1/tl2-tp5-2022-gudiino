using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MvcCadeteria.Models
{
    public class Usuario
    {
        private int id {get; set;}
        private string nombre {get; set;}
        private string usuario {get; set;}
        private string contrasenia {get; set;}
        private string rol {get; set;}
        //metodos de acceso a propiedades
        public int id_u { get => id; set => id = value; }
        public string nom_u { get => nombre; set => nombre=value;}
        public string log_u {get => usuario; set => usuario=value;}
        public string pass_u {get => contrasenia; set => contrasenia=value;}
        public string rol_u {get => rol; set => rol=value;}
        //constructor
        public Usuario(int num, string nom, string usu, string contr, string roles)
        {
            id=num;
            nombre=nom;
            usuario=usu;
            contrasenia=contr;
            rol=roles;
        }
    }
}