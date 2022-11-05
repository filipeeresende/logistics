using Logistics.Application.v1.Controllers.Base;
using Logistics.Domain.Constants;
using Logistics.Domain.Dto.Orders;
using Logistics.Domain.Interfaces.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Logistics.Application.v1.Controllers
{
    [ApiVersion("1.0")]
    [Route("/v{version:apiVersion}/order/")]
    public class OrderController : MainController
    {
        private readonly IOrderService _orderService;

        public OrderController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        [SwaggerOperation("Returns a data base order by id")]
        [SwaggerResponse(StatusCodes.Status200OK, "", typeof(OrderResponse))]
        [SwaggerResponse(StatusCodes.Status404NotFound, ReturnMessageOrder.MessageOrderNotFound, typeof(string))]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetOrderById(int id)
        {
            return Ok(await _orderService.GetOrderById(id));
        }

        [SwaggerOperation("List all database order")]
        [SwaggerResponse(StatusCodes.Status200OK, "", typeof(IList<OrdersResponse>))]
        [SwaggerResponse(StatusCodes.Status404NotFound, ReturnMessageOrder.MessageOrdersNotFound, typeof(string))]
        [HttpGet]
        public async Task<IActionResult> GetOrders()
        {
            return Ok(await _orderService.GetOrders());
        }

        [SwaggerOperation("Insert new order")]
        [SwaggerResponse(StatusCodes.Status200OK, ReturnMessageOrder.MessageOrdersRegistered, typeof(string))]
        [HttpPost]
        public async Task<IActionResult> InsertOrder(InsertOrderRequest order)
        {
            return Ok(await _orderService.InsertOrder(order));
        }

        [SwaggerOperation("Delete an order by id")]
        [SwaggerResponse(StatusCodes.Status200OK, ReturnMessageOrder.MessageOrdersDelete, typeof(string))]
        [SwaggerResponse(StatusCodes.Status404NotFound, ReturnMessageOrder.MessageOrderNotFound, typeof(string))]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteOrder(int id)
        {
            return Ok(await _orderService.DeleteOrder(id));

        }
    }
}
