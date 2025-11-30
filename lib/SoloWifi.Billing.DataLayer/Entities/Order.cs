using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace SoloWifi.Billing.DataLayer;

[Table("doc_order")]
public partial class Order
{
    [Key]
    [Column("id")]
    public long Id { get; set; }

    [Column("customer_id")]
    public long CustomerId { get; set; }

    [Column("package_id")]
    public long PackageId { get; set; }

    [Column("status_id")]
    public int StatusId { get; set; }

    [Column("created_at", TypeName = "timestamp without time zone")]
    public DateTime CreatedAt { get; set; }

    [ForeignKey("CustomerId")]
    public virtual Customer Customer { get; set; } = null!;

    [ForeignKey("PackageId")]
    public virtual Package Package { get; set; } = null!;

    [ForeignKey("StatusId")]
    public virtual Status Status { get; set; } = null!;
}
