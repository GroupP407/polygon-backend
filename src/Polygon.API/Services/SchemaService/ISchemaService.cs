using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.JsonPatch;
using OneOf;
using OneOf.Types;
using Polygon.API.Resources;
using Polygon.Domain.Entities;

namespace Polygon.API.Services.SchemaService
{
    public interface ISchemaService
    {
        Task AddSchema(FormSchema request, CancellationToken cancellationToken);
        Task UpdateSchema(FormSchema formSchema,
            CancellationToken cancellationToken = default);

        Task DeleteSchema(FormSchema formSchema, CancellationToken cancellationToken);

        IQueryable<FormSchema> GetSchemas(bool includeDeleted = default);

        Task<FormSchema> GetSchema(int id, bool includeDeleted = default, CancellationToken cancellationToken = default);
    }
}