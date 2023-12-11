using System;
using System.Collections.Generic;

namespace QueryDemo.VBModels;

public partial class Order
{
    public int OrderId { get; set; }

    public string? OrderName { get; set; }

    public string? OrderDesc { get; set; }

    public int CustomerId { get; set; }

    public DateTime OrderDate { get; set; }

    public DateTime? ShippingAddress { get; set; }

    public virtual Customer Customer { get; set; } = null!;
}
