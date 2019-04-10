using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Timers;
using System.Web.Helpers;
using System.Web.Mvc;
using DeepStreamNet.Contracts;
using Newtonsoft.Json;
using Tres_poker_management_application.Models;

namespace Tres_poker_management_application
{
    public enum GameStatus
    {
        Running,
        Setup,
        Paused,
        Stopped
    }
    
    public class GameRunner
    {
        public Dictionary<string ,List<User>> Tabels  = new Dictionary<string, List<User>>();
        
        private Stopwatch _gameTimer;
        private Timer _roundTimer;
        private Timer _notificationTimer;

        private Round _currentRound;
        private int _roundNumber;
        private int _totalNumberOfRounds;
        private int _gameId;
        private TimeSpan _pauseTime;
        private int _pauseNumber = 1;
        private int _pauseAfter;
        private GameStatus _gameStatus;

        private IDeepStreamRecord _blinds;
        private IDeepStreamRecord _chips;
        private IDeepStreamRecord _rules;
        private IDeepStreamRecord _tables;
        private IDeepStreamRecord _global;
        
        
        private GameRunner()
        {
            Task.Run(SubData).Wait();
            
            var PokerTableSql = new Poker_Table_SQL();
            var ListOfUsers = new User_SQL().GetUserList();
            var ListOfTables = PokerTableSql.GetTableList();

            foreach (var table in ListOfTables)
            {               
                Tabels.Add(table["Table_Name"], new List<User>());
            }

            foreach (var u in ListOfUsers)
            {
                var pktId = u["Poker_Table_Table_ID"];
                
                if(pktId is DBNull) continue;
                
                //TabelsIds[u["Poker_Table_Table_ID"]]
                Tabels[PokerTableSql.GetNameByID(pktId)].Add(new User
                {
                    Join_Game =  Convert.ToBoolean(u["Join_Game"]),
                    User_ID = u["User_ID"] ,
                    Wins =  u["Wins"] ,
                    Lastname =  u["Lastname"],
                    Firstname =  u["Firstname"],
                    Poker_Table_Table_ID = pktId
                });
            }

            _tables["Tables"] = JsonConvert.SerializeObject(Tabels, Formatting.Indented);
           
            // TimeSpan.FromMinutes(minutes).TotalMilliseconds
            _gameTimer = new Stopwatch();
            _notificationTimer = new Timer(1000);
            _notificationTimer.Elapsed += PushNotification;
            _notificationTimer.AutoReset = true;

            _roundTimer = new Timer(25000);
            _roundTimer.Elapsed += NextRound;
            _roundTimer.AutoReset = true;
        }


        public void StartGame(int gid)
        {
            _gameId = gid;
            Task.Run(SubData).Wait();

            var gameInfo = new Game_SQL().GetGame(_gameId)[0];

            _global["Game_ID"] = gameInfo["Game_ID"];
            _global["Round"] = gameInfo["Current_Round"];
            _global["Pokertables"] = gameInfo["Pokertables"];
            _global["Gameprofile_Profile_ID"] = gameInfo["Gameprofile_Profile_ID"];

            int pr = gameInfo["Gameprofile_Profile_ID"];

            var gameProfile = new Gameprofile_SQL().GetGameprofile(pr)[0];

            _global["Starting_Budget"] = gameProfile["Starting_Budget"];
            _global["Profilename"] = gameProfile["Profilename"];
            _global["Pause_Time"] = gameProfile["Pause_Time"];
            _global["Pause_After"] = gameProfile["Pause_After"];
            _chips["Rebuy"] = gameProfile["Rebuy"];
            _rules["Rules"] = gameProfile["Rules"];
            _chips["Chips"] = gameProfile["Chips"];


            _roundNumber = gameInfo["Current_Round"];
            _pauseAfter = gameProfile["Pause_After"];

            if (_roundNumber <= 0) _roundNumber = 1;

            var sql = new Round_SQL();

            _currentRound = sql.GetRound(_roundNumber, _gameId);

            Console.WriteLine($"id: {_gameId}");

            _roundTimer.Interval = _currentRound.Round_Time.TotalMilliseconds;
            _pauseTime = (TimeSpan)gameProfile["Pause_Time"];
            
            _totalNumberOfRounds = new Round_SQL().GetNumberOfRounds(_gameId);

            _global["TotalNumberOfRounds"] = _totalNumberOfRounds;
            
            _notificationTimer.Start();
            _roundTimer.Start();
            _gameTimer.Start();

            SetGameStatus(GameStatus.Running);
            
        }

