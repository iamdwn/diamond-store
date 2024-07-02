using System;
using System.Collections.Generic;

namespace BussinessObject.Models;

public partial class OrderItem
{
    public Guid OrderItemId { get; set; }

    public int Id { get; set; }

    public Guid? OrderId { get; set; }

    public Guid? ProductId { get; set; }

    public virtual Order? Order { get; set; }

    public virtual Product? Product { get; set; }
}
