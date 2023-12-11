
using QueryDemo.MyContexts;
using QueryDemo.VBModels;

using (VbcustomerOrderDbContext context=new VbcustomerOrderDbContext())
{
    //find single record// face exception

    //var recOne=context.Customers.First();

    ////will not face any exception
    //var resTwo = context.Customers.FirstOrDefault();

    //var recThree = context.Customers.FirstOrDefault(a=>a.CustomerId==1);

    // var recFour = context.Customers.Find(1);
    // string s = "Hello";

    //all customer info
    //var data = (from a in context.Customers
    //            select a).ToList();
    //foreach (var item in data)
    //{
    //    Console.WriteLine(item.CustomerName);
    //}
    //Console.WriteLine("============================");
    //var recData=context.Customers.Skip(2).Take(3).ToList();

    //foreach (var rec in recData)
    //{
    //    Console.WriteLine(rec.CustomerName);
    //}
    //var data=(from c in context.Customers
    //         from o in context.Orders
    //         where o.CustomerId == c.CustomerId
    //         select new {c.CustomerName,o.OrderName}).ToList();

    //foreach (var item in data)
    //{
    //    Console.WriteLine(item.CustomerName+"  "+item.OrderName);
    //}
    var sortedCustomer = from c in context.Customers
                         orderby c.CustomerId descending
                         select c;
    Console.WriteLine("----------Order By CustomersID-------------");
    Console.WriteLine();
    foreach (var k in sortedCustomer)
    {
        Console.WriteLine("ID is  "+k.CustomerId+"   Name is   " + k.CustomerName);
    }
    Console.WriteLine("==================Group By Id==============================");
    var orderCustData1 = context.Orders
        .GroupBy(o => o.CustomerId)
        .Select(g => new
        {
            CustomerId = g.Key,
            TotalOrders = g.Count()
        }).ToList();


    foreach (var order in orderCustData1)
    {
        Console.WriteLine($"Customer Id  :{order.CustomerId}. Total orders are  :{order.TotalOrders}");
    }
    Console.WriteLine("==================Group By Id And Name==============================");
    var orderCustData2 = context.Orders
        .Join(
            context.Customers,
            order=>order.CustomerId,
            customer=>customer.CustomerId,
            (order,customer)=>new {Order=order,Customer=customer}
        ).
        GroupBy(
            join=>new
            {
                join.Customer.CustomerId,
                join.Customer.CustomerName
            }
        ).
        Select(data => new
        {
            CustomerId=data.Key.CustomerId,
            CustomerName=data.Key.CustomerName,
            TotalOrders=data.Count()
        });
    foreach (var order in orderCustData2)
    {
        Console.WriteLine($"Customer Id  :{order.CustomerId}, Customer Name  :{order.CustomerName}, Total orders are  :{order.TotalOrders}");
    }
    Console.WriteLine("==================Data Id With Date,CName,OName==============================");
    var orderCustData3 = context.Customers
        .Join(
        context.Orders,
        customer=>customer.CustomerId,
        order=>order.CustomerId,
        (customer, order) => new {Customer=customer,Order=order}
        ).
        Select(join =>new
        {
          CustomerId=join.Customer.CustomerId,
          CustomerName=join.Customer.CustomerName,
          OrderName=join.Order.OrderName,
          OrderDate=join.Order.OrderDate
        }).ToList();

    foreach (var order in orderCustData3)
    {
        Console.WriteLine($"Customer Id  :{order.CustomerId}, Customer Name  :{order.CustomerName},Order Name is  :{order.OrderName}, Order date is  :{order.OrderDate}");
    }
    Console.WriteLine("==================Group By Id With Date==============================");
    var orderCustData4 = context.Customers
        .Join(
        context.Orders,
        customer => customer.CustomerId,
        order => order.CustomerId,
        (customer, order) => new { Customer = customer, Order = order }
        )
        .GroupBy(join =>new
        {
            join.Customer.CustomerId,
            join.Customer.CustomerName,
            OrderDate =join.Order.OrderDate.Date
        })
        .Select(join => new
        {
            CustomerId = join.Key.CustomerId,
            CustomerName = join.Key.CustomerName,
            OrderDate =join.Key.OrderDate,            
            TotalOrders = join.Count()
        }).ToList();

    
    foreach (var order in orderCustData4)
    {
        Console.WriteLine($"Customer Id  :{order.OrderDate}, Customer Name  :{order.OrderDate}, Order date is  :{order.OrderDate}, Total Orders:{ order.TotalOrders}");
       
    }
    Console.WriteLine( "============Customer ID finding all orders================");
    var orderCustData5 = context.Orders       
       .GroupBy(join => new
       {               
           join.Customer.CustomerId,
           join.Customer.CustomerName,           
       })
       .Select(join => new
       {
           CustomerId = join.Key.CustomerId,
           CustomerName = join.Key.CustomerName,
           Orders = join.OrderBy(o=>o.OrderDate).ToList(),
           TotalOrders = join.Count()
       }).ToList();


    foreach (var cus in orderCustData5)
    {
        Console.WriteLine($"Customer ID: {cus.CustomerId}, Customer Name:  {cus.CustomerName}");

        foreach (var order in cus.Orders)
        {
            Console.WriteLine($"  Order ID: {order.OrderId} - Order Date: {order.OrderDate}");
            // Access other properties of the order as needed
        }
    }
    Console.WriteLine("===============MAx======================");
    var maxId = (from a in context.Customers
                 select a.CustomerId).Max();

    Console.WriteLine(maxId);
    Console.WriteLine("===============MIN=======================");
    var minId = (from a in context.Customers
                 select a.CustomerId).Min();

    Console.WriteLine(minId);
    Console.WriteLine("==============Distinct==============");
    var d = (from a in context.Customers
             select a.CustomerName).Distinct();

    foreach (var item in d)
    {
        Console.WriteLine(item);
    }

    string s = "Hello";
}