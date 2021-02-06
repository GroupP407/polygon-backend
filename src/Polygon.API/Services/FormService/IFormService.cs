using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.JsonPatch;
using OneOf;
using OneOf.Types;
using Polygon.API.Resources;

namespace Polygon.API.Services.FormService
{
    public interface IFormService
    {
        Task<FormDataResponse> AddFormData(FormDataRequest request, int schemaId, CancellationToken cancellationToken);
        Task<List<FormDataResponse>> GetFormDatas();
        Task<FormDataResponse?> GetFormData(int id);
    }
}