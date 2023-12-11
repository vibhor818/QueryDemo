using System;
using System.Collections.Generic;

namespace QueryDemo.VBModels;

public partial class Customer
{
    public int CustomerId { get; set; }

    public string? CustomerName { get; set; }

    public string? CustomerAddress { get; set; }

    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();
}
