﻿using garagium_adm.Helpers;
using MySql.Data.MySqlClient;
using System;
using System.Data;
using GrgAdm.Dados.User;
using garagium_adm.Helpers;


namespace garagium_adm.Classes
{
    public class Login
    {
        public static DataTable VerificarLoginOld(string username, string password, out int id)
        {

            DataTable dt = new DataTable();

            MySqlConnection conn = new BaseDados().conn;
            MySqlCommand cmd = new MySqlCommand("VerificarLogin", conn);
            cmd.CommandType = CommandType.StoredProcedure;

            MySqlParameter p1 = new MySqlParameter("usern", MySqlDbType.VarChar);
            p1.Value = username;
            cmd.Parameters.Add(p1);

            MySqlParameter p2 = new MySqlParameter("passw", MySqlDbType.VarChar);
            p2.Value = password;
            cmd.Parameters.Add(p2);

            MySqlParameter p3 = new MySqlParameter("result", MySqlDbType.Int32);
            p3.Direction = ParameterDirection.Output;
            cmd.Parameters.Add(p3);

            conn.Open();
            dt.Load(cmd.ExecuteReader());

            id = (p3.Value == DBNull.Value ? 0 : (String.IsNullOrEmpty(p3.Value + "") ? 0 : Convert.ToInt32(p3.Value + "")));

            return dt;
        }

        public static UserInfo VerificarLogin(string username, string password)
        {

            BaseDados bd = new BaseDados();
            UserInfo userInfo = new UserInfo();

            string sql = @"SELECT id FROM gestcom.login WHERE username = @1 AND password = @2;";
            int id = bd.ExecNumberQuery(sql, username, password);

            if (id < 1) return null;

            return userInfo;
        }


    }
}