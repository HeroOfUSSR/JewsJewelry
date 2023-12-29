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
    /// CRUD контроллер по работе с мастерскими
    /// </summary>
    [ApiController]
    [Route("[Controller]")]
    [ApiExplorerSettings(GroupName = "Workshop")]
    public class WorkshopController : ControllerBase
    {
        private readonly IWorkshopService WorkshopService;
        private readonly IMapper mapper;

        public WorkshopController(IWorkshopService WorkshopService, IMapper mapper)
        {
            this.WorkshopService = WorkshopService;
            this.mapper = mapper;
        }

        /// <summary>
        /// Получить список мастерских
        /// </summary>
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<WorkshopResponse>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
        {
            var result = await WorkshopService.GetAllAsync(cancellationToken);
            return Ok(result.Select(x => mapper.Map<WorkshopResponse>(x)));
        }

        /// <summary>
        /// Получить мастерскую по айди
        /// </summary>
        [HttpGet("{id:guid}")]
        [ProducesResponseType(typeof(WorkshopResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(APIExc), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetById([Required] Guid id, CancellationToken cancellationToken)
        {
            var item = await WorkshopService.GetByIdAsync(id, cancellationToken);
            return Ok(mapper.Map<WorkshopResponse>(item));
        }

        /// <summary>
        /// Добавить мастерскую
        /// </summary>
        [HttpPost]
        [ProducesResponseType(typeof(WorkshopResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(APIExcValidation), StatusCodes.Status409Conflict)]
        [ProducesResponseType(typeof(APIExc), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Add([FromBody] CreateWorkshopReq model, CancellationToken cancellationToken)
        {
            var workshopModel = mapper.Map<WorkshopModel>(model);
            var result = await WorkshopService.AddAsync(workshopModel, cancellationToken);
            return Ok(mapper.Map<WorkshopResponse>(result));
        }

        /// <summary>
        /// Изменить мастерскую
        /// </summary>
        [HttpPut]
        [ProducesResponseType(typeof(WorkshopResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(APIExcValidation), StatusCodes.Status409Conflict)]
        [ProducesResponseType(typeof(APIExc), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(APIExc), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Edit(WorkshopRequest request, CancellationToken cancellationToken)
        {
            var model = mapper.Map<WorkshopModel>(request);
            var result = await WorkshopService.EditAsync(model, cancellationToken);
            return Ok(mapper.Map<WorkshopResponse>(result));
        }

        /// <summary>
        /// Удалить мастерскую по айди
        /// </summary>
        [HttpDelete("{id:guid}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(APIExc), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(APIExc), StatusCodes.Status417ExpectationFailed)]
        public async Task<IActionResult> Delete([Required] Guid id, CancellationToken cancellationToken)
        {
            await WorkshopService.DeleteAsync(id, cancellationToken);
            return Ok();
        }
    }
}
