namespace dotnet_store.Models;


public class Cart
{
    public int CartId { get; set; }
    public string CustomerId { get; set; } = null!;

    public List<CartItem> CartItems { get; set; } = new();
}

public class CartItem
{
    public int CartItemId { get; set; }

    public int ProductId { get; set; }
    public Product Product { get; set; } = null!;

    public int CartId { get; set; }
    public Cart Cart { get; set; } = null!;

    public int Amount { get; set; }

}