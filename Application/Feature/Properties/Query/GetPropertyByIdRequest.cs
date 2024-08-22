using Application.Models;
using Application.PipelineBehaviours.Contracts;
using Application.Responsitories;
using AutoMapper;
using Domain;
using MediatR;

namespace Application.Feature.Properties.Query
{
    public class GetPropertyByIdRequest : IRequest<PropertyDto>, ICacheable
    {
        public int PropertyId { get; set; }
        public string CacheKey { get ; set; }
        public bool BypassCache { get ; set ; }
        public TimeSpan? SlidingExpiration { get ; set; }

        public GetPropertyByIdRequest(int propertyId)
        {
            PropertyId = propertyId; 
            CacheKey = $"GetPropertyById:{PropertyId}";
        }
        public class GetPropertyByIdRequestHandler : IRequestHandler<GetPropertyByIdRequest, PropertyDto>
        {
            private readonly IPropertyRepon _propertyRepon;
            private readonly IMapper _mapper;

            public GetPropertyByIdRequestHandler(IPropertyRepon propertyRepon, IMapper mapper)
            {
                _propertyRepon = propertyRepon;
                _mapper = mapper;
            }

            public async Task<PropertyDto> Handle(GetPropertyByIdRequest request, CancellationToken cancellationToken)
            {
                Property propertyInDb = await _propertyRepon.GetByIdAsync(request.PropertyId);
                if (propertyInDb != null)
                { //Mapping Property to PropertyDto
                    PropertyDto propertyDto = _mapper.Map<PropertyDto>(propertyInDb);
                    return propertyDto;
                }
                return null;

            }
        }
    }
}
