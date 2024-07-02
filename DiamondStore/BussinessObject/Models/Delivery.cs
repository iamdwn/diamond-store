using System;
using System.Collections.Generic;

namespace BussinessObject.Models;

public partial class Delivery
{
    public Guid DeliveryId { get; set; }

    public int Id { get; set; }

    public Guid? OrderId { get; set; }

    public Guid? ShiperId { get; set; }

    public Guid? ManagerId { get; set; }

    public string Status { get; set; } = null!;

    public virtual User? Manager { get; set; }

    public virtual Order? Order { get; set; }

    public virtual User? Shiper { get; set; }
}
