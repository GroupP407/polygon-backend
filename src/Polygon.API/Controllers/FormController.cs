using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Polygon.API.Resources;
using Polygon.API.Services.FormService;
using Polygon.API.Services.SchemaService;
using Polygon.Infrastructure;

namespace Polygon.API.Controllers
{
    [Route("from")]
    [ApiController]
    public class FormController : ControllerBase
    {
        private readonly ApplicationContext _db;
        private readonly IMapper _mapper;
        private readonly ILogger<FormController> _logger;
        private readonly ISchemaService _schemaService;
        private readonly IFormService _formService;


        public FormController(ApplicationContext db, IMapper mapper, ILogger<FormController> logger, ISchemaService schemaService, IFormService formService)
        {
            _db = db;
            _mapper = mapper;
            _logger = logger;
            _schemaService = schemaService;
            _formService = formService;
        }
        
        [HttpPost]
        [ProducesResponseType(typeof(FormSchemaResponse), StatusCodes.Status201Created)]
        public async Task<IActionResult> AddFormData(FormSchemaRequest request,
            CancellationToken cancellationToken)
        {
            var response = await _schemaService.AddSchema(request, cancellationToken);
            return Created(Url.Action("GetFormData", new {response.Id}), response);
        }
        
        
        [HttpGet("{id:int}")]
        [ProducesResponseType(typeof(FormDataResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetFormData(int id)
        {
            var response = await _formService.GetFormData(id);

            if (response is null)
            {
                return NotFound();
            }
            
            return Ok(response);
        }


        [HttpGet]
        [ProducesResponseType(typeof(FormDataResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> GetFormDatas()
        {
            var formDatas = await _formService.GetFormDatas();

            if (!formDatas.Any())
            {
                return NoContent();
            }

            return Ok(formDatas);
        }
        
    }
}