using Confluent.Kafka;
using Microsoft.AspNetCore.Mvc;
using SoloWifi.Billing.ServiceLayer;
using SoloWifi.Billing.ServiceLayer.Services;
using SoloWifi.Billing.ServiceLayer.Services.OrderSevices;

namespace SoloWifi.Billing.WebApi.Controllers;

[ApiController]
[Route("orders")]
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
    }
}
