using System;
using System.Data.SQLite;
using MvcCadeteria.Models;

namespace MvcCadeteria.Repositorio
{
    public class RepositorioPedido : MiRepositorioPedido
    {
        private static  string CadenaDeConexionPedidosDB = "Data Source=PedidosDB.db; Version=3";
        // **************************************************************************
        public List<Pedido> getPedidos()
        {
            List<Pedido> lista = new List<Pedido>();
            string CadenaDeConsulta = "SELECT * FROM pedidos";
            using(SQLiteConnection connexion = new SQLiteConnection(CadenaDeConexionPedidosDB))
            {
                SQLiteCommand command = new SQLiteCommand(CadenaDeConsulta,connexion);
                connexion.Open();
                using(SQLiteDataReader reader = command.ExecuteReader()){
                    while(reader.Read()){
                        Pedido pd2 = new Pedido{id_pd2=reader.GetInt32(0),detalle_pedido=reader.GetString(1),id_cli=reader.GetInt32(2),id_cdt=reader.GetInt32(3),estado_pedido=reader.GetInt32(4)};
                        lista.Add(pd2);
                    }
                }
                connexion.Close();
            }
            return lista;
        }
         // **************************************************************************
        public Pedido getPedido(int id)
        {
            Pedido? pd2= null;
            string CadenaDeConsulta = "SELECT * FROM pedidos WHERE id_pedido="+id;
            using(SQLiteConnection connexion = new SQLiteConnection(CadenaDeConexionPedidosDB))
            {
                SQLiteCommand command = new SQLiteCommand(CadenaDeConsulta,connexion);
                connexion.Open();
                using(SQLiteDataReader reader = command.ExecuteReader()){
                        while(reader.Read()){
                        pd2 = new Pedido{id_pd2=reader.GetInt32(0),detalle_pedido=reader.GetString(1),id_cli=reader.GetInt32(2),id_cdt=reader.GetInt32(3),estado_pedido=reader.GetInt32(4)};
                    }
                }
                connexion.Close();
            }
            return pd2!;
        }
        //******************************************************************************
        public bool altaPedido(Pedido pd2)
        {
            int valor=0;
            using(SQLiteConnection connexion = new SQLiteConnection(CadenaDeConexionPedidosDB))
            {
                SQLiteCommand command = new();
                connexion.Open();
                command.CommandText="INSERT INTO pedidos (obs_pedido,id_cliente,id_cadete,estado) VALUES (@obs,@cli,@cdt,@esta2)";
                command.Connection=connexion;
                command.Parameters.AddWithValue("@obs",pd2.detalle_pedido);
                command.Parameters.AddWithValue("@cli",pd2.id_cli);
                command.Parameters.AddWithValue("@cdt",pd2.id_cdt);
                command.Parameters.AddWithValue("@esta2",pd2.estado_pedido);
                valor = command.ExecuteNonQuery();
                connexion.Close();
            }
            return valor>0;
        }
         // **************************************************************************
        public bool updatePedido(Pedido pd2)
        {
            int valor=0;
            using(SQLiteConnection connexion = new SQLiteConnection(CadenaDeConexionPedidosDB))
            {
                //SQLiteCommand command = new SQLiteCommand(CadenaDeUpdate,connexion);
                SQLiteCommand command = new();
                connexion.Open();
                command.CommandText="UPDATE pedidos SET obs_pedido=@obs, id_cliente=@cli, id_cadete=@cdt, estado=@esta2 WHERE id_pedido=@id";
                command.Connection=connexion;
                command.Parameters.AddWithValue("@obs",pd2.detalle_pedido);
                command.Parameters.AddWithValue("@cli",pd2.id_cli);
                command.Parameters.AddWithValue("@cdt",pd2.id_cdt);
                command.Parameters.AddWithValue("@esta2",pd2.estado_pedido);
                command.Parameters.AddWithValue("@id",pd2.id_pd2);
                valor=command.ExecuteNonQuery();
                connexion.Close();
            }
            if(valor==0){return false;}else{return true;}
        }
        //++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
        public bool deletePedido(int id_pd2)
        {
            int num_eliminado=0;
            string CadenaDeConsulta = "DELETE FROM pedidos WHERE id_pedido="+id_pd2;
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
        public int ultimoIdPedido()
        {
            int IdBuscado;
            string CadenaDeConsulta = "SELECT MAX(id_pedido) FROM pedidos";
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