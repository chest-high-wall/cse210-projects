using System;

class Program
{
    static void Main(string[] args)
    {
        Address addr1 = new Address("123 River St", "St. George", "UT", "USA");
        Customer cust1 = new Customer("Tyler Andrew", addr1);
        Order order1 = new Order(cust1);
        order1.AddProduct(new Product("Fishing Rod", "F001", 79.99, 1));
        order1.AddProduct(new Product("Tackle Box", "T002", 24.50, 2));

        Address addr2 = new Address("45 Hillview Dr", "Toronto", "ON", "Canada");
        Customer cust2 = new Customer("Alex Johnson", addr2);
        Order order2 = new Order(cust2);
        order2.AddProduct(new Product("Camping Tent", "C101", 150.00, 1));
        order2.AddProduct(new Product("Sleeping Bag", "S202", 65.00, 2));

        Console.WriteLine(order1.GetPackingLabel());
        Console.WriteLine(order1.GetShippingLabel());
        Console.WriteLine($"Total Cost: ${order1.GetTotalCost():F2}\n");

        Console.WriteLine(order2.GetPackingLabel());
        Console.WriteLine(order2.GetShippingLabel());
        Console.WriteLine($"Total Cost: ${order2.GetTotalCost():F2}\n");
    }
}
