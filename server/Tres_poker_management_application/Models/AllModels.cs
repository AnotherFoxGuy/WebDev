using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Tres_poker_management_application.Models;

namespace Tres_poker_management_application.Models
{
    public class AllModels
    {
        public Game Game { get; set; }
        public Game_Has_Round Game_Has_Round { get; set; }
        public Gameprofile Gameprofile { get; set; }
        public Poker_Table Poker_Table { get; set; }
        public Round Round { get; set; }
        public User User { get; set; }
        public Chips Chips { get; set; }
    }
}