namespace dotnet_store.Models;

public class Order
{
    public int Id { get; set; }
    public DateTime OrderDate { get; set; }
    public string FullName { get; set; } = null!;
    public string Username { get; set; } = null!;
    public string City { get; set; } = null!;
    public string AddressLine { get; set; } = null!;
    public string PostCode { get; set; } = null!;
    public string Phone { get; set; } = null!;
    public double TotalPrice { get; set; }
    public string OrderNote { get; set; } = null!;

    public List<OrderItem> OrderItems { get; set; } = new();
}

public class OrderItem
{
    public int Id { get; set; }
    public int OrderId { get; set; }
    public Order Order { get; set; } = null!;
    public int ProductId { get; set; }
    public Product product { get; set; } = null!;
    public double Price { get; set; }
    public int Amount { get; set; }
}