using dotnet_store.Models;
using dotnet_store.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace dotnet_store.Controllers;

public class CartController : Controller
{
    private readonly ICartService _cartService;
    public CartController(ICartService cartService)
    {
        _cartService = cartService;
    }


    public async Task<ActionResult> Index()
    {
        var customerId = _cartService.GetCustomerId();
        var cart = await _cartService.GetCart(customerId);
        return View(cart);
    }

    [HttpPost]
    public async Task<ActionResult> AddToCart(int productId, int amount = 1)
    {

        await _cartService.AddToCart(productId, amount);

        return RedirectToAction("Index", "Cart");
    }


    [HttpPost]
    public async Task<ActionResult> RemoveItem(int productId, int amount)
    {
        await _cartService.RemoveItem(productId, amount);


        return RedirectToAction("Index", "Cart");
    }


}