using AutoMapper;
using JewsJewelry.API.Exceptions;
using JewsJewelry.API.Models.CreateRequest;
using JewsJewelry.API.Models.Request;
using JewsJewelry.API.Models.Response;
using JewsJewelry.Services.Contracts.Interface;
using JewsJewelry.Services.Contracts.Models;
using JewsJewelry.Services.Implementations;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace JewsJewelry.API.Controllers
{
    /// <summary>
    /// CRUD контроллер по работе с мастерами
    /// </summary>
    [ApiController]
    [Route("[Controller]")]
    [ApiExplorerSettings(GroupName = "Craftsman")]
    public class CraftsmanController : ControllerBase
    {
        private readonly ICraftsmanService craftsmanService;
        private readonly IMapper mapper;

        public CraftsmanController(ICraftsmanService craftsmanService, IMapper mapper)
        {
            this.craftsmanService = craftsmanService;
            this.mapper = mapper;
        }

        /// <summary>
        /// Получить список мастеров
        /// </summary>
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<CraftsmanResponse>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
        {
            var result = await craftsmanService.GetAllAsync(cancellationToken);
            return Ok(result.Select(x => mapper.Map<CraftsmanResponse>(x)));
        }

        /// <summary>
        /// Получить мастера по айди
        /// </summary>
        [HttpGet("{id:guid}")]
        [ProducesResponseType(typeof(CraftsmanResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(APIExc), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetById([Required] Guid id, CancellationToken cancellationToken)
        {
            var item = await craftsmanService.GetByIdAsync(id, cancellationToken);
            return Ok(mapper.Map<CraftsmanResponse>(item));
        }

        /// <summary>
        /// Добавить мастера
        /// </summary>
        [HttpPost]
        [ProducesResponseType(typeof(CraftsmanResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(APIExcValidation), StatusCodes.Status409Conflict)]
        [ProducesResponseType(typeof(APIExc), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Add([FromBody] CreateCraftsmanReq model, CancellationToken cancellationToken)
        {
            var craftsmanModel = mapper.Map<CraftsmanModel>(model);
            var result = await craftsmanService.AddAsync(craftsmanModel, cancellationToken);
            return Ok(mapper.Map<CraftsmanResponse>(result));
        }

        /// <summary>
        /// Изменить мастера
        /// </summary>
        [HttpPut]
        [ProducesResponseType(typeof(CraftsmanResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(APIExcValidation), StatusCodes.Status409Conflict)]
        [ProducesResponseType(typeof(APIExc), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(APIExc), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Edit(CraftsmanRequest request, CancellationToken cancellationToken)
        {
            var model = mapper.Map<CraftsmanModel>(request);
            var result = await craftsmanService.EditAsync(model, cancellationToken);
            return Ok(mapper.Map<CraftsmanResponse>(result));
        }

        /// <summary>
        /// Удалить мастера по айди
        /// </summary>
        [HttpDelete("{id:guid}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(APIExc), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(APIExc), StatusCodes.Status417ExpectationFailed)]
        public async Task<IActionResult> Delete([Required] Guid id, CancellationToken cancellationToken)
        {
            await craftsmanService.DeleteAsync(id, cancellationToken);
            return Ok();
        }
    }
}

