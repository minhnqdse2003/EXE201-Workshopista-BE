using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Models.Category
{
    public class CategoryRequestModel
    {
        [Required(ErrorMessage = "Please input your category's name")]
        public string? Name { get; set; }

        public string? Slug { get; set; }

        [Required(ErrorMessage = "Please input your category's description")]
        public string? Description { get; set; }
    }
}
