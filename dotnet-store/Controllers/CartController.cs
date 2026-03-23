using dotnet_store.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace dotnet_store.Controllers;

[Authorize]
public class CartController : Controller
{
    private readonly DataContext _context;
    public CartController(DataContext context)
    {
        _context = context;
    }


    public async Task<ActionResult> Index()
    {
        var cart = await GetCart();
        return View(cart);
    }

    [HttpPost]
    public async Task<ActionResult> AddToCart(int productId, int amount = 1)
    {
        var cart = await GetCart();

        var item = cart.CartItems.Where(i => i.ProductId == productId).FirstOrDefault();

        if (item != null)
        {
            item.Amount += 1;
            // added before
        }
        else
        {
            cart.CartItems.Add(new CartItem
            {
                ProductId = productId,
                Amount = amount
            });
        }

        await _context.SaveChangesAsync();

        return RedirectToAction("Index", "Home");
    }

    private async Task<Cart> GetCart()
    {
        var customerId = User.Identity?.Name;

        var cart = await _context.Carts
                        .Include(i => i.CartItems)
                        .ThenInclude(i => i.Product)
                        .Where(i => i.CustomerId == customerId)
                        .FirstOrDefaultAsync();
        if (cart == null)
        {
            cart = new Cart { CustomerId = customerId! };
            _context.Carts.Add(cart);
            await _context.SaveChangesAsync();
        }

        return cart;
    }
}