        public void PauseGame()
        {
            _notificationTimer.Stop();
            _roundTimer.Stop();
            _gameTimer.Stop();
            SetGameStatus(GameStatus.Paused);
        }

        public void StopGame()
        {
            _notificationTimer.Stop();
            _roundTimer.Stop();
            _gameTimer.Stop();

            _gameTimer.Reset();
            _roundNumber = 0;
            _pauseNumber = 1;
            
            SetGameStatus(GameStatus.Stopped);
            
            new Game_SQL().DeleteGame();          
        }

        public bool GetGameStatus()
        {
            return _gameTimer.IsRunning;
        }


        private void NextRound(object sender, ElapsedEventArgs e)
        {
            _gameTimer.Reset();
            _gameTimer.Start();
          
            if (_roundNumber >= _totalNumberOfRounds)
            {
                DeepStreamConnector.Instance.Events.Publish("PushNotification", "The game has ended!");
                StopGame();
                return;
            }

            if (_gameStatus != GameStatus.Paused && _pauseNumber >= _pauseAfter)
            {
                _pauseNumber = 0;
                SetGameStatus(GameStatus.Paused) ;
                _roundTimer.Interval = _pauseTime.TotalMilliseconds;                      
                return;
            }
            
            if (_gameStatus == GameStatus.Running && _pauseNumber != 0)
            {
                _roundTimer.Interval = TimeSpan.FromMinutes(1).TotalMilliseconds;
                SetGameStatus(GameStatus.Setup);
                return;
            }
            
            _pauseNumber++;
            _roundNumber++;
            SetGameStatus(GameStatus.Running);
            _global["Round"] = _roundNumber;

            new Game_SQL().UpdateCurrentRound(_roundNumber, _gameId);
                       
            _currentRound = new Round_SQL().GetRound(_roundNumber, _gameId);
            _roundTimer.Interval = _currentRound.Round_Time.TotalMilliseconds;
        }

        private void SetGameStatus(GameStatus status)
        {
            _gameStatus = status;

            switch (status)
            {
                case GameStatus.Running:
                    _global["GameStatus"] = "Running";
                    break;
                case GameStatus.Setup:
                    _global["GameStatus"] = "Setup";
                    break;
                case GameStatus.Paused:
                    _global["GameStatus"] = "Paused";
                    break;
                case GameStatus.Stopped:
                    _global["GameStatus"] = "Stopped";
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(status), status, null);
            }
        }

        private void PushNotification(object sender, ElapsedEventArgs e)
        {
            DeepStreamConnector.Instance.Events.Publish("TimerUpdate", _gameTimer.ElapsedMilliseconds);
        }

        private async Task SubData()
        {
            _blinds = await DeepStreamConnector.Instance.Records.GetRecordAsync("Blinds");
            _chips = await DeepStreamConnector.Instance.Records.GetRecordAsync("Chips");
            _rules = await DeepStreamConnector.Instance.Records.GetRecordAsync("Rules");
            _tables = await DeepStreamConnector.Instance.Records.GetRecordAsync("Tables");
            _global = await DeepStreamConnector.Instance.Records.GetRecordAsync("Global");
        }


        #region Singleton

        private static readonly Lazy<GameRunner> LazyGameRunner =
            new Lazy<GameRunner>(() => new GameRunner());

        public static GameRunner Instance => LazyGameRunner.Value;

        #endregion
    }
}