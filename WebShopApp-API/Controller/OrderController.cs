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
    public async Task<ActionResult> Checkout(List<Cart> cartItems)
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
                ShippingStatus = "Pending",
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
        }
        catch (Exception ex)
        {
            return BadRequest(("Error processing order.", ex.Message));
        }

        return Ok(cartItems);
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
}
