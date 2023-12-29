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
    /// CRUD контроллер по работе с ювелирными изделиями
    /// </summary>
    [ApiController]
    [Route("[Controller]")]
    [ApiExplorerSettings(GroupName = "Jewelry")]
    public class JewelryController : ControllerBase
    {
        private readonly IJewelryService JewelryService;
        private readonly IMapper mapper;

        public JewelryController(IJewelryService JewelryService, IMapper mapper)
        {
            this.JewelryService = JewelryService;
            this.mapper = mapper;
        }

        /// <summary>
        /// Получить список изделий
        /// </summary>
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<JewelryResponse>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
        {
            var result = await JewelryService.GetAllAsync(cancellationToken);
            return Ok(result.Select(x => mapper.Map<JewelryResponse>(x)));
        }

        /// <summary>
        /// Получить изделия по айди
        /// </summary>
        [HttpGet("{id:guid}")]
        [ProducesResponseType(typeof(JewelryResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(APIExc), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetById([Required] Guid id, CancellationToken cancellationToken)
        {
            var item = await JewelryService.GetByIdAsync(id, cancellationToken);
            return Ok(mapper.Map<JewelryResponse>(item));
        }

        /// <summary>
        /// Добавить изделие
        /// </summary>
        [HttpPost]
        [ProducesResponseType(typeof(JewelryResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(APIExcValidation), StatusCodes.Status409Conflict)]
        [ProducesResponseType(typeof(APIExc), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Add([FromBody] CreateJewelryReq model, CancellationToken cancellationToken)
        {
            var jewelryModel = mapper.Map<JewelryModel>(model);
            var result = await JewelryService.AddAsync(jewelryModel, cancellationToken);
            return Ok(mapper.Map<JewelryResponse>(result));
        }

        /// <summary>
        /// Изменить изделие
        /// </summary>
        [HttpPut]
        [ProducesResponseType(typeof(JewelryResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(APIExcValidation), StatusCodes.Status409Conflict)]
        [ProducesResponseType(typeof(APIExc), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(APIExc), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Edit(JewelryRequest request, CancellationToken cancellationToken)
        {
            var model = mapper.Map<JewelryModel>(request);
            var result = await JewelryService.EditAsync(model, cancellationToken);
            return Ok(mapper.Map<JewelryResponse>(result));
        }

        /// <summary>
        /// Удалить изделие по айди
        /// </summary>
        [HttpDelete("{id:guid}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(APIExc), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(APIExc), StatusCodes.Status417ExpectationFailed)]
        public async Task<IActionResult> Delete([Required] Guid id, CancellationToken cancellationToken)
        {
            await JewelryService.DeleteAsync(id, cancellationToken);
            return Ok();
        }
    }
}
