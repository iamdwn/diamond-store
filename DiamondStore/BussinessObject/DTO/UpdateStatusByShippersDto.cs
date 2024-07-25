using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Dtos
{
    public class UpdateStatusByShippersDTO
    {
        public Guid DeliveryId { get; set; }
        public string Status { get; set; } = null!;
    }
}
