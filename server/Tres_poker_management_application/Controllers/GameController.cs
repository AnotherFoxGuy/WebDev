using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Tres_poker_management_application.Models;

namespace Tres_poker_management_application.Controllers
{
    /// <summary>
    /// contains all actions regarding game page
    /// </summary>
    public class GameController : Controller
    {
        /// <summary>
        /// initial page load
        /// </summary>
        /// <returns>Index page</returns>
        public ActionResult Index()
        {
            return GameRunner.Instance.GetGameStatus() ? View("RunningGame") : View();
        }

        /// <summary>
        /// Gets this page when the game is running
        /// </summary>
        /// <returns>RunningGame page</returns>
        public ActionResult RunningGame()
        {
            return View();
        }

        /// <summary>
        /// get the data fo the partial view
        /// </summary>
        /// <param name="id">1 for auto, 0 for manual</param>
        /// <returns>partial view</returns>
        public ActionResult GetPartialData(int? id)
        {
            return PartialView(id == 0 ? "PartialManual" : "PartialAutomatic");
        }

        /// <summary>
        /// Clear all table id in users, then remove all tables
        /// </summary>
        /// <returns>index page</returns>
        public ActionResult DeleteAllTables()
        {
            new User_SQL().ResetAllTableID();
            new Poker_Table_SQL().DeleteAllTables();
            return View("Index");
        }

        /// <summary>
        /// changes the state of a user, ingame or not
        /// </summary>
        /// <param name="id">url id(user)</param>
        /// <param name="join">yes / no</param>
        /// <returns>index page</returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult UserInGame(int? id, bool join)
        {
            if (id != null)
                new User_SQL().JoinGame(id, join);

            return View("Index");
        }

        /// <summary>
        /// check if tables can be combined and show message for it
        /// </summary>
        /// <returns>running game page</returns>
        public ActionResult MergeTables()
        {
            var tableRecord = DeepStreamConnector.Instance.GetRecord("Tables");

            var list = (from tab in GameRunner.Instance.Tabels
                where tab.Value.Count(u => u.Join_Game) <= 5
                select tab.Key).ToList();
            
            if(list.Count < 2)
                return View("Index");

            var fr = list[0];
            list.Remove(fr);

            var newTable = new List<User>();
            newTable.AddRange(GameRunner.Instance.Tabels[fr]);
            GameRunner.Instance.Tabels.Remove(fr);

            foreach (var t in list)
            {
                newTable.AddRange(GameRunner.Instance.Tabels[t]);
                GameRunner.Instance.Tabels.Remove(t);
            }

            GameRunner.Instance.Tabels.Add(fr, newTable);

            tableRecord["Tables"] = JsonConvert.SerializeObject(GameRunner.Instance.Tabels, Formatting.Indented);


            return View("RunningGame");
        }

        /// <summary>
        /// checks the users that are still in the game
        /// </summary>
        /// <param name="id">url id(user)</param>
        /// <param name="join">true for all users that are in the game, false for those that arent</param>
        /// <returns>running game page</returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult UserInCurrentGame(int? id, bool join)
        {
            if (id == null) return View("Index");

            var tableRecord = DeepStreamConnector.Instance.GetRecord("Tables");
            new User_SQL().JoinGame(id, join);

            var UsersAtTable = new List<int>();

            foreach (var t in GameRunner.Instance.Tabels)
            {
                var v = t.Value.Find(x => x.User_ID == id);

                if (v != null)
                    v.Join_Game = join;
            }

            foreach (var t in GameRunner.Instance.Tabels)
            {
                UsersAtTable.Add(t.Value.Count(u => u.Join_Game));
            }

            var num = UsersAtTable.Count(i => i <= 5);

            if (num >= 2)
                DeepStreamConnector.Instance.Events.Publish("MergeTablesNotification",
                    $"We merge {num} tables ");


            tableRecord["Tables"] = JsonConvert.SerializeObject(GameRunner.Instance.Tabels, Formatting.Indented);

            return View("RunningGame");
        }

        /// <summary>
        /// sets join game for all users, true or false
        /// </summary>
        /// <param name="join">true or false</param>
        /// <returns>redirect to index page</returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AllUsersInGame(bool join)
        {
            new User_SQL().AllJoinGame(join);

            return RedirectToAction("Index");
        }

