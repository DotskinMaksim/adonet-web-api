using DotskinWebApi.Data;
using DotskinWebApi.DTOs;
using DotskinWebApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DotskinWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public OrdersController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllOrders()
        {
            // Проверка, вошел ли пользователь
            var userId = HttpContext.Session.GetInt32("UserId");
            if (userId == null)
            {
                return Unauthorized("User not logged in.");
            }
            var orders = await _context.Orders
                .Select(o => new
                {
                    o.Id,
                    o.Date,
                })
                .ToListAsync();

            return Ok(orders);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteOrder(int id)
        {
            var order = await _context.Orders.FindAsync(id);

            if (order == null)
            {
                return NotFound();
            }

            _context.Orders.Remove(order);
            await _context.SaveChangesAsync();

            return Ok(new { message = "Order deleted successfully" });
        }

        // POST: orders
        [HttpPost]
        public async Task<ActionResult<Order>> CreateOrder([FromBody] CreateOrderDto orderDto)
        {
            if (orderDto == null || orderDto.UserId <= 0 || orderDto.OrderItems == null || !orderDto.OrderItems.Any())
            {
                return BadRequest("Invalid order data.");
            }

            var user = await _context.Users.FindAsync(orderDto.UserId);
            if (user == null)
            {
                return NotFound("User not found.");
            }

            var order = new Order
            {
                Date = DateOnly.FromDateTime(DateTime.Now),
                IsPaid = orderDto.IsPaid,
                UserId = orderDto.UserId
            };

            double totalOrderPrice = 0.0;

            foreach (var itemDto in orderDto.OrderItems)
            {
                var product = await _context.Products.FindAsync(itemDto.ProductId);
                if (product == null)
                {
                    return NotFound($"Product with ID {itemDto.ProductId} not found.");
                }

                // Рассчитываем стоимость в зависимости от единицы измерения продукта
                double itemPrice = product.Unit == "kg"
                    ? product.PricePerUnit * itemDto.Quantity
                    : product.PricePerUnit * itemDto.Quantity;

                totalOrderPrice += itemPrice;

                var orderItem = new OrderItem
                {
                    Order = order,
                    ProductId = product.Id,
                    Quantity = itemDto.Quantity
                };

                _context.OrderItems.Add(orderItem);
            }

            _context.Orders.Add(order);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetOrder), new { id = order.Id }, new { order, totalOrderPrice });
        }

        // GET: orders/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<Order>> GetOrder(int id)
        {
            var order = await _context.Orders
                                      .Include(o => o.OrderItems)
                                      .ThenInclude(oi => oi.Product)
                                      .FirstOrDefaultAsync(o => o.Id == id);

            if (order == null)
            {
                return NotFound("Order not found.");
            }

            // Дополнительно добавим расчет общей стоимости заказа для отдачи на фронтенд
            var totalOrderPrice = order.OrderItems.Sum(oi => oi.TotalPrice);

            return Ok(new { order, totalOrderPrice });
        }
    }
}
