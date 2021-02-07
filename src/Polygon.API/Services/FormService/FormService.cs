using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using FluentValidation;
using FluentValidation.Results;
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

        public FormService(ApplicationContext db, IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
        }

        public async Task ValidateFormDataRequest(FormDataRequest request, FormSchema schema, CancellationToken cancellationToken)
        {
            var draft = await NJsonSchema.JsonSchema.FromJsonAsync(schema.Schema.ToString(), cancellationToken);
            var errors = draft.Validate(request.JsonData);

            if (errors.Any())
            {
                throw new ValidationException(errors.Select(error =>
                    new ValidationFailure(error.Path, error.Kind.ToString())));
            }
        }
        
        public IQueryable<FormData> GetFormData(int schemaId)
        {
            return _db.FormData.Where(data => data.FormSchemaId == schemaId);
        }
    }
}