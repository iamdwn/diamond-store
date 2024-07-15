using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace BussinessObject.DTO
{
    public class DeliveryResponse
    {
        public Guid DeliveryId { get; set; }

        [JsonIgnore]
        public Guid? OrderId { get; set; }
        [JsonIgnore]
        public Guid? ShiperId { get; set; }
        [JsonIgnore]
        public Guid? ManagerId { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public List<string> Product { get; set; }
        public decimal ProductPrice { get; set; }
        public decimal EndPrice { get; set; }
        public string ManagerName { get; set; }
        public string DeliveryStatus { get; set; }
        public DateOnly OrderDate { get; set; }

    }
}
