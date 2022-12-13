using System;
using System.Data.SQLite;
using MvcCadeteria.Models;

namespace MvcCadeteria.Repositorio
{
    public class RepositorioCliente : MiRepositorioCliente
    {
        private static  string CadenaDeConexionPedidosDB = "Data Source=PedidosDB.db; Version=3";
        // **************************************************************************
        public List<Cliente> getClientes()
        {
            List<Cliente> lista = new List<Cliente>();
            string CadenaDeConsulta = "SELECT * FROM clientes";
            using(SQLiteConnection connexion = new SQLiteConnection(CadenaDeConexionPedidosDB))
            {
                SQLiteCommand command = new SQLiteCommand(CadenaDeConsulta,connexion);
                connexion.Open();
                using(SQLiteDataReader reader = command.ExecuteReader()){
                    while(reader.Read()){
                        //Cadete nuevo = new Cadete(reader.GetInt32(0),reader.GetString(1),reader.GetString(2),reader.GetString(3),reader.GetInt32(4));
                        Cliente nuevo = new Cliente{cli_id=reader.GetInt32(0),cli_nombre=reader.GetString(1),cli_domicilio=reader.GetString(2),cli_telefono=reader.GetString(3),cli_detalle_direccion=reader.GetString(4)};
                        lista.Add(nuevo);
                    }
                }
                connexion.Close();
            }
            return lista;
        }
         // **************************************************************************
        public Cliente getClienteId(int id)
        {
            Cliente? cli= null;
            string CadenaDeConsulta = "SELECT * FROM clientes WHERE id_cliente="+id;
            using(SQLiteConnection connexion = new SQLiteConnection(CadenaDeConexionPedidosDB))
            {
                SQLiteCommand command = new SQLiteCommand(CadenaDeConsulta,connexion);
                connexion.Open();
                using(SQLiteDataReader reader = command.ExecuteReader()){
                        while(reader.Read()){
                        cli = new Cliente{cli_id=reader.GetInt32(0),cli_nombre=reader.GetString(1),cli_domicilio=reader.GetString(2),cli_telefono=reader.GetString(3),cli_detalle_direccion=reader.GetString(4)};
                    }
                }
                connexion.Close();
            }
            return cli!;
        }
        public Cliente getClienteNomDir(string nom,string dir)
        {
            Cliente? cli= null;
            string CadenaDeConsulta = "SELECT * FROM clientes WHERE nombre='"+nom.Trim()+"'"+" AND direccion='"+dir.Trim()+"'";
            using(SQLiteConnection connexion = new SQLiteConnection(CadenaDeConexionPedidosDB))
            {
                SQLiteCommand command = new SQLiteCommand(CadenaDeConsulta,connexion);
                connexion.Open();
                using(SQLiteDataReader reader = command.ExecuteReader()){
                        while(reader.Read()){
                        cli = new Cliente{cli_id=reader.GetInt32(0),cli_nombre=reader.GetString(1),cli_domicilio=reader.GetString(2),cli_telefono=reader.GetString(3),cli_detalle_direccion=reader.GetString(4)};
                    }
                }
                connexion.Close();
            }
            return cli!;
        }
        //******************************************************************************
        public bool altaCliente(Cliente cli)
        {//id es autonumerico
            int valor=0;
            using(SQLiteConnection connexion = new SQLiteConnection(CadenaDeConexionPedidosDB))
            {
                SQLiteCommand command = new();
                connexion.Open();
                command.CommandText="INSERT INTO clientes (nombre,direccion,telefono, referencia_direccion) VALUES (@nom,@dir,@tel,@ref_dir)";
                command.Connection=connexion;
                command.Parameters.AddWithValue("@nom",cli.cli_nombre);
                command.Parameters.AddWithValue("@dir",cli.cli_domicilio);
                command.Parameters.AddWithValue("@tel",cli.cli_telefono);
                command.Parameters.AddWithValue("@ref_dir",cli.cli_detalle_direccion);
                valor = command.ExecuteNonQuery();
                connexion.Close();
            }
            return valor>0;
        }
         // **************************************************************************
        public bool updateCliente(Cliente cli)
        {
            int valor=0;
            using(SQLiteConnection connexion = new SQLiteConnection(CadenaDeConexionPedidosDB))
            {
                //SQLiteCommand command = new SQLiteCommand(CadenaDeUpdate,connexion);
                SQLiteCommand command = new();
                connexion.Open();
                command.CommandText="UPDATE clientes SET telefono=@tel, referencia_direccion=@ref_dir WHERE id_cliente=@id";
                command.Connection=connexion;
                command.Parameters.AddWithValue("@tel",cli.cli_telefono);
                command.Parameters.AddWithValue("@ref_dir",cli.cli_detalle_direccion);
                command.Parameters.AddWithValue("@id",cli.cli_id);
                valor=command.ExecuteNonQuery();
                connexion.Close();
            }
            if(valor==0){return false;}else{return true;}
        }
        //++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
        public bool deleteCliente(int id_cli)
        {
            int num_eliminado=0;
            string CadenaDeConsulta = "DELETE FROM clientes WHERE id_cliente="+id_cli;
            using(SQLiteConnection connexion = new SQLiteConnection(CadenaDeConexionPedidosDB))
            {
                SQLiteCommand command = new SQLiteCommand(CadenaDeConsulta,connexion);
                connexion.Open();
                num_eliminado=Convert.ToInt32(command.ExecuteNonQuery());
                connexion.Close();
            }
            return num_eliminado>0;
        }
    }
}