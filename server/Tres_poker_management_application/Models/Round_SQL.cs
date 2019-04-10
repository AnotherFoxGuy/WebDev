using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Tres_poker_management_application.Models;

namespace Tres_poker_management_application.Models
{
    public class Round_SQL : Model
    {
        public IEnumerable<dynamic> GetRoundList(int Profile_ID)
        {
            string sql = "SELECT * FROM Round WHERE Gameprofile_Profile_ID = @0 ORDER BY Round_NR ASC";
            var results = read(sql, Profile_ID);
            return results;
        }

        public void CreateRound(Round model)
        {
            string sql =
                "INSERT INTO Round(Gameprofile_Profile_ID, Round_NR, Round_Time, Small_Blind, Big_Blind) VALUES(@0, @1, @2, @3, @4)";
            create(sql, model.Gameprofile_Profile_ID, model.Round_NR, model.Round_Time, model.Small_Blind,
                model.Big_Blind);
        }

        public void EditRound(int? Round_ID, Round model)
        {
            string sql =
                "UPDATE Round SET Round_Time = @0, Small_Blind = @1, Big_Blind = @2 WHERE Round_ID = @3 AND Gameprofile_Profile_ID = @4";
            update(sql, model.Round_Time, model.Small_Blind, model.Big_Blind, Round_ID, model.Gameprofile_Profile_ID);
        }

        public void UpdateRoundNumber(int? Round_ID, Round model)
        {
            string sql = "UPDATE Round SET Round_NR = @0 WHERE Round_ID = @1";
            update(sql, model.Round_NR, Round_ID);
        }

        public void DeleteRound(int? Round_ID)
        {
            string sql = "DELETE FROM Round WHERE Round_ID = @0";
            delete(sql, Round_ID);
        }

        public Round GetRound(int Round_NR, int Profile_ID)
        {
            var result = find("SELECT * FROM `Game_has_Round` WHERE `Game_Game_ID` = @1 AND `Round_NR` = @0", Round_NR,
                Profile_ID)[0];
            var model = new Round
            {
                Round_ID = result["Round_Round_ID"],
                Round_NR = result["Round_NR"],
                Round_Time = result["Round_Time"],
                Small_Blind = result["Small_Blind"],
                Big_Blind = result["Big_Blind"]
            };
            return model;
        }

        public int GetNumberOfRounds(int Profile_ID)
        {
            return (int) find(
                "SELECT COUNT(Game_Game_ID) AS NumberOfRounds FROM `Game_has_Round` WHERE `Game_Game_ID` = @0",
                Profile_ID)[0]["NumberOfRounds"];
        }

        /// <summary>
        /// copies all rounds from the selected profile into Game_has_Round
        /// </summary>
        /// <param name="Game_ID"></param>
        /// <param name="Profile_ID"></param>
        public void CloneRound2Game(int Game_ID, int? Profile_ID)
        {
            string sql =
                "INSERT INTO Game_has_Round(Game_Game_ID, Round_Round_ID, Round_NR, Round_Time, Small_Blind, Big_Blind) SELECT Game.Game_ID, Round.Round_ID, Round.Round_NR, Round.Round_Time, Round.Small_Blind, Round.Big_Blind FROM Game, Round WHERE Game.Game_ID = @0 AND Round.Gameprofile_Profile_ID = @1";
            create(sql, Game_ID, Profile_ID);
        }
    }
}