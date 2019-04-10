using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Tres_poker_management_application.Models
{
    public class Game
    {
        [Key]
        public int Game_ID { get; set; }
        public bool Game_Finished { get; set; }
        public int Time_Left { get; set; }
        public int Current_Round { get; set; }
        public int Pokertables { get; set; }

        //foreignkey
        public int Gameprofile_Profile_ID { get; set; }
    }
}