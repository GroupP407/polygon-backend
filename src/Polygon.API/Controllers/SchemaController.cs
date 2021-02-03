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

        public SchemaController(ApplicationContext db, IMapper mapper, ILogger<SchemaController> logger)
        {
            _db = db;
            _mapper = mapper;
            _logger = logger;
        }

        [HttpPost]
        [ProducesResponseType(typeof(FormSchemaResponse), StatusCodes.Status201Created)]
        public async Task<IActionResult> AddSchema(FormSchemaRequest request,
            CancellationToken cancellationToken)
        {
            var formSchema = new FormSchema(DateTimeOffset.Now);

            _logger.LogInformation("test {@FormSchema}", formSchema);
            
            _mapper.Map(request, formSchema);

            await _db.FormSchemas.AddAsync(formSchema, cancellationToken);

            await _db.SaveChangesAsync(cancellationToken);

            var response = _mapper.Map<FormSchemaResponse>(formSchema);
            
            return Created(Url.Action("GetSchema", new {response.Id}), response);
        }

        [HttpPatch("{id:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> PatchSchema(int id, JsonPatchDocument<FormSchemaRequest> patchDocument,
            CancellationToken cancellationToken)
        {
            var formSchema =
                await _db.FormSchemas.FirstOrDefaultAsync(s => s.Id == id, cancellationToken);

            if (formSchema is null)
            {
                return NotFound();
            }
            
            var resource = _mapper.Map<FormSchemaRequest>(formSchema);

            patchDocument.ApplyTo(resource);

            _mapper.Map(resource, formSchema);

            await _db.SaveChangesAsync(cancellationToken);

            return NoContent();
        }

        [HttpDelete("{id:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteSchema(int id, CancellationToken cancellationToken)
        {
            var resource =
                await _db.FormSchemas.IgnoreQueryFilters().FirstOrDefaultAsync(s => s.Id == id, cancellationToken);

            if (resource is null)
            {
                return NotFound();
            }

            if (!resource.IsDeleted)
            {
                resource.IsDeleted = true;
                await _db.SaveChangesAsync(cancellationToken);
            }
            
            return NoContent();
        }

        [HttpGet]
        [ProducesResponseType(typeof(FormSchemaResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> GetSchemas()
        {
            var response = await _mapper.ProjectTo<FormSchemaResponse>(_db.FormSchemas).ToListAsync();
            
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
            var response = await _mapper.ProjectTo<FormSchemaResponse>(_db.FormSchemas)
                .FirstOrDefaultAsync(schema => schema.Id == id);

            if (response is null)
            {
                return NotFound();
            }
            
            return Ok(response);
        }
    }
}