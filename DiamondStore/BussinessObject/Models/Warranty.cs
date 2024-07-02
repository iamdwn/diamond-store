using System;
using System.Collections.Generic;

namespace BussinessObject.Models;

public partial class Warranty
{
    public Guid WarrantyId { get; set; }

    public int Id { get; set; }

    public Guid? ProductId { get; set; }

    public Guid? UserId { get; set; }

    public DateOnly IssueDate { get; set; }

    public DateOnly ExpirationDate { get; set; }

    public virtual Product? Product { get; set; }

    public virtual User? User { get; set; }
}
