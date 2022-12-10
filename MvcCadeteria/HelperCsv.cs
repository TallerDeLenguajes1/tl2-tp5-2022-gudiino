using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using MvcCadeteria.Models;

namespace MvcCadeteria
{
    public static class HelperDeArchivos
    {
        public static List<string[]> LeerCsv(string ruta, char caracter)
        {
            FileStream MiArchivo = new FileStream(ruta, FileMode.OpenOrCreate);
            StreamReader StrReader = new StreamReader(MiArchivo);

            string? Linea = "";
            List<string[]> LecturaDelArchivo = new List<string[]>();
            while ((Linea = StrReader.ReadLine()) != null)
            {
                string[] Fila = Linea.Split(caracter);
                LecturaDelArchivo.Add(Fila);
            }
            MiArchivo.Close();
            return LecturaDelArchivo;
        }
        public static void GuardarCSV(string ruta, List<Cadete> lista,string[] cadeteria)
        {
            //**************************
            FileStream Fstream = new FileStream(ruta, FileMode.OpenOrCreate);
            using (StreamWriter StreamW = new StreamWriter(Fstream))
            {//Cadete(int iden, string nom, string dir,int num, string tel)
                StreamW.WriteLine(cadeteria[0]+','+cadeteria[1]+','+cadeteria[2]);
                foreach (Cadete linea in lista)
                {
                    // string a=linea.getId().ToString();
                    // string b=linea.getNumero().ToString();
                    // StreamW.WriteLine(a+','+linea.getNom()+','+linea.getCalle()+','+b+','+linea.getTelefono());
                }
            }//using libera los recursos 
            Fstream.Close();
        }
        public static void LimpiarCSV(string ruta)
        {
            File.Delete(ruta);
            FileStream Fstream = new FileStream(ruta, FileMode.OpenOrCreate);
            using (StreamWriter StreamW = new StreamWriter(Fstream))
            {
                StreamW.WriteLine("ID,Apellido,Nombre,DNI");
            }
            Fstream.Close();
        }
    }
}