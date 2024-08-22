using Application.Models;
using Application.PipelineBehaviours.Contracts;
using Application.Responsitories;
using AutoMapper;
using Domain;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Application.Feature.Properties.Query
{
    public class GetPropertyRequest : IRequest<List<PropertyDto>>, ICacheable
    {

        public string CacheKey { get; set; }
        public bool BypassCache { get; set; }
        public TimeSpan? SlidingExpiration { get; set; }

        public GetPropertyRequest()
        {
            CacheKey = "GetProperties";
        }

    }
    public class GetPropertyRequestHandler : IRequestHandler<GetPropertyRequest, List<PropertyDto>>
    {
        private readonly IPropertyRepon _propertyRepon;

        private readonly IMapper _mapper;

        private ILogger<GetPropertyRequest> _logger;

        private ActivitySource _activitySource;

        public GetPropertyRequestHandler(IPropertyRepon propertyRepon, IMapper mapper, ILogger<GetPropertyRequest> logger
            , Instrumentation instrumentation)
        {
            _propertyRepon = propertyRepon;
            _mapper = mapper;
           this._logger = logger;
           this._activitySource = instrumentation.ActivitySource;
        }
        public async Task<List<PropertyDto>> Handle(GetPropertyRequest request, CancellationToken cancellationToken)
        {
            using (var activity = _activitySource.StartActivity("GetPropertyRequest", ActivityKind.Server))
            {
                List<Property> properties = await _propertyRepon.GetAllAsync();
                if (properties != null)
                {

                    activity?.SetTag("get.success", true);
                    //mapper
                    List<PropertyDto> propertyDtos = _mapper.Map<List<PropertyDto>>(properties);
                    return propertyDtos;
                }
                return null;
            }
        }
    }
}
