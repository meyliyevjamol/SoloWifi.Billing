using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoloWifi.Billing.ServiceLayer.Services;

public class CreateOrderModel 
{ 
    public long CustomerId { get; set; }
    public long PackageId { get; set; }

}
