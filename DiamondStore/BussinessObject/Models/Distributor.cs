using System;
using System.Collections.Generic;

namespace BussinessObject.Models;

public partial class Distributor
{
    public Guid DistributorId { get; set; }

    public int Id { get; set; }

    public string DistributorName { get; set; } = null!;

    public string Locate { get; set; } = null!;

    public virtual ICollection<Category> Categories { get; set; } = new List<Category>();
}
