using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace iBalekaWeb.Models.HomeViewModels
{
    public class EmailUsViewModels
    {
        [Required(ErrorMessage = "Name is required")]
        [Display(Name = "FullName")]
        public string name { get; set; }

        [Required]
        [EmailAddress]
        [Display (Name = "Email")]
        public string email { get; set; }

        [Required]
        [Phone]
        [Display(Name ="Contact")]
        public string contact { get; set; }


        [Required(ErrorMessage ="This field is required")]
        [Display(Name ="Radio")]
        public string radio { get; set; }


        [Required(ErrorMessage ="This field is required")]
        [Display(Name ="DropDown")]
        public string dropdwon { get; set; }


        [Required]
        [MinLength(10,ErrorMessage ="The message must be at least {0} characters long")]
        [Display(Name ="Message")]
        public string message { get; set; }


    }
}
