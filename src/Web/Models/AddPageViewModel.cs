using System;
using System.ComponentModel.DataAnnotations;

namespace Web.Models
{
    public class AddPageViewModel
    {
        [Required]
        [MinLength(3)]
        public string Url { get; set; }
    }
}
