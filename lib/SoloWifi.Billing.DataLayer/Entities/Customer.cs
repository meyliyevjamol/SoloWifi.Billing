using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SoloWifi.Billing.DataLayer;

[Table("customer")]
public partial class Customer
{
    [Key]
    [Column("id")]
    public long Id { get; set; }

    [Column("balance")]
    public decimal? Balance { get; set; }
    [Column("total_mb")]
    public decimal? TotalMb { get; set; }
}
