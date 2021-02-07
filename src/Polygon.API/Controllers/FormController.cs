using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Polygon.API.Managers;
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
        private readonly IFormManager _formManager;


        public FormController(IFormManager formManager)
        {
            _formManager = formManager;
        }

        [HttpPost]
        [ProducesResponseType(typeof(FormDataResponse), StatusCodes.Status201Created)]
        public async Task<IActionResult> AddFormData(FormDataRequest request, int schemaId,
            CancellationToken cancellationToken)
        {
            var formData = await _formManager.AddFormData(request, schemaId, cancellationToken);
            return Ok(formData);
        }

        [HttpGet]
        [ProducesResponseType(typeof(FormDataResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> GetFormData(int schemaId, CancellationToken cancellationToken)
        {
            var formData = await _formManager.GetFormData(schemaId, cancellationToken);

            if (!formData.Any())
            {
                return NoContent();
            }

            return Ok(formData);
        }
    }
}