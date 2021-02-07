using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.JsonPatch;
using Polygon.API.Resources;

namespace Polygon.API.Managers
{
    public interface ISchemaManager
    {
        Task<FormSchemaResponse> AddSchema(FormSchemaRequest request, CancellationToken cancellationToken);

        Task PatchSchema(int id, JsonPatchDocument<FormSchemaRequest> patchDocument,
            CancellationToken cancellationToken);

        Task<List<FormSchemaResponse>> GetSchemas(CancellationToken cancellationToken);
        Task<FormSchemaResponse> GetSchema(int id, CancellationToken cancellationToken);
        Task DeleteSchema(int id, CancellationToken cancellationToken);
    }
}