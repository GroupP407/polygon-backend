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
using Polygon.API.Managers;
using Polygon.API.Resources;
using Polygon.Domain.Entities;
using Polygon.Infrastructure;

namespace Polygon.API.Controllers
{
    [Route("schema")]
    [ApiController]
    public class SchemaController : ControllerBase
    {
        private readonly ISchemaManager _schemaManager;

        public SchemaController(ISchemaManager schemaManager)
        {
            _schemaManager = schemaManager;
        }

        [HttpPost]
        [ProducesResponseType(typeof(FormSchemaResponse), StatusCodes.Status201Created)]
        public async Task<IActionResult> AddSchema(FormSchemaRequest request,
            CancellationToken cancellationToken)
        {
            var response = await _schemaManager.AddSchema(request, cancellationToken);
            return Created(Url.Action("GetSchema", new {schemaId = response.Id}), response);
        }

        [HttpPatch("{schemaId:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> PatchSchema(int schemaId, JsonPatchDocument<FormSchemaRequest> patchDocument,
            CancellationToken cancellationToken)
        {
            await _schemaManager.PatchSchema(schemaId, patchDocument, cancellationToken);
            return NoContent();
        }

        [HttpDelete("{schemaId:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteSchema(int schemaId, CancellationToken cancellationToken)
        {
            await _schemaManager.DeleteSchema(schemaId, cancellationToken);
            return NoContent();
        }

        [HttpGet]
        [ProducesResponseType(typeof(FormSchemaResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> GetSchemas(CancellationToken cancellationToken)
        {
            var response = await _schemaManager.GetSchemas(cancellationToken);
            
            if (!response.Any())
            {
                return NoContent();
            }

            return Ok(response);
        }

        [HttpGet("{schemaId:int}")]
        [ProducesResponseType(typeof(FormSchemaResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetSchema(int schemaId, CancellationToken cancellationToken)
        {
            var response = await _schemaManager.GetSchema(schemaId, cancellationToken);

            return Ok(response);
        }
    }
}