using AutoMapper;
using JewsJewelry.API.Exceptions;
using JewsJewelry.API.Models.CreateRequest;
using JewsJewelry.API.Models.Request;
using JewsJewelry.API.Models.Response;
using JewsJewelry.Services.Contracts.Interface;
using JewsJewelry.Services.Contracts.Models;
using JewsJewelry.Services.Contracts.ModelsRequest;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace JewsJewelry.API.Controllers
{
    /// <summary>
    /// CRUD контроллер по работе с заказами
    /// </summary>
    [ApiController]
    [Route("[Controller]")]
    [ApiExplorerSettings(GroupName = "Order")]
    public class OrderController : ControllerBase
    {
        private readonly IOrderService OrderService;
        private readonly IMapper mapper;

        public OrderController(IOrderService OrderService, IMapper mapper)
        {
            this.OrderService = OrderService;
            this.mapper = mapper;
        }

        /// <summary>
        /// Получить список заказов
        /// </summary>
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<OrderResponse>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
        {
            var result = await OrderService.GetAllAsync(cancellationToken);
            return Ok(result.Select(x => mapper.Map<OrderResponse>(x)));
        }

        /// <summary>
        /// Получить заказ по айди
        /// </summary>
        [HttpGet("{id:guid}")]
        [ProducesResponseType(typeof(OrderResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(APIExc), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetById([Required] Guid id, CancellationToken cancellationToken)
        {
            var item = await OrderService.GetByIdAsync(id, cancellationToken);
            return Ok(mapper.Map<OrderResponse>(item));
        }

        /// <summary>
        /// Добавить заказ
        /// </summary>
        [HttpPost]
        [ProducesResponseType(typeof(OrderResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(APIExcValidation), StatusCodes.Status409Conflict)]
        [ProducesResponseType(typeof(APIExc), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Add([FromBody] CreateOrderReq model, CancellationToken cancellationToken)
        {
            var orderModel = mapper.Map<OrderRequestModel>(model);
            var result = await OrderService.AddAsync(orderModel, cancellationToken);
            return Ok(mapper.Map<OrderResponse>(result));
        }

        /// <summary>
        /// Изменить заказ
        /// </summary>
        [HttpPut]
        [ProducesResponseType(typeof(OrderResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(APIExcValidation), StatusCodes.Status409Conflict)]
        [ProducesResponseType(typeof(APIExc), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(APIExc), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Edit(OrderRequest request, CancellationToken cancellationToken)
        {
            var model = mapper.Map<OrderRequestModel>(request);
            var result = await OrderService.EditAsync(model, cancellationToken);
            return Ok(mapper.Map<OrderResponse>(result));
        }

        /// <summary>
        /// Удалить заказ по айди
        /// </summary>
        [HttpDelete("{id:guid}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(APIExc), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(APIExc), StatusCodes.Status417ExpectationFailed)]
        public async Task<IActionResult> Delete([Required] Guid id, CancellationToken cancellationToken)
        {
            await OrderService.DeleteAsync(id, cancellationToken);
            return Ok();
        }
    }
}
