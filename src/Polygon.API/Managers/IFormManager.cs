using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Polygon.API.Resources;
using Polygon.Domain.Entities;

namespace Polygon.API.Managers
{
    public interface IFormManager
    {
        Task<FormDataResponse> AddFormData(FormDataRequest request, int schemaId,
            CancellationToken cancellationToken);

        Task<List<FormDataResponse>> GetFormData(int schemaId, CancellationToken cancellationToken);
    }
}