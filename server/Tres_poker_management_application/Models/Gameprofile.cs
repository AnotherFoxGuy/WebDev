using MySql.Data.Types;
using System;
using System.ComponentModel.DataAnnotations;

namespace Tres_poker_management_application.Models
{
    public class Gameprofile
    {
        [Key]
        public int Profile_ID { get; set; }

        [Required(ErrorMessage = "{0} mag niet leeg zijn")]
        [Display(Name = "Profiel naam")]
        public string Profilename { get; set; }

        [Required(ErrorMessage = "{0} mag niet leeg zijn")]
        [Display(Name = "Start kapitaal")]
        [Range(1, int.MaxValue, ErrorMessage = "{0} moet minimaal {1} zijn & maximaal {2}.")]
        public int Starting_Budget { get; set; }

        [Required(ErrorMessage = "{0} mag niet leeg zijn")]
        [Display(Name = "Hoogte rebuy")]
        [Range(1, int.MaxValue, ErrorMessage = "{0} moet minimaal {1} zijn & maximaal {2}.")]
        public int Rebuy { get; set; }

        [Required(ErrorMessage = "{0} mag niet leeg zijn")]
        [Display(Name = "Lengte pauze")]
        [DataType(DataType.Time)]
        public TimeSpan Pause_Time { get; set; }

        [Required(ErrorMessage = "{0} mag niet leeg zijn")]
        [Display(Name = "Pauze na(rondes)")]
        [Range(1, int.MaxValue, ErrorMessage = "{0} moet minimaal {1} zijn & maximaal {2}.")]
        public int Pause_After { get; set; }

        public string Rules { get; set; }
        public string Chips { get; set; }
    }
}