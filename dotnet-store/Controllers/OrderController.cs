using dotnet_store.Models;
using dotnet_store.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace dotnet_store.Controllers;


[Authorize]
public class OrderController : Controller
{
    private ICartService _cartService;
    private readonly DataContext _context;
    public OrderController(ICartService cartService, DataContext context)
    {
        _cartService = cartService;
        _context = context;
    }
    public async Task<ActionResult> Checkout()
    {
        ViewBag.Cart = await _cartService.GetCart(User.Identity?.Name!);
        return View();
    }
    [HttpPost]
    public async Task<ActionResult> Checkout(OrderCreateModel model)
    {
        var username = User.Identity?.Name!;
        var cart = await _cartService.GetCart(username);

        if (cart.CartItems.Count == 0)
        {
            ModelState.AddModelError("", "There are no products in your cart.");
        }

        if (ModelState.IsValid)
        {
            var order = new Order
            {
                FullName = model.FullName,
                Phone = model.Phone,
                AddressLine = model.AddressLine,
                PostCode = model.PostCode,
                City = model.City,
                OrderNote = model.OrderNote!,
                OrderDate = DateTime.Now,
                TotalPrice = cart.Total(),
                Username = username,
                OrderItems = cart.CartItems.Select(ci => new OrderItem
                {
                    ProductId = ci.ProductId,
                    Price = ci.Product.Price,
                    Amount = ci.Amount,
                }).ToList()
            };

            _context.Orders.Add(order);
            _context.Carts.Remove(cart);

            await _context.SaveChangesAsync();

            return RedirectToAction("Completed", new { orderId = order.Id });
        }

        ViewBag.Cart = cart;
        return View(model);
    }

    public ActionResult Completed(string orderId)
    {
        return View("Completed", orderId);
    }
}