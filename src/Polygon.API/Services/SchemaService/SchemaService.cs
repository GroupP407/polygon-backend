using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using OneOf;
using OneOf.Types;
using Polygon.API.Controllers;
using Polygon.API.Resources;
using Polygon.Domain.Entities;
using Polygon.Infrastructure;

namespace Polygon.API.Services.SchemaService
{
    public class SchemaService : ISchemaService
    {
        private readonly ApplicationContext _db;
        private readonly IMapper _mapper;
        private readonly ILogger<SchemaService> _logger;

        public SchemaService(ApplicationContext db, IMapper mapper, ILogger<SchemaService> logger)
        {
            _db = db;
            _mapper = mapper;
            _logger = logger;
        }


        public async Task<FormSchemaResponse> AddSchema(FormSchemaRequest request, CancellationToken cancellationToken)
        {
            var formSchema = new FormSchema(DateTimeOffset.Now);
            
            _mapper.Map(request, formSchema);

            await _db.FormSchemas.AddAsync(formSchema, cancellationToken);

            await _db.SaveChangesAsync(cancellationToken);

            var response = _mapper.Map<FormSchemaResponse>(formSchema);
            return response;
        }

        public async Task<OneOf<Success, NotFound>>  PatchSchema(int id, JsonPatchDocument<FormSchemaRequest> patchDocument, CancellationToken cancellationToken)
        {
            var formSchema =
                await _db.FormSchemas.FirstOrDefaultAsync(s => s.Id == id, cancellationToken);

            if (formSchema is null)
            {
                return new NotFound();
            }
            
            var resource = _mapper.Map<FormSchemaRequest>(formSchema);

            patchDocument.ApplyTo(resource);
            
            _mapper.Map(resource, formSchema);
            await _db.SaveChangesAsync(cancellationToken);
            
            return new Success();
        }

        public async Task<OneOf<Success, NotFound>> DeleteSchema(int id, CancellationToken cancellationToken)
        {
            var resource =
                await _db.FormSchemas.IgnoreQueryFilters().FirstOrDefaultAsync(s => s.Id == id, cancellationToken);

            if (resource is null)
            {
                return new NotFound();
            }

            if (!resource.IsDeleted)
            {
                resource.IsDeleted = true;
                await _db.SaveChangesAsync(cancellationToken);
            }
            return new Success();
        }

        public async Task<List<FormSchemaResponse>> GetSchemas()
        {
            return await _mapper.ProjectTo<FormSchemaResponse>(_db.FormSchemas).ToListAsync();
        }

        public Task<FormSchemaResponse?> GetSchema(int id)
        {
            return _mapper.ProjectTo<FormSchemaResponse>(_db.FormSchemas)
                .FirstOrDefaultAsync(schema => schema.Id == id);
        }
    }
}