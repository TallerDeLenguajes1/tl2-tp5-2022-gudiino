using System;
using System.Data.SQLite;
using MvcCadeteria.Models;

namespace MvcCadeteria.Repositorio
{
    public class RepositorioCadeteria : MiRepositorioCadeteria
    {
        private static  string CadenaDeConexionPedidosDB = "Data Source=PedidosDB.db; Version=3";

        public Cadeteria GetDatosCadeteria()
        {
            Cadeteria datoSuc=null;
            string CadenaDeConsulta = "SELECT * FROM cadeterias";
            using(SQLiteConnection connexion = new SQLiteConnection(CadenaDeConexionPedidosDB))
            {
                SQLiteCommand command = new SQLiteCommand(CadenaDeConsulta,connexion);
                connexion.Open();
                using(SQLiteDataReader reader = command.ExecuteReader()){
                    while(reader.Read())
                    {
                        datoSuc = new Cadeteria{suc_id=reader.GetInt32(0),suc_nombre=reader.GetString(1),suc_domicilio=reader.GetString(2),suc_telefono=reader.GetString(3),suc_pago=reader.GetDouble(4)};
                    }
                }
                connexion.Close();
            }
            return datoSuc;
        }
    }
}