using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Threading.Tasks;
using MvcCadeteria.Models;

namespace MvcCadeteria.Repositorio
{
    public class RepositorioUsuario : MiRepositorioUsuario
    {
        private static  string CadenaDeConexionPedidosDB = "Data Source=PedidosDB.db; Version=3";

        public Usuario getUsuario(string in_u, string pass_u)
        {
            string u = in_u.Trim();
            Usuario? nuevo=null;
            string CadenaDeConsulta = "SELECT * FROM usuarios WHERE usuario='"+u+"'";
            using(SQLiteConnection connexion = new SQLiteConnection(CadenaDeConexionPedidosDB))
            {
                SQLiteCommand command = new SQLiteCommand(CadenaDeConsulta,connexion);
                connexion.Open();
                using(SQLiteDataReader reader = command.ExecuteReader()){
                    while(reader.Read())
                    {
                        nuevo = new Usuario(reader.GetInt32(0),reader.GetString(1),reader.GetString(2),reader.GetString(3),reader.GetString(4));
                    }
                }
                connexion.Close();
            }
            if (nuevo!=null && nuevo.pass_u==pass_u)return nuevo;
            return null!;
        }
    }
}