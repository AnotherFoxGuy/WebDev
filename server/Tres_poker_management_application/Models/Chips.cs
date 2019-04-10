using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Tres_poker_management_application.Models
{
    public class Chips
    {
        [Required(ErrorMessage = "{0} mag niet leeg zijn")]
        [Display(Name = "Waarde groen")]
        [Range(1, int.MaxValue, ErrorMessage = "{0} moet minimaal {1} zijn & maximaal {2}.")]
        public int Green { get; set; }

        [Required(ErrorMessage = "{0} mag niet leeg zijn")]
        [Display(Name = "Waarde paars")]
        [Range(1, int.MaxValue, ErrorMessage = "{0} moet minimaal {1} zijn & maximaal {2}.")]
        public int Purple { get; set; }

        [Required(ErrorMessage = "{0} mag niet leeg zijn")]
        [Display(Name = "Waarde rood")]
        [Range(1, int.MaxValue, ErrorMessage = "{0} moet minimaal {1} zijn & maximaal {2}.")]
        public int Red { get; set; }

        [Required(ErrorMessage = "{0} mag niet leeg zijn")]
        [Display(Name = "Waarde wit")]
        [Range(1, int.MaxValue, ErrorMessage = "{0} moet minimaal {1} zijn & maximaal {2}.")]
        public int White { get; set; }

        [Required(ErrorMessage = "{0} mag niet leeg zijn")]
        [Display(Name = "Waarde zwart")]
        [Range(1, int.MaxValue, ErrorMessage = "{0} moet minimaal {1} zijn & maximaal {2}.")]
        public int Black { get; set; }
    }
}