        /// <summary>
        /// game start
        /// </summary>
        /// <param name="id">url id(profile)</param>
        /// <param name="game">game model with data</param>
        /// <returns>running game page</returns>
        public ActionResult Start(int? id, Game game)
        {
            if (id == null) return RedirectToAction("Index");
            game.Gameprofile_Profile_ID = (int) id;
            game.Game_Finished = false;
            game.Current_Round = 1;

            var AddedGame_ID =  new Game_SQL().CreateGame(id, game);
            new Round_SQL().CloneRound2Game(AddedGame_ID, id);

            GameRunner.Instance.StartGame(AddedGame_ID);
            return RedirectToAction("RunningGame");
        }

        /// <summary>
        /// creates a poker table
        /// </summary>
        /// <param name="poker_Table">pokertable data</param>
        /// <returns>partialview + created pokertable</returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreatePokerTable(Poker_Table poker_Table)
        {
            if (ModelState.IsValid)
                new Poker_Table_SQL().CreateTable(poker_Table);
            return PartialView("PartialManual", poker_Table);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id">url id(tableid)</param>
        /// <param name="poker_Table">pokertable model</param>
        /// <returns>partialview and the edited pokertable</returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditPokerTable(int? id, Poker_Table poker_Table)
        {
            if (id != null)
                new Poker_Table_SQL().EditTable(id, poker_Table);

            return PartialView("PartialManual", poker_Table);
        }

        /// <summary>
        /// delete pokertable by id
        /// </summary>
        /// <param name="id">url id(pokertable id)</param>
        /// <returns>partialview manual</returns>
        public ActionResult DeletePokerTable(int? id)
        {
            if (id != null)
            {
                new User_SQL().RemoveTableID(id);
                new Poker_Table_SQL().DeleteTable(id);
            }
                
            return PartialView("PartialManual");
        }

        /// <summary>
        /// adds table id to the user
        /// </summary>
        /// <param name="id">url id(user)</param>
        /// <param name="table_id">table id</param>
        /// <returns>partialview manual</returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddUser2Table(int? id, int? table_id)
        {
            if (id != null)
                new User_SQL().SetTable(id, table_id);
            return PartialView("PartialManual");
        }

        /// <summary>
        /// automatic devides users in tables
        /// </summary>
        /// <param name="id">url id(profile)</param>
        /// <returns></returns>
        public ActionResult SetupTables(int? id)
        {
            if (id == null) return View("Index");
            new User_SQL().ResetAllTableID();
            new Poker_Table_SQL().DeleteAllTables();

            var PokerTableSql = new Poker_Table_SQL();

            var tableRecord = DeepStreamConnector.Instance.GetRecord("Tables");
            var players = new User_SQL().GetUserList(true);

            var playersCount = players.Count;
            var tablesCount = 1;

            var cnt = playersCount / tablesCount;

            while (cnt >= 10)
            {
                tablesCount++;
                cnt = playersCount / tablesCount;
            }

            var playersAtTable = new int[tablesCount];
            for (var i = 0; i < playersAtTable.Length; i++)
            {
                playersAtTable[i] = playersCount / tablesCount;
            }

            for (var r = 0; r < playersCount % tablesCount; r++)
            {
                playersAtTable[r % tablesCount]++;
            }

            players.Sort((x, y) => y.Wins - x.Wins);

            playersCount = 0;
            tablesCount = 0;

            GameRunner.Instance.Tabels.Clear();

            foreach (var y in playersAtTable)
            {
                var tableName = $"Tafel nummer {tablesCount}";
                var users = new List<User>();
                
                var SQLTableId = PokerTableSql.GetIDByName(tableName);
                
                if(SQLTableId == -1)
                    SQLTableId = PokerTableSql.CreateTable(tableName);

                for (var i = 0; i < y; i++)
                {
                    var plr = players[playersCount];
                    new User_SQL().SetTable(plr.User_ID, SQLTableId);
                    users.Add(plr);

                    playersCount++;
                    if (playersCount >= players.Count)
                        playersCount = 0;
                }
                GameRunner.Instance.Tabels.Add(tableName, users);
                tablesCount++;
            }

            tableRecord["Tables"] = JsonConvert.SerializeObject(GameRunner.Instance.Tabels);

            return RedirectToAction("Index");
        }

        /// <summary>
        /// game pause
        /// </summary>
        /// <returns>runninggame page</returns>
        public ActionResult Pause()
        {
            GameRunner.Instance.PauseGame();

            return View("RunningGame");
        }

        /// <summary>
        /// stops the game
        /// </summary>
        /// <returns>game index page</returns>
        public ActionResult Stop()
        {
            GameRunner.Instance.StopGame();
            return RedirectToAction("Index");
        }
    }
}