using Microsoft.AspNetCore.Mvc;
using SoloWifi.Billing.ServiceLayer;

namespace SoloWifi.Billing.WebApi;

[ApiController]
[Route("[controller]/[action]")]
public class OrdersController : ControllerBase
{
    private readonly IOrderService _orderService;

    public OrdersController(IOrderService orderService)
    {
        _orderService = orderService;
    }

    [HttpPost]
    public async Task<IActionResult> CreateOrder([FromBody] CreateOrderModel request)
    {
        try
        {
            var order = await _orderService.CreateOrderAsync(request);
            return Ok(order);
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(new { error = ex.Message });
        }
        catch 
        {
            return BadRequest(new { error = "Tizimda xatolik yuz berdi." });
        }
    }

    [HttpPost("{id}/pay")]
    public async Task<IActionResult> PayOrder(int id)
    {
        try
        {
            var order = await _orderService.PayOrderAsync(id);
            return Ok(order);
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(new { error = ex.Message });
        }
        catch
        {
            return BadRequest(new { error = "Tizimda xatolik yuz berdi." });
        }
    }
    [HttpGet]
    public async Task<IActionResult> GetAllAsync()
    {
        return Ok(await _orderService.GetAllAsync());
    }
}
