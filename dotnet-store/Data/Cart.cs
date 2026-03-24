namespace dotnet_store.Models;


public class Cart
{
    public int CartId { get; set; }
    public string CustomerId { get; set; } = null!;

    public List<CartItem> CartItems { get; set; } = new();

    public void AddItem(Product product, int amount)
    {
        var item = CartItems.Where(i => i.ProductId == product.Id).FirstOrDefault();

        if (item == null)
        {
            CartItems.Add(new CartItem { Product = product, Amount = amount });
        }
        else
        {
            item.Amount += amount;
        }
    }

    public void DeleteItem(int productId, int amount)
    {
        var item = CartItems.Where(i => i.ProductId == productId).FirstOrDefault();

        if (item != null)
        {
            item.Amount -= amount;

            if (item.Amount == 0)
            {
                CartItems.Remove(item);
            }
        }

    }

    public double SubTotal()
    {
        return CartItems.Sum(i => i.Product.Price * i.Amount);
    }
    public double Total()
    {
        return CartItems.Sum(i => i.Product.Price * i.Amount) * 1.2;
    }
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