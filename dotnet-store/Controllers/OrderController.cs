using dotnet_store.Models;
using dotnet_store.Services;
using Iyzipay;
using Iyzipay.Model;
using Iyzipay.Request;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace dotnet_store.Controllers;


[Authorize]
public class OrderController : Controller
{
    private ICartService _cartService;
    private readonly IConfiguration _configuration;
    private readonly DataContext _context;
    public OrderController(ICartService cartService, DataContext context, IConfiguration configuration)
    {
        _cartService = cartService;
        _context = context;
        _configuration = configuration;
    }

    [Authorize(Roles = "Admin")]
    public ActionResult Index()
    {
        return View(_context.Orders.ToList());
    }

    [Authorize(Roles = "Admin")]
    public ActionResult Details(int id)
    {
        var order = _context.Orders
                            .Include(i => i.OrderItems)
                            .ThenInclude(i => i.product)
                            .FirstOrDefault(i => i.Id == id);

        return View(order);
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
                OrderItems = cart.CartItems.Select(ci => new Models.OrderItem
                {
                    ProductId = ci.ProductId,
                    Price = ci.Product.Price,
                    Amount = ci.Amount,
                }).ToList()
            };

            var payment = await ProcessPayment(model, cart);
            if (payment.Status == "success")
            {
                _context.Orders.Add(order);
                _context.Carts.Remove(cart);

                await _context.SaveChangesAsync();

                return RedirectToAction("Completed", new { orderId = order.Id });
            }
            else
            {
                ModelState.AddModelError("", payment.ErrorMessage);
            }
        }

        ViewBag.Cart = cart;
        return View(model);
    }

    public ActionResult Completed(string orderId)
    {
        return View("Completed", orderId);
    }

    public async Task<ActionResult> OrderList()
    {
        var username = User.Identity?.Name;
        var orders = await _context.Orders
                                  .Include(i => i.OrderItems)
                                  .ThenInclude(i => i.product)
                                  .Where(i => i.Username == username)
                                  .ToListAsync();

        return View(orders);
    }

    private async Task<Payment> ProcessPayment(OrderCreateModel model, Cart cart)
    {
        Options options = new Options();
        options.ApiKey = _configuration["PaymentAPI:APIKey"];
        options.SecretKey = _configuration["PaymentAPI:SecretKey"];
        options.BaseUrl = "https://sandbox-api.iyzipay.com";

        CreatePaymentRequest request = new CreatePaymentRequest();
        request.Locale = Locale.TR.ToString();
        request.ConversationId = Guid.NewGuid().ToString();
        request.Price = cart.SubTotal().ToString();
        request.PaidPrice = cart.SubTotal().ToString();
        request.Currency = Currency.TRY.ToString();
        request.Installment = 1;
        request.BasketId = "B67832";
        request.PaymentChannel = PaymentChannel.WEB.ToString();
        request.PaymentGroup = PaymentGroup.PRODUCT.ToString();

        PaymentCard paymentCard = new PaymentCard();
        paymentCard.CardHolderName = model.CartName;
        paymentCard.CardNumber = model.CartNumber;
        paymentCard.ExpireMonth = model.CartExpirationMonth;
        paymentCard.ExpireYear = model.CartExpirationYear;
        paymentCard.Cvc = model.CartCVV;
        paymentCard.RegisterCard = 0;
        request.PaymentCard = paymentCard;

        Buyer buyer = new Buyer();
        buyer.Id = "BY789";
        buyer.Name = model.FullName;
        buyer.Surname = "Doe";
        buyer.GsmNumber = model.Phone;
        buyer.Email = "email@email.com";
        buyer.IdentityNumber = "74300864791";
        buyer.LastLoginDate = "2015-10-05 12:43:35";
        buyer.RegistrationDate = "2013-04-21 15:12:09";
        buyer.RegistrationAddress = "Nidakule Göztepe, Merdivenköy Mah. Bora Sok. No:1";
        buyer.Ip = "85.34.78.112";
        buyer.City = model.City;
        buyer.Country = "Turkey";
        buyer.ZipCode = model.PostCode;
        request.Buyer = buyer;

        Address address = new Address();
        address.ContactName = model.FullName;
        address.City = model.City;
        address.Country = "Turkey";
        address.Description = model.AddressLine;
        address.ZipCode = model.PostCode;
        request.ShippingAddress = address;
        request.BillingAddress = address;

        List<BasketItem> basketItems = new List<BasketItem>();

        foreach (var item in cart.CartItems)
        {
            BasketItem basketItem = new BasketItem();
            basketItem.Id = item.CartItemId.ToString();
            basketItem.Name = item.Product.ProductName;
            basketItem.Category1 = "Phone";
            basketItem.ItemType = BasketItemType.PHYSICAL.ToString();
            basketItem.Price = item.Product.Price.ToString();

            basketItems.Add(basketItem);
        }


        request.BasketItems = basketItems;

        return await Payment.Create(request, options);
    }
}