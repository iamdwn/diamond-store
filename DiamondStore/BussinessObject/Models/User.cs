using System;
using System.Collections.Generic;

namespace BussinessObject.Models;

public partial class User
{
    public Guid UserId { get; set; }

    public int Id { get; set; }

    public string Username { get; set; } = null!;

    public string Password { get; set; } = null!;

    public string Email { get; set; } = null!;

    public Guid? RoleId { get; set; }

    public string Status { get; set; } = null!;

    public virtual ICollection<CustomerVoucher> CustomerVouchers { get; set; } = new List<CustomerVoucher>();

    public virtual ICollection<Delivery> DeliveryManagers { get; set; } = new List<Delivery>();

    public virtual ICollection<Delivery> DeliveryShipers { get; set; } = new List<Delivery>();

    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();

    public virtual Role? Role { get; set; }

    public virtual ICollection<Warranty> Warranties { get; set; } = new List<Warranty>();
}
