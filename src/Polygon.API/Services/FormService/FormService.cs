using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Nest;
using OneOf;
using OneOf.Types;
using Polygon.API.Resources;
using Polygon.Domain.Entities;
using Polygon.Infrastructure;

namespace Polygon.API.Services.FormService
{
    public class FormService : IFormService
    {
        private readonly ApplicationContext _db;
        private readonly IMapper _mapper;
        private readonly ILogger<FormService> _logger;
        private readonly IElasticClient _elasticClient;

        public FormService(ApplicationContext db, IMapper mapper, ILogger<FormService> logger, IElasticClient elasticClient)
        {
            _db = db;
            _mapper = mapper;
            _logger = logger;
            _elasticClient = elasticClient;
        }


        public async Task<FormDataResponse> AddFormData(FormDataRequest request, int schemaId, CancellationToken cancellationToken)
        {
            var schema = await _db.FormSchemas.FirstOrDefaultAsync(s => s.Id == schemaId, cancellationToken: cancellationToken);
            if (schema is null)
            {
                // TODO вернуть ошибку
            }
            
            var fromJsonAsync = await NJsonSchema.JsonSchema.FromJsonAsync(schema.Schema.ToString(), cancellationToken);
            var errors = fromJsonAsync.Validate(request.JsonData.ToString());

            if (errors.Count > 0)
            {
                // TODO вернуть ошибки
            }
            
            var formData = new FormData(DateTimeOffset.Now);
            
            
            
            _mapper.Map(request, formData);
            schema.FormDatas.Add(formData);
            await _db.SaveChangesAsync(cancellationToken);
            var indexDocumentAsync = await _elasticClient.IndexDocumentAsync(formData, cancellationToken);
            
            
            var response = _mapper.Map<FormDataResponse>(formData);
            return response;
        }
        
        public async Task<List<FormDataResponse>> GetFormDatas()
        {
            return await _mapper.ProjectTo<FormDataResponse>(_db.FormDatas).ToListAsync();
        }

        public async Task<FormDataResponse?> GetFormData(int id)
        {
            return await _mapper.ProjectTo<FormDataResponse>(_db.FormDatas).FirstOrDefaultAsync(response => response.Id == id);
        }
    }
}