using System;
using System.Data;
using MySql.Data;
using MySql.Data.Types;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Tres_poker_management_application.Models
{
    public class Round
    {
        [Key]
        public int Round_ID { get; set; }

        [Required(ErrorMessage = "{0} mag niet leeg zijn")]
        [Display(Name = "Ronde")]
        public int Round_NR { get; set; }

        [Required(ErrorMessage = "{0} mag niet leeg zijn")]
        [Display(Name = "Lengte ronde")]
        [DataType(DataType.Time)]
        public TimeSpan Round_Time { get; set; }

        [Required(ErrorMessage = "{0} mag niet leeg zijn")]
        [Display(Name = "Hoogte small blind")]
        [Range(1, int.MaxValue, ErrorMessage = "{0} moet minimaal {1} zijn & maximaal {2}.")]
        public int Small_Blind { get; set; }

        [Required(ErrorMessage = "{0} mag niet leeg zijn")]
        [Display(Name = "Hoogte big blind")]
        [Range(1, int.MaxValue, ErrorMessage = "{0} moet minimaal {1} zijn & maximaal {2}.")]
        public int Big_Blind { get; set; }

        //foreignkey
        public int Gameprofile_Profile_ID { get; set; }

        //for passing the amount of round to the forloop in the settingscontroller
        [Display(Name = "Aantal rondes")]
        [Range(1, 25, ErrorMessage = "{0} moet minimaal {1} & maximaal {2}")]        
        public int Number_Of_Rounds { get; set; }
    }
}