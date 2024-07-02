using System;
using System.Collections.Generic;

namespace BussinessObject.Models;

public partial class Product
{
    public Guid ProductId { get; set; }

    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string? Description { get; set; }

    public decimal Price { get; set; }

    public bool IsExpired { get; set; }

    public Guid? CategoryId { get; set; }

    public virtual Category? Category { get; set; }

    public virtual ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();

    public virtual ICollection<Warranty> Warranties { get; set; } = new List<Warranty>();
}
