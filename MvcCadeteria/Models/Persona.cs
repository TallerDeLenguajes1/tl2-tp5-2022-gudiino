using System;
namespace MvcCadeteria.Models {
    public class Persona {
        protected int id {get; set;}
        protected string nombre {get; set;}
        protected string calle {get; set;}
        protected int numero {get; set;}
        protected string telefono {get; set;}
        
        public Persona(int iden, string nom, string dir, int num, string tel){
            id=iden;
            nombre=nom;
            calle=dir;
            numero=num;
            telefono=tel;
        }
        public Persona(){
            // id=0;
            // nombre="Sin Dato";
            // calle="Sin Dato";
            // numero=0;
            // telefono="Sin Dato";
        }
        public int getId(){
            return id;
        }
        public string getNom(){
            return nombre;
        }
        public string getCalle(){
            return calle;
        }
        public int getNumero(){
            return numero;
        }
        public string getTelefono(){
            return telefono;
        }
         public void setId(int idin){
            id = idin;
        }
        public void setNom(string nom){
            nombre=nom;
        }
        public void setCalle(string call){
            calle=call;
        }
        public void setNumero(int num){
            numero=num;
        }
        public void setTelefono(string tel){
            telefono=tel;
        }
    }
}