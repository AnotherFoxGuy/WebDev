using MySql.Data.Types;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Tres_poker_management_application.Models
{
    public class Game_Has_Round
    {
        //foreignkey pair(1)
        public int Game_ID { get; set; }
        //foreignkey pair(2)
        public int Round_ID { get; set; }

        public int Round_NR { get; set; }
        public TimeSpan Round_Time { get; set; }
        public int Small_Blind { get; set; }
        public int Big_Blind { get; set; }
    }
}