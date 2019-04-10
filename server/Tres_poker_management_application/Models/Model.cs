using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Tres_poker_management_application.Models
{
    public class Model
    {
        public const string ConnectionString = "<Read the install guide>";

        public void create(string sql, params object[] args)
        {
            MySqlConnection conn = new MySqlConnection(ConnectionString);
            try
            {
                Console.WriteLine("Connecting to MySQL...");
                conn.Open();

                MySqlCommand cmd = new MySqlCommand(sql, conn);

                for (int i = 0; i < args.Length; i++)
                {
                    cmd.Parameters.AddWithValue("@" + i, args[i]);
                }

                cmd.ExecuteNonQuery();

                //MySqlDataReader rdr = cmd.ExecuteReader();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            conn.Close();

        }
        public dynamic read(string sql, params object[] args)
        {
            MySqlConnection conn = new MySqlConnection(ConnectionString);
            var list = new List<dynamic>();

            try
            {
                Console.WriteLine("Connecting to MySQL...");
                conn.Open();

                MySqlCommand cmd = new MySqlCommand(sql, conn);
                if (args != null)
                {
                    for (int i = 0; i < args.Length; i++)
                    {
                        cmd.Parameters.AddWithValue("@" + i, args[i]);
                    }
                }

                var rdr = cmd.ExecuteReader();
                foreach (var item in rdr)
                {
                    list.Add(item);
                }
                rdr.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            conn.Close();

            return list;
        }

        public dynamic find(string sql, params object[] args)
        {
            MySqlConnection conn = new MySqlConnection(ConnectionString);
            var list = new List<dynamic>();

            try
            {
                Console.WriteLine("Connecting to MySQL...");
                conn.Open();

                MySqlCommand cmd = new MySqlCommand(sql, conn);
                if (args != null)
                {
                    for (int i = 0; i < args.Length; i++)
                    {
                        cmd.Parameters.AddWithValue("@" + i, args[i]);
                    }
                }

                var rdr = cmd.ExecuteReader();

                //while(rdr.Read())
                foreach (var item in rdr)
                {
                    list.Add(item);
                }
                rdr.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            conn.Close();

            return list;
        }

        public void update(string sql, params object[] args)
        {
            MySqlConnection conn = new MySqlConnection(ConnectionString);

            try
            {
                Console.WriteLine("Connecting to MySQL...");
                conn.Open();

                MySqlCommand cmd = new MySqlCommand(sql, conn);
                if (args != null)
                {
                    for (int i = 0; i < args.Length; i++)
                    {
                        cmd.Parameters.AddWithValue("@" + i, args[i]);
                    }
                }

                //var rdr = cmd.ExecuteReader();
                cmd.ExecuteNonQuery();
                //rdr.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            conn.Close();

        }

        public void delete(string sql, params object[] args)
        {
            MySqlConnection conn = new MySqlConnection(ConnectionString);
            var list = new List<dynamic>();

            try
            {
                Console.WriteLine("Connecting to MySQL...");
                conn.Open();

                MySqlCommand cmd = new MySqlCommand(sql, conn);
                if (args != null)
                {
                    for (int i = 0; i < args.Length; i++)
                    {
                        cmd.Parameters.AddWithValue("@" + i, args[i]);
                    }
                }

                //var rdr = cmd.ExecuteReader();
                cmd.ExecuteNonQuery();
                //rdr.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            conn.Close();
        }
    }
}
