using System;
using System.Collections.Generic;

namespace BussinessObject.Models;

public partial class Category
{
    public Guid CategoryId { get; set; }

    public int Id { get; set; }

    public string? GiaCertificate { get; set; }

    public string? Cut { get; set; }

    public string? Color { get; set; }

    public string? Clarity { get; set; }

    public decimal? Carat { get; set; }

    public Guid? DistributorId { get; set; }

    public virtual Distributor? Distributor { get; set; }

    public virtual ICollection<Product> Products { get; set; } = new List<Product>();
}
