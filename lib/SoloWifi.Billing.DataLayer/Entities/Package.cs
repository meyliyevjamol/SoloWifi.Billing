using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace SoloWifi.Billing.DataLayer;

[Table("package")]
public partial class Package
{
    [Key]
    [Column("id")]
    public long Id { get; set; }

    [Column("name", TypeName = "character varying")]
    public string Name { get; set; } = null!;

    [Column("price")]
    public decimal Price { get; set; }

    [Column("traffic_amount_mb")]
    public decimal TrafficAmountMb { get; set; }

}
