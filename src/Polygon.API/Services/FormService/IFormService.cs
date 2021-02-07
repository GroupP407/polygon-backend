using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.JsonPatch;
using OneOf;
using OneOf.Types;
using Polygon.API.Resources;
using Polygon.Domain.Entities;

namespace Polygon.API.Services.FormService
{
    public interface IFormService
    {
        Task ValidateFormDataRequest(FormDataRequest request, FormSchema schema, CancellationToken cancellationToken);
        IQueryable<FormData> GetFormData(int schemaId);
    }
}