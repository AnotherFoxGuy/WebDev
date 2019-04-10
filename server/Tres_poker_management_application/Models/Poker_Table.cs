using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Tres_poker_management_application.Models
{
    public class Poker_Table
    {
        [Key]
        public int Table_ID { get; set; }

        public string Table_Name { get; set; }

        //foreignkey
        public int Current_Game_ID { get; set; }
    }
}