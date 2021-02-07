using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Nest;
using Polygon.API.Resources;
using Polygon.API.Services.FormService;
using Polygon.API.Services.SchemaService;
using Polygon.Domain.Entities;

namespace Polygon.API.Managers
{
    public class FormManager : IFormManager
    {
        private readonly IFormService _formService;
        private readonly ISchemaService _schemaService;
        private readonly IElasticClient _elasticClient;
        private readonly IMapper _mapper;

        public FormManager(IFormService formService, ISchemaService schemaService, IElasticClient elasticClient,
            IMapper mapper)
        {
            _formService = formService;
            _schemaService = schemaService;
            _elasticClient = elasticClient;
            _mapper = mapper;
        }

        public async Task<FormDataResponse> AddFormData(FormDataRequest request, int schemaId,
            CancellationToken cancellationToken)
        {
            var formSchema = await _schemaService.GetSchema(schemaId, cancellationToken: cancellationToken);

            await _formService.ValidateFormDataRequest(request, formSchema, cancellationToken);

            var formData = FormData.Create();

            _mapper.Map(request, formData);
            
            formSchema.AddFormData(formData);

            await _schemaService.UpdateSchema(formSchema, cancellationToken);
            
            var indexResponse = await _elasticClient.IndexDocumentAsync(formData, cancellationToken);

            return _mapper.Map<FormDataResponse>(formData);
        }

        public Task<List<FormDataResponse>> GetFormData(int schemaId, CancellationToken cancellationToken) => _mapper
            .ProjectTo<FormDataResponse>(_formService.GetFormData(schemaId))
            .ToListAsync(cancellationToken: cancellationToken);
    }
}