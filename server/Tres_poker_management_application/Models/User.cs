using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Tres_poker_management_application.Models
{
    public class User
    {
        [Key]
        public int User_ID { get; set; }

        [Required(ErrorMessage = "{0} mag niet leeg zijn")]
        [Display(Name = "Voornaam")]
        [StringLength(45, MinimumLength = 1, ErrorMessage = "{0} moet minimaal {2} en maximaal {1} tekens lang zijn")]
        public string Firstname { get; set; }

        [Required(ErrorMessage = "{0} mag niet leeg zijn")]
        [Display(Name = "Achternaam")]
        [StringLength(45, MinimumLength = 1, ErrorMessage = "{0} moet minimaal {2} en maximaal {1} tekens lang zijn")]
        public string Lastname { get; set; }

        public int Wins { get; set; }
        public bool Join_Game { get; set; }

        //foreignkey
        public int Poker_Table_Table_ID { get; set; }
    }
}