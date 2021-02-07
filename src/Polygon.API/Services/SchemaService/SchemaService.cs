using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.EntityFrameworkCore;
using OneOf;
using OneOf.Types;
using Polygon.API.Controllers;
using Polygon.API.Resources;
using Polygon.Domain.Entities;
using Polygon.Domain.Exceptions;
using Polygon.Infrastructure;

namespace Polygon.API.Services.SchemaService
{
    public class SchemaService : ISchemaService
    {
        private readonly ApplicationContext _db;

        public SchemaService(ApplicationContext db)
        {
            _db = db;
        }


        public async Task AddSchema(FormSchema formSchema, CancellationToken cancellationToken)
        {
            await _db.FormSchemas.AddAsync(formSchema, cancellationToken);
            await _db.SaveChangesAsync(cancellationToken);
        }

        public async Task UpdateSchema(FormSchema formSchema, CancellationToken cancellationToken = default)
        {
            _db.Update(formSchema);
            await _db.SaveChangesAsync(cancellationToken);
        }

        public async Task DeleteSchema(FormSchema formSchema, CancellationToken cancellationToken)
        {
            if (!formSchema.IsDeleted)
            {
                formSchema.IsDeleted = true;
                await _db.SaveChangesAsync(cancellationToken);
            }
        }

        public IQueryable<FormSchema> GetSchemas(bool includeDeleted = default)
        {
            IQueryable<FormSchema> formSchemaQuery = _db.FormSchemas;
            
            if (includeDeleted is false)
            {
                formSchemaQuery = formSchemaQuery.Where(schema => !schema.IsDeleted);
            }

            return formSchemaQuery;
        }

        public async Task<FormSchema> GetSchema(int id, bool includeDeleted = default, CancellationToken cancellationToken = default)
        {
            var formSchema = await GetSchemas(includeDeleted).FirstOrDefaultAsync(schema => schema.Id == id, cancellationToken: cancellationToken);

            if (formSchema is null)
            {
                throw new EntityNotFoundException();
            }
            
            return formSchema;
        }
    }
}