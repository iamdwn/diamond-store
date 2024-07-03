namespace BussinessObject.Models;

public partial class Voucher
{
    public Guid VoucherId { get; set; }

    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public decimal DiscountPercentage { get; set; }

    public DateOnly StartDate { get; set; }

    public DateOnly EndDate { get; set; }

    public virtual ICollection<CustomerVoucher> CustomerVouchers { get; set; } = new List<CustomerVoucher>();

    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();
}
