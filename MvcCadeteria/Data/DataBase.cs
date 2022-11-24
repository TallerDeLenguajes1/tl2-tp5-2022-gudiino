using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
//using Microsoft.EntityFrameworkCore;
//using Microsoft.EntityFrameworkCore.Sqlite;
using System.Data.SQLite;
using MvcCadeteria.Models;
namespace MvcCadeteria.Data
{
    // public class CadeteriaDbContext : DbContext
    // {//DbContextOptions -> contiene la cadena de conexion y el proveedor db
    //     public CadeteriaDbContext (DbContextOptions<CadeteriaDbContext> options)
    //         : base(options)
    //     {Console.WriteLine("Carga de la base de datos");
    //     }
    //     public DbSet<Cadete> cadetes {get;set;}= default!;
    //     public DbSet<Pedido> pedidos {get;set;} = default!;
    //     public DbSet<Cliente> clientes {get;set;} = default!;
    // }
    public class CadeteriaDB
    {
        private static  string cadenaConexionPedidosDB = "Data Source=PedidosDB.db; Version=3";
        //string connectionString = "Data Source=(local); Initial Catalog=Northwind;"+"Integrated Security=true";
        //using(SQLiteConnection connection = new SQLiteConnection(connectionString));
        //SQLiteConnection conectar = new SQLiteConnection(CadenaDeConexion);
        // using()
        // {
        //     conectar.Open();
        //     //...
        //     connection.Close();
        // }

        public List<Cadete> getCadetes(){
            List<Cadete> lista = new List<Cadete>();
            string cadenaDeConsulta = "SELECT id_persona, nombre, direccion, numero, telefono FROM cadetes INNER JOIN personas ON id_cadete=id_persona";
            using(SQLiteConnection connexion = new SQLiteConnection(cadenaConexionPedidosDB))
            {
                SQLiteCommand command = new SQLiteCommand(cadenaDeConsulta,connexion);
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

    }

    // public class MiCommand
    // {
    //     string queryString ="SELECT Nombre, FechaNacimiento FROM dbo.Empleados;";
    //     var command = new SqlCommand(QueryString,connection);
    //     connection.Open();
    //     //usign(SqlDataReader reader = command.ExecuteReader())//devuelve filas
    //     //command.ExecuteNonQuery()//crud
    //     //Int buscado=(Int32)command.ExecuteScalar()//recupera valor unico
    //     usign(var reader = command.ExecuteReader())
    //     {
    //         while(reader.Read())
    //         {
    //             //resultado obtenido una fila por vez
    //         }
    //     }
    //     using(SqlDataReader reader = command.ExecuteReader())
    //     {
    //         while(reader.Read()){
    //             Console.WriteLine(String.Format("{0},{1}", reader[0],reader[1]));
    //         }
    //     }
    //     private static void GetCadetes(string connectionString)
    //     {
    //         string connectionString = "Data Source=(local); Initial Catalog=Northwind;"+"Integrated Security=true";
    //         string queryString="SELECT * FROM cadetes INNER JOIN personas ON id_cadete=id_persona;";
    //         using(SqliteConnection connection = new SqliteConnection(connectionString))
    //         {
    //             SqliteCommand command = new SqliteCommand(queryString,connection);
    //             connection.Open();
    //             using(SqliteDataReader reader = command.ExecuteReader()){
    //                 while(reader.Read()){
    //                     //muestra datos
    //                 }
    //             }
    //             connection.Close();
    //         }
    //     }
    // }
    
}