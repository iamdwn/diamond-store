using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BussinessObject.DTO
{
    public class ProductElement
    {
        public Guid? ProductId { get; set; }
        public string Name { get; set; } = null!;
        public decimal Price { get; set; }
        public Guid? CategoryId { get; set; }
        public string CategoryName { get; set; }
    }
}
