using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Models.Category
{
    public class CategoryResponseModel
    {
        public Guid CategoryId { get; set; }

        public string? Name { get; set; }

        public string? Slug { get; set; }

        public string? Description { get; set; }
    }
}
