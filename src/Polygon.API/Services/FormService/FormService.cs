using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
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

        public FormService(ApplicationContext db, IMapper mapper, ILogger<FormService> logger)
        {
            _db = db;
            _mapper = mapper;
            _logger = logger;
        }


        public async Task<FormDataResponse> AddFormData(FormDataRequest request, CancellationToken cancellationToken)
        {
            var formData = new FormData(DateTimeOffset.Now);
            
            _mapper.Map(request, formData);

            await _db.FormDatas.AddAsync(formData, cancellationToken);

            await _db.SaveChangesAsync(cancellationToken);

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