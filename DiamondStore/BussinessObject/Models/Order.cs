using System;
using System.Collections.Generic;

namespace BussinessObject.Models;

public partial class Order
{
    public Guid OrderId { get; set; }

    public int Id { get; set; }

    public Guid? UserId { get; set; }

    public DateOnly OrderDate { get; set; }

    public decimal TotalAmount { get; set; }

    public string Status { get; set; } = null!;

    public Guid? VoucherId { get; set; }

    public decimal TotalPrice { get; set; }

    public virtual ICollection<Delivery> Deliveries { get; set; } = new List<Delivery>();

    public virtual ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();

    public virtual User? User { get; set; }

    public virtual Voucher? Voucher { get; set; }
}
