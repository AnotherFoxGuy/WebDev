using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Tres_poker_management_application.Models;

namespace Tres_poker_management_application.Models
{
    public class Game_SQL : Model
    {
        public int CreateGame(int? Gameprofile_Profile_ID, Game model)
        {
            int Game_ID = 0;

            using (MySqlConnection connection = new MySqlConnection(ConnectionString))
            using (MySqlCommand command = new MySqlCommand("", connection))
            {
                connection.Open();
                command.CommandText = "INSERT INTO Game(Gameprofile_Profile_ID, Pokertables, Current_Round, Time_Left) VALUES(@0, @1, @2, @3)";
                command.Parameters.AddWithValue("@0", Gameprofile_Profile_ID);
                command.Parameters.AddWithValue("@1", model.Pokertables);
                command.Parameters.AddWithValue("@2", model.Current_Round);
                command.Parameters.AddWithValue("@3", model.Time_Left);
                command.ExecuteNonQuery();

                command.CommandText = "SELECT * FROM Game WHERE Game_ID = LAST_INSERT_ID()";
                MySqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    Game_ID = Convert.ToInt32(reader["Game_ID"]);
                }
                connection.Close();
            }
            return Game_ID;
        }

        public void UpdateCurrentRound(int currentRound, int gameId)
        {
            update("UPDATE Game SET Current_Round = @0 WHERE Game_ID = @1", currentRound, gameId);
        }

        public void DeleteGame()
        {
            string sql = "DELETE FROM Game_has_Round";
            delete(sql);

            string sql2 = "DELETE FROM Game";
            delete(sql2);
        }

        public List<dynamic> GetGame(int id)
        {
            string sql = "SELECT * FROM Game WHERE Game_ID = @0";
            var result = find(sql, id);
            return result;
        }
    }
}