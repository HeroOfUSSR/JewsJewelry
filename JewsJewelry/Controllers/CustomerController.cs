using AutoMapper;
using JewsJewelry.API.Exceptions;
using JewsJewelry.API.Models.CreateRequest;
using JewsJewelry.API.Models.Request;
using JewsJewelry.API.Models.Response;
using JewsJewelry.Services.Contracts.Interface;
using JewsJewelry.Services.Contracts.Models;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace JewsJewelry.API.Controllers
{
    /// <summary>
    /// CRUD контроллер по работе с заказчиками
    /// </summary>
    [ApiController]
    [Route("[Controller]")]
    [ApiExplorerSettings(GroupName = "Customer")]
    public class CustomerController : ControllerBase
    {
        private readonly ICustomerService CustomerService;
        private readonly IMapper mapper;

        public CustomerController(ICustomerService CustomerService, IMapper mapper)
        {
            this.CustomerService = CustomerService;
            this.mapper = mapper;
        }

        /// <summary>
        /// Получить список заказчиков
        /// </summary>
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<CustomerResponse>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
        {
            var result = await CustomerService.GetAllAsync(cancellationToken);
            return Ok(result.Select(x => mapper.Map<CustomerResponse>(x)));
        }

        /// <summary>
        /// Получить заказчика по айди
        /// </summary>
        [HttpGet("{id:guid}")]
        [ProducesResponseType(typeof(CustomerResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(APIExc), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetById([Required] Guid id, CancellationToken cancellationToken)
        {
            var item = await CustomerService.GetByIdAsync(id, cancellationToken);
            return Ok(mapper.Map<CustomerResponse>(item));
        }

        /// <summary>
        /// Добавить заказчика
        /// </summary>
        [HttpPost]
        [ProducesResponseType(typeof(CustomerResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(APIExcValidation), StatusCodes.Status409Conflict)]
        [ProducesResponseType(typeof(APIExc), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Add([FromBody] CreateCustomerReq model, CancellationToken cancellationToken)
        {
            var customerModel = mapper.Map<CustomerModel>(model);
            var result = await CustomerService.AddAsync(customerModel, cancellationToken);
            return Ok(mapper.Map<CustomerResponse>(result));
        }

        /// <summary>
        /// Изменить заказчика
        /// </summary>
        [HttpPut]
        [ProducesResponseType(typeof(CustomerResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(APIExcValidation), StatusCodes.Status409Conflict)]
        [ProducesResponseType(typeof(APIExc), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(APIExc), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Edit(CustomerRequest request, CancellationToken cancellationToken)
        {
            var model = mapper.Map<CustomerModel>(request);
            var result = await CustomerService.EditAsync(model, cancellationToken);
            return Ok(mapper.Map<CustomerResponse>(result));
        }

        /// <summary>
        /// Удалить заказчика по айди
        /// </summary>
        [HttpDelete("{id:guid}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(APIExc), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(APIExc), StatusCodes.Status417ExpectationFailed)]
        public async Task<IActionResult> Delete([Required] Guid id, CancellationToken cancellationToken)
        {
            await CustomerService.DeleteAsync(id, cancellationToken);
            return Ok();
        }
    }
}
