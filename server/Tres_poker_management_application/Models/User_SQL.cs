using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MySql.Data.MySqlClient;

namespace Tres_poker_management_application.Models
{
    public class User_SQL : Model
    {
        public IEnumerable<dynamic> GetUserList()
        {
            return read("SELECT * FROM User");
        }

        public List<User> GetUserList(bool joingame)
        {
            var ListOfUsers = new List<User>();

            using (var connection = new MySqlConnection(ConnectionString))
            using (var command = new MySqlCommand("SELECT * FROM User WHERE Join_Game = @0", connection))
            {
                connection.Open();
                command.Parameters.AddWithValue("@0", joingame);
                var reader = command.ExecuteReader();
                while (reader.Read())
                {
                    var user = new User
                    {
                        User_ID = reader["User_ID"] as int? ?? 0,
                        Firstname = reader["Firstname"] as string,
                        Lastname = reader["Lastname"] as string,
                        Wins = reader["Wins"] as int? ?? 0,
                        Join_Game = Convert.ToBoolean(reader["Join_Game"]),
                        Poker_Table_Table_ID = reader["Poker_Table_Table_ID"] as int? ?? 0
                    };
                    ListOfUsers.Add(user);
                }

                connection.Close();
                return ListOfUsers;
            }
        }

        public void SetTable(int? User_ID, int? Tabel_ID)
        {
            update("UPDATE User SET Poker_Table_Table_ID = @0 WHERE User_ID = @1", Tabel_ID, User_ID);
        }
       
        public void FindUser(int? User_ID, User model)
        {
            var result = find("SELECT Firstname, Lastname FROM User WHERE User_ID = @0", User_ID)[0];
            model.Firstname = result["Firstname"];
            model.Lastname = result["Lastname"];
            model.Join_Game = result["Join_Game"];
            model.Wins = result["Wins"];
            model.Poker_Table_Table_ID = result["Poker_Table_Table_ID"];
        }
        public void CreateUser(User model)
        {
            create("INSERT INTO User(Firstname, Lastname) VALUES(@0, @1)", model.Firstname, model.Lastname);
        }
        public void EditUser(int User_ID, User model)
        {
            update("UPDATE User SET Firstname = @0, Lastname = @1 WHERE User_ID = @2", model.Firstname, model.Lastname, User_ID);
        }
        public void JoinGame(int? User_ID, bool join)
        {
            update("UPDATE User SET Join_Game = @0 WHERE User_ID = @1", join, User_ID);
        }
        public void AllJoinGame(bool join)
        {
            update("UPDATE User SET Join_Game = @0", join);
        }
        public void DeleteUser(int? User_ID)
        {
            delete("DELETE FROM User WHERE User_ID = @0", User_ID);
        }
        public void ResetAllTableID()
        {
            update("UPDATE User SET Poker_Table_Table_ID = null");
        }
        public void RemoveTableID(int? Poker_Table_Table_ID)
        {
            update("UPDATE User SET Poker_Table_Table_ID = null WHERE Poker_Table_Table_ID = @0", Poker_Table_Table_ID);
        }
    }
}