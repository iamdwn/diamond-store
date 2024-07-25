using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace BussinessObject.Models;

public partial class Delivery
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public Guid DeliveryId { get; set; }
    
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    public Guid? OrderId { get; set; }

    public Guid? ShiperId { get; set; }

    public Guid? ManagerId { get; set; }

    public string Status { get; set; } = null!;

    public virtual User? Manager { get; set; }

    public virtual Order? Order { get; set; }

    public virtual User? Shiper { get; set; }
}
