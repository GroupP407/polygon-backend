using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.JsonPatch;
using OneOf;
using OneOf.Types;
using Polygon.API.Resources;

namespace Polygon.API.Services.SchemaService
{
    public interface ISchemaService
    {
        Task<FormSchemaResponse> AddSchema(FormSchemaRequest request, CancellationToken cancellationToken);
        Task<OneOf<Success, NotFound>> PatchSchema(int id, JsonPatchDocument<FormSchemaRequest> patchDocument,
            CancellationToken cancellationToken);

        Task<OneOf<Success, NotFound>> DeleteSchema(int id, CancellationToken cancellationToken);

        Task<List<FormSchemaResponse>> GetSchemas();

        Task<FormSchemaResponse?> GetSchema(int id);

    }
}