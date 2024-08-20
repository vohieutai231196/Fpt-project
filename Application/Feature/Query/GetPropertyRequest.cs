using Application.Models;
using Application.Responsitories;
using AutoMapper;
using Domain;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Application.Feature.Query
{
    public class GetPropertyRequest : IRequest<List<PropertyDto>>
    {

    }
    public class GetPropertyRequestHandler : IRequestHandler<GetPropertyRequest, List<PropertyDto>>
    {
        private readonly IPropertyRepon _propertyRepon;

        private readonly IMapper _mapper;
        public GetPropertyRequestHandler(IPropertyRepon propertyRepon, IMapper mapper) 
        { 
            _propertyRepon = propertyRepon;
            _mapper = mapper;
        }
        public async Task<List<PropertyDto>> Handle(GetPropertyRequest request, CancellationToken cancellationToken)
        {
            List<Property> properties = await _propertyRepon.GetAllAsync();
            if (properties != null)
            {
                //mapper
                List<PropertyDto> propertyDtos = _mapper.Map<List<PropertyDto>>(properties);
                return propertyDtos;
            }
            return null;
        }
    }
}
