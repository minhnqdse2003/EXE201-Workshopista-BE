using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Models.Reviews
{
    public class ReviewCreateModel
    {
        [Required(ErrorMessage = "Please input work shop id!")]
        public Guid? WorkshopId { get; set; }

        [Required(ErrorMessage = "Please input your rating!")]
        [Range(0, 5, ErrorMessage = "The value must between 0 and 5!")]
        public short? Rating { get; set; }

        public string? Comment { get; set; }

    }
}
