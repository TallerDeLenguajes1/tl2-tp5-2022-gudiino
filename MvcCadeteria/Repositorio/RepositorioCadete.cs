using System;
using System.Data.SQLite;
using MvcCadeteria.Models;
using MvcCadeteria.ViewModels;

namespace MvcCadeteria.Repositorio
{
    public class RepositorioCadete : MiRepositorioCadete
    {
        private static  string CadenaDeConexionPedidosDB = "Data Source=PedidosDB.db; Version=3";
        // **************************************************************************
        public List<Cadete> getCadetes()
        {
            List<Cadete> lista = new List<Cadete>();
            string CadenaDeConsulta = "SELECT * FROM cadetes";
            using(SQLiteConnection connexion = new SQLiteConnection(CadenaDeConexionPedidosDB))
            {
                SQLiteCommand command = new SQLiteCommand(CadenaDeConsulta,connexion);
                connexion.Open();
                using(SQLiteDataReader reader = command.ExecuteReader()){
                    while(reader.Read()){
                        Cadete nuevo = new Cadete(reader.GetInt32(0),reader.GetString(1),reader.GetString(2),reader.GetString(3),reader.GetInt32(4));
                        lista.Add(nuevo);
                    }
                }
                connexion.Close();
            }
            return lista;
        }
         // **************************************************************************
        public Cadete getCadete(int id)
        {
            Cadete? cdt= null;
            string CadenaDeConsulta = "SELECT * FROM cadetes WHERE id_cadete="+id;
            using(SQLiteConnection connexion = new SQLiteConnection(CadenaDeConexionPedidosDB))
            {
                SQLiteCommand command = new SQLiteCommand(CadenaDeConsulta,connexion);
                connexion.Open();
                using(SQLiteDataReader reader = command.ExecuteReader()){
                        while(reader.Read()){
                        cdt = new Cadete(reader.GetInt32(0),reader.GetString(1),reader.GetString(2),reader.GetString(3),reader.GetInt32(4));
                    }
                }
                connexion.Close();
            }
            return cdt!;
        }
        //******************************************************************************
        public bool altaCadete(Cadete cdt)
        {
            int valor=0;
            using(SQLiteConnection connexion = new SQLiteConnection(CadenaDeConexionPedidosDB))
            {
                SQLiteCommand command = new();
                connexion.Open();
                command.CommandText="INSERT INTO cadetes (nombre,direccion,telefono, id_cadeteria) VALUES (@nom,@call,@tel,@suc)";
                command.Connection=connexion;
                command.Parameters.AddWithValue("@nom",cdt.cdt_nombre);
                command.Parameters.AddWithValue("@call",cdt.cdt_domicilio);
                command.Parameters.AddWithValue("@tel",cdt.cdt_telefono);
                command.Parameters.AddWithValue("@suc",cdt.cdt_id_sucursal);
                valor = command.ExecuteNonQuery();
                connexion.Close();
            }
            return valor>0;
        }
         // **************************************************************************
        public bool updateCadete(Cadete cdt)
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
                command.CommandText="UPDATE cadetes SET nombre=@nom, direccion=@call, telefono=@tel, id_cadeteria=@suc WHERE id_persona=@id";
                command.Connection=connexion;
                command.Parameters.AddWithValue("@nom",cdt.cdt_nombre);
                command.Parameters.AddWithValue("@call",cdt.cdt_domicilio);
                command.Parameters.AddWithValue("@tel",cdt.cdt_telefono);
                command.Parameters.AddWithValue("@id",cdt.cdt_id_sucursal);
                valor=command.ExecuteNonQuery();
                connexion.Close();
            }
            if(valor==0){return false;}else{return true;}
        }
        //++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
        public bool deleteCadete(int id_cdt)
        {
            int num_eliminado=0;
            string CadenaDeConsulta = "DELETE FROM cadetes WHERE id_cadete="+id_cdt;
            using(SQLiteConnection connexion = new SQLiteConnection(CadenaDeConexionPedidosDB))
            {
                SQLiteCommand command = new SQLiteCommand(CadenaDeConsulta,connexion);
                connexion.Open();
                num_eliminado=Convert.ToInt32(command.ExecuteNonQuery());
                connexion.Close();
            }
            return num_eliminado>0;
        }
        //++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
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