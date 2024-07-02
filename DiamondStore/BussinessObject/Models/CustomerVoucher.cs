using System;
using System.Collections.Generic;

namespace BussinessObject.Models;

public partial class CustomerVoucher
{
    public Guid CustomerVoucherId { get; set; }

    public int Id { get; set; }

    public Guid? UserId { get; set; }

    public Guid? VoucherId { get; set; }

    public string Status { get; set; } = null!;

    public virtual User? User { get; set; }

    public virtual Voucher? Voucher { get; set; }
}
