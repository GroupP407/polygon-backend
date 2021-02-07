using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.EntityFrameworkCore;
using Polygon.API.Resources;
using Polygon.API.Services.SchemaService;
using Polygon.Domain.Entities;

namespace Polygon.API.Managers
{
    public class SchemaManager : ISchemaManager
    {
        private readonly IMapper _mapper;
        private readonly ISchemaService _schemaService;

        public SchemaManager(IMapper mapper, ISchemaService schemaService)
        {
            _mapper = mapper;
            _schemaService = schemaService;
        }

        public async Task<FormSchemaResponse> AddSchema(FormSchemaRequest request, CancellationToken cancellationToken)
        {
            var formSchema = FormSchema.Create();

            _mapper.Map(request, formSchema);

            await _schemaService.AddSchema(formSchema, cancellationToken);

            return _mapper.Map<FormSchemaResponse>(formSchema);
        }

        public async Task PatchSchema(int id, JsonPatchDocument<FormSchemaRequest> patchDocument,
            CancellationToken cancellationToken)
        {
            var formSchema = await _schemaService.GetSchema(id, cancellationToken: cancellationToken);

            var formSchemaRequest = _mapper.Map<FormSchemaRequest>(formSchema);
            patchDocument.ApplyTo(formSchemaRequest);
            _mapper.Map(formSchemaRequest, formSchema);

            await _schemaService.UpdateSchema(formSchema, cancellationToken);
        }

        public Task<List<FormSchemaResponse>> GetSchemas(CancellationToken cancellationToken) =>
            _mapper.ProjectTo<FormSchemaResponse>(_schemaService.GetSchemas())
                .ToListAsync(cancellationToken: cancellationToken);

        public async Task<FormSchemaResponse> GetSchema(int id, CancellationToken cancellationToken)
        {
            var formSchema = await _schemaService.GetSchema(id, cancellationToken: cancellationToken);
            return _mapper.Map<FormSchemaResponse>(formSchema);
        }

        public async Task DeleteSchema(int id, CancellationToken cancellationToken)
        {
            var formSchema = await _schemaService.GetSchema(id, true, cancellationToken);
            await _schemaService.DeleteSchema(formSchema, cancellationToken);
        }
    }
}