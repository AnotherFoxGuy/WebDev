using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Tres_poker_management_application.Models
{
    public class Gameprofile_SQL : Model
    {
        public IEnumerable<dynamic> GetProfileList()
        {
            string sql = "SELECT * FROM Gameprofile";
            var results = read(sql);
            return results;
        }
        public int CreateProfile(Gameprofile model)
        {
            int Profile_ID = 0;

            using (MySqlConnection connection = new MySqlConnection(ConnectionString))
            using (MySqlCommand command = new MySqlCommand("", connection))
            {
                connection.Open();
                command.CommandText = "INSERT INTO Gameprofile(Profilename, Starting_Budget, Rebuy, Pause_Time, Pause_After) VALUES(@0, @1, @2, @3, @4)";
                command.Parameters.AddWithValue("@0", model.Profilename);
                command.Parameters.AddWithValue("@1", model.Starting_Budget);
                command.Parameters.AddWithValue("@2", model.Rebuy);
                command.Parameters.AddWithValue("@3", model.Pause_Time);
                command.Parameters.AddWithValue("@4", model.Pause_After);
                command.ExecuteNonQuery();

                command.CommandText = "SELECT * FROM Gameprofile WHERE Profile_ID = LAST_INSERT_ID()";
                MySqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    Profile_ID = Convert.ToInt32(reader["Profile_ID"]);
                }
                connection.Close();
            }
            return Profile_ID;
        }
        public void FindProfile(int? Profile_ID, Gameprofile model)
        {
            string sql = "SELECT * FROM Gameprofile WHERE Profile_ID = @0";
            var result = find(sql, Profile_ID);
            model.Profile_ID = result[0]["Profile_ID"];
            model.Profilename = result[0]["Profilename"];
            model.Starting_Budget = result[0]["Starting_Budget"];
            model.Rebuy = result[0]["Rebuy"];
            model.Pause_Time = result[0]["Pause_Time"];
            model.Pause_After = result[0]["Pause_After"];
            model.Rules = result[0]["Rules"] is DBNull ? "" : result[0]["Rules"];
            model.Chips = result[0]["Chips"] is DBNull ? "{}" : result[0]["Chips"];
        }
        public void EditProfile(int? Profile_ID, Gameprofile model)
        {
            string sql = "UPDATE Gameprofile SET Profilename = @0, Starting_Budget = @1, Rebuy = @2, Pause_Time = @3, Pause_After = @4 WHERE Profile_ID = @5";
            update(sql, model.Profilename, model.Starting_Budget, model.Rebuy, model.Pause_Time, model.Pause_After, Profile_ID);
        }
        public void DeleteGameprofile(int? Profile_ID)
        {
            //delete the rounds connected to profile
            string sql = "DELETE FROM Round WHERE Gameprofile_Profile_ID = @0";
            delete(sql, Profile_ID);

            //delete game(s) using this profile 
            string sql2 = "DELETE FROM Game WHERE Gameprofile_Profile_ID = @0";
            delete(sql2, Profile_ID);

            string sql3 = "DELETE FROM Gameprofile WHERE Profile_ID = @0";
            delete(sql3, Profile_ID);
        }

        public List<dynamic> GetGameprofile(int profile_id)
        {
            string sql = "SELECT * FROM Gameprofile WHERE Profile_ID = @0";
            var result = find(sql, profile_id);
            return result;
        }
    }
}