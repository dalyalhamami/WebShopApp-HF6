namespace WebShopApp_API.Controller;

[Route("api/[controller]")]
[ApiController]
public class OrderController : ControllerBase
{
    private readonly WebShopAppDBContext webShopAppDBContext;
    public OrderController(WebShopAppDBContext webShopAppDBContext)
    {
        this.webShopAppDBContext = webShopAppDBContext;
    }

    [HttpPost("Checkout")]
    public async Task<ActionResult<string>> Checkout(List<Cart> cartItems)
    {
        if (cartItems == null || !cartItems.Any())
        {
            return BadRequest(("Invalid data.", "No items in cart."));
        }

        string orderId = GenerateOrderId();
        var prods = await webShopAppDBContext.Product.ToListAsync();

        try
        {
            var detail = cartItems.FirstOrDefault();
            UserOrder userOrder = new UserOrder
            {
                UserId = detail.UserId,
                OrderId = orderId,
                PaymentMode = detail.PaymentMode,
                ShippingAddress = detail.ShippingAddress,
                ShippingCharges = detail.ShippingCharges,
                SubTotal = detail.SubTotal,
                Total = detail.Total,
                ShippingStatus = "Ordre modtaget",
                CreatedOn = DateTime.Now.ToString("dd/MM/yyyy"),
                UpdatedOn = DateTime.Now.ToString("dd/MM/yyyy")
            };
            webShopAppDBContext.UserOrder.Add(userOrder);

            foreach (var item in cartItems)
            {
                OrderDetail orderDetail = new OrderDetail
                {
                    OrderId = orderId,
                    ProductId = item.ProductId,
                    Quantity = item.Quantity,
                    Price = item.Price,
                    SubTotal = item.SubTotal,
                    CreatedOn = DateTime.Now.ToString("dd/MM/yyyy"),
                    UpdatedOn = DateTime.Now.ToString("dd/MM/yyyy")
                };
                webShopAppDBContext.OrderDetail.Add(orderDetail);

                var selectedProduct = prods.FirstOrDefault(x => x.Id == item.ProductId);
                if (selectedProduct != null)
                {
                    selectedProduct.Stock = selectedProduct.Stock - item.Quantity;
                    webShopAppDBContext.Product.Update(selectedProduct);
                }
            }

            await webShopAppDBContext.SaveChangesAsync();

            // Return the order ID as a response
            return Ok(orderId);
        }
        catch (Exception ex)
        {
            return BadRequest(("Error processing order.", ex.Message));
        }
    }

    private string GenerateOrderId()
    {
        string orderId;
        Random generator = new Random();
        do
        {
            orderId = "p" + generator.Next(1, 10000000).ToString("D8");
        } while (webShopAppDBContext.UserOrder.Any(x => x.OrderId == orderId));
        return orderId;
    }

    [HttpGet("GetOrdersByUserId")]
    public async Task<ActionResult<List<UserOrder>>> GetOrdersByUserId(int userId)
    {
        var data = webShopAppDBContext.UserOrder.Where(x => x.UserId == userId).ToList();
        var UserOrdersList = new List<UserOrder>();
        foreach (var uo in data)
        {
            UserOrder userOrder = new UserOrder();
            userOrder.Id = uo.Id;
            userOrder.OrderId = uo.OrderId;
            userOrder.ShippingAddress = uo.ShippingAddress;
            userOrder.ShippingCharges = uo.ShippingCharges;
            userOrder.ShippingStatus = uo.ShippingStatus;
            userOrder.PaymentMode = uo.PaymentMode;
            userOrder.SubTotal = uo.SubTotal;
            userOrder.Total = uo.Total;
            userOrder.CreatedOn = uo.CreatedOn;
            userOrder.UpdatedOn = uo.UpdatedOn;
            UserOrdersList.Add(userOrder);
        }
        Console.WriteLine($"UserOrdersList: {string.Join(", ", UserOrdersList.Select(uo => uo.OrderId))}");
        return UserOrdersList;
    }

    [HttpGet("GetOrderDetailForUser")]
    public async Task<ActionResult<List<Cart>>> GetOrderDetailForUser(int userId, string orderNumber)
    {
        // Find the UserOrder for the given userId and orderNumber
        var userOrder = await webShopAppDBContext.UserOrder.FirstOrDefaultAsync(uo => uo.UserId == userId && uo.OrderId == orderNumber);

        if (userOrder == null)
        {
            return NotFound("Order not found for the specified user");
        }

        // Find all OrderDetails for the given OrderId
        var orderDetails = await webShopAppDBContext.OrderDetail.Where(od => od.OrderId == orderNumber).ToListAsync();

        if (orderDetails == null || !orderDetails.Any())
        {
            return NotFound("Order details not found");
        }

        // Map OrderDetail to CartModel
        var cart = orderDetails.Select(od => new Cart
        {
            ProductId = od.ProductId,
            ProductName = webShopAppDBContext.Product.FirstOrDefault(p => p.Id == od.ProductId)?.Name,
            ProductImage = webShopAppDBContext.Product.FirstOrDefault(p => p.Id == od.ProductId)?.ImageUrl,
            Price = od.Price,
            Quantity = od.Quantity,
            AvailableStock = webShopAppDBContext.Product.FirstOrDefault(p => p.Id == od.ProductId)?.Stock ?? 0,
            ShippingAddress = userOrder.ShippingAddress,
            ShippingCharges = userOrder.ShippingCharges,
            SubTotal = od.SubTotal,
            PaymentMode = userOrder.PaymentMode,
            UserId = userOrder.UserId,
            Total = userOrder.Total
        }).ToList();

        return cart;
    }

    [HttpGet("GetShippingStatusForOrder")]
    public async Task<ActionResult<List<string>>> GetShippingStatusForOrder(string orderNumber)
    {
        // Find the UserOrder for the given orderNumber
        var userOrders = await webShopAppDBContext.UserOrder.Where(uo => uo.OrderId == orderNumber).ToListAsync();

        if (userOrders == null || !userOrders.Any())
        {
            return NotFound("No orders found for the specified order number");
        }

        // Retrieve and split shipping statuses from UserOrders
        var shippingStatuses = userOrders.SelectMany(uo => uo.ShippingStatus.Split('|')).ToList();
        return shippingStatuses;
    }
}
