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
        
            var orders = await _context.Orders
                .Select(o => new
                {
                    o.Id,
                    o.Date,
                })
                .ToListAsync();

            return Ok(orders);
        }

        [HttpGet("history/{userId}")]
        public async Task<IActionResult> GetOrderHistory(int userId)
        {
            if (userId <= 0)
            {
                return Unauthorized("User not logged in or session expired.");
            }

            var orders = await _context.Orders
                .Where(o => o.UserId == userId)
                .Include(o => o.OrderItems)
                .ThenInclude(oi => oi.Product)
                .ToListAsync();

            if (orders.Count == 0)
            {
                return NotFound("No orders found for this user.");
            }

            var orderHistory = orders.Select(o => new
            {
                o.Id,
                o.Date,
                TotalPrice = o.TotalPrice,
                Items = o.OrderItems.Select(oi => new
                {
                    oi.Product.Name,
                    oi.Quantity,
                    oi.Product.Unit
     
                }).ToList()
            }).ToList();

            return Ok(orderHistory);
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

        [HttpPost]
        public async Task<ActionResult<Order>> CreateOrder([FromQuery] int userId, [FromBody] CreateOrderDto orderDto)
        {
            if (userId <= 0)
            {
                return Unauthorized("User not logged in or session expired.");
            }

            var order = new Order
            {
                Date = DateOnly.FromDateTime(DateTime.Now),
                UserId = userId,
                TotalPrice = orderDto.TotalPrice  // Используем переданную общую сумму
            };

            double totalOrderPrice = 0.0;

            foreach (var itemDto in orderDto.OrderItems)
            {
                var product = await _context.Products.FindAsync(itemDto.ProductId);
                if (product == null)
                {
                    return NotFound($"Product with ID {itemDto.ProductId} not found.");
                }

                if (product.AmountInStock < itemDto.Quantity)
                {
                    return BadRequest("Not enough product in stock.");
                }

                double itemPrice = product.Unit == "kg"
                    ? product.PricePerUnit * itemDto.Quantity
                    : product.PricePerUnit * itemDto.Quantity;

                totalOrderPrice += TaxCalculator.GetWithTax(itemPrice);
                if (product.HasBottle)
                {
                    totalOrderPrice += 0.10;
                }

                var orderItem = new OrderItem
                {
                    Order = order,
                    ProductId = product.Id,
                    Quantity = itemDto.Quantity
                };

                _context.OrderItems.Add(orderItem);
                product.AmountInStock -= orderItem.Quantity;
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

            var totalOrderPrice = order.OrderItems.Sum(oi => oi.TotalPrice);

            return Ok(new { order, totalOrderPrice });
        }
    }
}
