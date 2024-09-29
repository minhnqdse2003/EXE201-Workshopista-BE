using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Models.TicketRank
{
    public class TicketRankRequestModel
    {
        [Required(ErrorMessage = "Please input rank name")]
        public string RankName { get; set; } = null!;

        [Required(ErrorMessage = "Please input rank description")]
        public string? Description { get; set; }
        
        [Required(ErrorMessage = "Please input price")]
        public decimal Price { get; set; }

        [Required(ErrorMessage = "Please input capacity")]
        [RegularExpression("([0-9]+)", ErrorMessage = "Please input a number")]
        public int Capacity { get; set; }
    }
}
