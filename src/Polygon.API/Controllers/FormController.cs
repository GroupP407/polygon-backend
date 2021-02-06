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
    [Route("schema/{schemaId:int}/form")]
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
        [ProducesResponseType(typeof(FormDataResponse), StatusCodes.Status201Created)]
        public async Task<IActionResult> AddFormData(FormDataRequest request, int schemaId,
            CancellationToken cancellationToken)
        {
            var response = await _formService.AddFormData(request, schemaId, cancellationToken);
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