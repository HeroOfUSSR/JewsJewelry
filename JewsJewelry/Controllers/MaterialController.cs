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
    /// CRUD контроллер по работе с материалами
    /// </summary>
    [ApiController]
    [Route("[Controller]")]
    [ApiExplorerSettings(GroupName = "Material")]
    public class MaterialController : ControllerBase
    {
        private readonly IMaterialService MaterialService;
        private readonly IMapper mapper;

        public MaterialController(IMaterialService MaterialService, IMapper mapper)
        {
            this.MaterialService = MaterialService;
            this.mapper = mapper;
        }

        /// <summary>
        /// Получить список материалов
        /// </summary>
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<MaterialResponse>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
        {
            var result = await MaterialService.GetAllAsync(cancellationToken);
            return Ok(result.Select(x => mapper.Map<MaterialResponse>(x)));
        }

        /// <summary>
        /// Получить материал по айди
        /// </summary>
        [HttpGet("{id:guid}")]
        [ProducesResponseType(typeof(MaterialResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(APIExc), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetById([Required] Guid id, CancellationToken cancellationToken)
        {
            var item = await MaterialService.GetByIdAsync(id, cancellationToken);
            return Ok(mapper.Map<MaterialResponse>(item));
        }

        /// <summary>
        /// Добавить материал
        /// </summary>
        [HttpPost]
        [ProducesResponseType(typeof(MaterialResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(APIExcValidation), StatusCodes.Status409Conflict)]
        [ProducesResponseType(typeof(APIExc), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Add([FromBody] CreateMaterialReq model, CancellationToken cancellationToken)
        {
            var materialModel = mapper.Map<MaterialModel>(model);
            var result = await MaterialService.AddAsync(materialModel, cancellationToken);
            return Ok(mapper.Map<MaterialResponse>(result));
        }

        /// <summary>
        /// Изменить материал
        /// </summary>
        [HttpPut]
        [ProducesResponseType(typeof(MaterialResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(APIExcValidation), StatusCodes.Status409Conflict)]
        [ProducesResponseType(typeof(APIExc), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(APIExc), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Edit(MaterialRequest request, CancellationToken cancellationToken)
        {
            var model = mapper.Map<MaterialModel>(request);
            var result = await MaterialService.EditAsync(model, cancellationToken);
            return Ok(mapper.Map<MaterialResponse>(result));
        }

        /// <summary>
        /// Удалить материал по айди
        /// </summary>
        [HttpDelete("{id:guid}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(APIExc), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(APIExc), StatusCodes.Status417ExpectationFailed)]
        public async Task<IActionResult> Delete([Required] Guid id, CancellationToken cancellationToken)
        {
            await MaterialService.DeleteAsync(id, cancellationToken);
            return Ok();
        }
    }
}
