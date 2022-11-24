using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Threading.Tasks;
using MvcCadeteria.Models;
using MvcCadeteria.ViewModels;

namespace MvcCadeteria.Repositorio
{
    public class RepositorioCadete : MiRepositorioCadete
    {
        private static  string CadenaDeConexionPedidosDB = "Data Source=PedidosDB.db; Version=3";
        public List<Cadete> getCadetes()
        {
            List<Cadete> lista = new List<Cadete>();
            string CadenaDeConsulta = "SELECT id_persona, nombre, direccion, numero, telefono FROM cadetes INNER JOIN personas ON id_cadete=id_persona";
            using(SQLiteConnection connexion = new SQLiteConnection(CadenaDeConexionPedidosDB))
            {
                SQLiteCommand command = new SQLiteCommand(CadenaDeConsulta,connexion);
                connexion.Open();
                using(SQLiteDataReader reader = command.ExecuteReader()){
                    while(reader.Read()){
                        Cadete nuevo = new Cadete(reader.GetInt32(0),reader.GetString(1),reader.GetString(2),reader.GetInt32(3),reader.GetString(4));
                        lista.Add(nuevo);
                    }
                }
                connexion.Close();
            }
            return lista;
        }
        public Cadete getCadete(int id)
        {
            Cadete? cdt=null;
            string CadenaDeConsulta = "SELECT id_persona, nombre, direccion, numero, telefono FROM cadetes INNER JOIN personas ON id_cadete=id_persona WHERE id_cadete="+id;
            using(SQLiteConnection connexion = new SQLiteConnection(CadenaDeConexionPedidosDB))
            {
                SQLiteCommand command = new SQLiteCommand(CadenaDeConsulta,connexion);
                connexion.Open();
                using(SQLiteDataReader reader = command.ExecuteReader()){
                        while(reader.Read()){
                        cdt = new Cadete(reader.GetInt32(0),reader.GetString(1),reader.GetString(2),reader.GetInt32(3),reader.GetString(4));
                    }
                }
                connexion.Close();
            }
            return cdt!;
        }
        public bool updateCadete(EditarCdtViewModel cdt)
        {
            int valor=0;
            //****
             /*no es una consulta parametrizada y podría ser vulnerable a ataques de inyección SQL (o simplemente romperse si uno de los valores contiene un '). Se recomienda utilizar una declaración preparada con parámetros:*/
            // string sql = "update customers set balance = @balance where first_name = @forename";
            // SQLiteCommand command = new SQLiteCommand(sql, m_dbConnection);
            // command.Parameters.AddWithValue("@balance", balance);
            // command.Parameters.AddWithValue("@forename", forename);
            // command.ExecuteNonQuery();
            //****
            //string CadenaDeUpdate = "UPDATE personas SET nombre=@nom, direccion=@call, numero=@num, telefono=@tel WHERE id_persona=@id";
            using(SQLiteConnection connexion = new SQLiteConnection(CadenaDeConexionPedidosDB))
            {
                //SQLiteCommand command = new SQLiteCommand(CadenaDeUpdate,connexion);
                SQLiteCommand command = new();
                connexion.Open();
                command.CommandText="UPDATE personas SET nombre=@nom, direccion=@call, numero=@num, telefono=@tel WHERE id_persona=@id";
                command.Connection=connexion;
                command.Parameters.AddWithValue("@nom",cdt.nombre);
                command.Parameters.AddWithValue("@call",cdt.calle);
                command.Parameters.AddWithValue("@num",cdt.numero);
                command.Parameters.AddWithValue("@tel",cdt.telefono);
                command.Parameters.AddWithValue("@id",cdt.id);
                valor=command.ExecuteNonQuery();
                connexion.Close();
            }
            if(valor==0){return false;}else{return true;}
        }
        public bool altaCadete(AltaCdtViewModel cdt){
            int valor=0;
            int id=altaPersona(cdt.NombreCadete!,cdt.Direccion!,cdt.Numero,cdt.Telefono!);
            if(id!=0){
                using(SQLiteConnection connexion = new SQLiteConnection(CadenaDeConexionPedidosDB))
                {
                    SQLiteCommand command = new();
                    connexion.Open();
                    command.CommandText="INSERT INTO cadetes (id_cadete, id_cadeteria) VALUES (@id, @sucursal)";
                    command.Connection=connexion;
                    command.Parameters.AddWithValue("@id",id);
                    command.Parameters.AddWithValue("@sucursal",1);
                    valor=command.ExecuteNonQuery();
                    connexion.Close();
                }
            }
            if(valor==0){return false;}else{return true;}
        }
        public int altaPersona(string nom, string calle, int num, string tel)
        {
            using(SQLiteConnection connexion = new SQLiteConnection(CadenaDeConexionPedidosDB))
            {
                int valor=0;
                SQLiteCommand command = new();
                connexion.Open();
                command.CommandText="INSERT INTO personas (nombre,direccion,numero,telefono) VALUES (@nom,@call,@num,@tel)";
                command.Connection=connexion;
                command.Parameters.AddWithValue("@nom",nom);
                command.Parameters.AddWithValue("@call",calle);
                command.Parameters.AddWithValue("@num",num);
                command.Parameters.AddWithValue("@tel",tel);
                valor = command.ExecuteNonQuery();
                connexion.Close();
                if(valor>0)
                {
                    return ultimoIdPersona();
                }else{
                    return 0;
                }
            }
        }
        public int ultimoIdPersona()
        {
            int IdBuscado;
            string CadenaDeConsulta = "SELECT MAX(id_persona) FROM personas";
            using(SQLiteConnection connexion = new SQLiteConnection(CadenaDeConexionPedidosDB))
            {
                SQLiteCommand command = new SQLiteCommand(CadenaDeConsulta,connexion);
                connexion.Open();
                IdBuscado=Convert.ToInt32(command.ExecuteScalar());
                connexion.Close();
            }
            return IdBuscado;
        }
    }
}