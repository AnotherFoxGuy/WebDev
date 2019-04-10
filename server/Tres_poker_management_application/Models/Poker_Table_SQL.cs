using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Tres_poker_management_application.Models;

namespace Tres_poker_management_application.Models
{
    public class Poker_Table_SQL : Model
    {
        public int CreateTable(string Table_Name)
        {
            int Table_ID = 0;

            using (MySqlConnection connection = new MySqlConnection(ConnectionString))
            using (MySqlCommand command = new MySqlCommand("", connection))
            {
                connection.Open();
                command.CommandText = "INSERT INTO Poker_Table(Table_Name) VALUES(@0)";
                command.Parameters.AddWithValue("@0", Table_Name);
                command.ExecuteNonQuery();

                command.CommandText = "SELECT * FROM Poker_Table WHERE Table_ID = LAST_INSERT_ID()";
                MySqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    Table_ID = Convert.ToInt32(reader["Table_ID"]);
                }

                connection.Close();
            }

            return Table_ID;
        }

        public void CreateTable(Poker_Table model)
        {
            create("INSERT INTO Poker_Table(Table_Name) VALUES(@0)", model.Table_Name);
        }
        public void EditTable(int? Table_ID, Poker_Table model)
        {
            update("UPDATE Poker_Table SET Table_Name = @0 WHERE Table_ID = @1", model.Table_Name, Table_ID);
        }

        public string GetNameByID(int id)
        {
            var result = find("SELECT Table_Name FROM Poker_Table WHERE Table_ID = @0", id);
            return result.Count == 0 ? null : result[0]["Table_Name"];
        }

        public int GetIDByName(string name)
        {
            var result = find("SELECT Table_ID FROM Poker_Table WHERE Table_Name = @0", name);
            return result.Count == 0 ? -1 : result[0]["Table_ID"];
        }
        
        public int UsersAtTable(int Tabel_ID)
        {
            return (int)find("SELECT COUNT(User_ID) AS NumUsers FROM User WHERE Poker_Table_Table_ID = @0", Tabel_ID)[0]["NumUsers"];
        }

        public IEnumerable<dynamic> GetTableList()
        {
            return read("SELECT * FROM Poker_Table");
        }
        public void DeleteAllTables()
        {
            delete("DELETE FROM Poker_Table");
        }
        public void DeleteTable(int? Table_ID)
        {
            delete("DELETE FROM Poker_Table WHERE Table_ID = @0", Table_ID);
        }
    }
}