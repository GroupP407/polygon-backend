using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json.Linq;
using NJsonSchema;
using Polygon.API.Resources;
using Polygon.API.Services.SchemaService;
using Polygon.Domain.Entities;
using Polygon.Infrastructure;

namespace Polygon.API.Controllers
{
    [Route("schema")]
    [ApiController]
    public class SchemaController : ControllerBase
    {
        private readonly ApplicationContext _db;
        private readonly IMapper _mapper;
        private readonly ILogger<SchemaController> _logger;
        private readonly ISchemaService _schemaService;

        public SchemaController(ApplicationContext db, IMapper mapper, ILogger<SchemaController> logger, ISchemaService schemaService)
        {
            _db = db;
            _mapper = mapper;
            _logger = logger;
            _schemaService = schemaService;
        }

        [HttpPost]
        [ProducesResponseType(typeof(FormSchemaResponse), StatusCodes.Status201Created)]
        public async Task<IActionResult> AddSchema(FormSchemaRequest request,
            CancellationToken cancellationToken)
        {
            var response = await _schemaService.AddSchema(request, cancellationToken);
            return Created(Url.Action("GetSchema", new {response.Id}), response);
        }

        [HttpPatch("{id:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> PatchSchema(int id, JsonPatchDocument<FormSchemaRequest> patchDocument,
            CancellationToken cancellationToken)
        {
            var result = await _schemaService.PatchSchema(id, patchDocument, cancellationToken);
            return result.Match<IActionResult>(
                success => NoContent(),
                notFound => NotFound()
            );
        }

        [HttpDelete("{id:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteSchema(int id, CancellationToken cancellationToken)
        {
            var result = await _schemaService.DeleteSchema(id, cancellationToken);
            return result.Match<IActionResult>(
                success => NoContent(),
                notFound => NotFound()
            );
        }

        [HttpGet]
        [ProducesResponseType(typeof(FormSchemaResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> GetSchemas()
        {
            var response = await _schemaService.GetSchemas();
            
            if (!response.Any())
            {
                return NoContent();
            }

            return Ok(response);
        }

        [HttpGet("{id:int}")]
        [ProducesResponseType(typeof(FormSchemaResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetSchema(int id)
        {
            var response = await _schemaService.GetSchema(id);

            if (response is null)
            {
                return NotFound();
            }
            
            return Ok(response);
        }
    }
}