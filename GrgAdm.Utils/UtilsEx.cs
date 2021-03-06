﻿using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Web;
using System.Text;

namespace GrgAdm.Utils
{
    public class UtilsEx
    {
        public static string PastaLogs { get; set; }

        public static void Log(Exception ex)
        {
            StringBuilder texto = new StringBuilder();
            texto.AppendFormat("Mensagem Erro: {0};\r\n", ex.Message);
            texto.AppendFormat("Causa do Erro: {0};\r\n", ex.Source);
            texto.AppendFormat("Metodo: {0};\r\n", ex.TargetSite);
            texto.AppendFormat("Inner Exception: {0};\r\n", ex.InnerException);
            texto.Append("---------------------\r\n");
            texto.Append("StackTrace:\r\n");
            texto.Append(ex.StackTrace);

            MakeLog(texto.ToString());
        }

        public static void Log(Exception ex, string message)
        {
            StringBuilder texto = new StringBuilder();
            texto.AppendFormat("Mensagem Personalizada: {0};\r\n", message);
            texto.Append("---------------------\r\n");
            texto.AppendFormat("Mensagem Erro: {0};\r\n", ex.Message);
            texto.AppendFormat("Causa do Erro: {0};\r\n", ex.Source);
            texto.AppendFormat("Metodo: {0};\r\n", ex.TargetSite);
            texto.AppendFormat("Inner Exception: {0};\r\n", ex.InnerException);
            texto.Append("---------------------\r\n");
            texto.Append("StackTrace:\r\n");
            texto.Append(ex.StackTrace);

            MakeLog(texto.ToString());
        }

        public static void Log(string message)
        {
            MakeLog("Mensagem de Log: " + message);
        }

        private static void MakeLog(string text)
        {
            string path = PastaLogs;

            if (!Directory.Exists(path))
                Directory.CreateDirectory(path);

            
            string file = path + "\\" + string.Format("{7}_ErrorLog_{0}_{1}_{2}-{3}_{4}_{5}_{6}.log", DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, DateTime.Now.Hour, DateTime.Now.Minute, DateTime.Now.Second, DateTime.Now.Millisecond, Environment.UserName);
            using (StreamWriter sw = new StreamWriter(file, true))
            {
                sw.WriteLine("Relatorio de Erro Garagium Adm");
                sw.WriteLine("---------------------------------------------------------");
                sw.WriteLine("Data: " + DateTime.Now.ToShortDateString());
                sw.WriteLine("Hora: " + DateTime.Now.ToShortTimeString());
                sw.WriteLine("---------------------------------------------------------");
                sw.WriteLine(text);
                sw.WriteLine("---------------------------------------------------------");
                sw.Close();
            }
        }   
    }
}