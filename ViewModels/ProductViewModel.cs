using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Identity.ViewModels
{
    public class ProductViewModel
    {
        [Required]
        [StringLength(50, MinimumLength =2)]
        [Display(Name ="Product Name")]
        public string ProductName { get; set; }
        [Required]
        [Range(12, 2)]
        [Display(Name ="Price")]
        public decimal UnitPrice { get; set; }
        [Required]
        [StringLength(30)]
        public string Package { get; set; }
        public bool IsDiscontinued { get; set; }
    }
}
