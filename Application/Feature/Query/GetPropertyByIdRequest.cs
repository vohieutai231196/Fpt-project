using Application.Models;
using Application.Responsitories;
using AutoMapper;
using Domain;
using MediatR;

namespace Application.Feature.Query
{
    public class GetPropertyByIdRequest : IRequest<PropertyDto>
    {
        public int PropertyId {  get; set; }
        public GetPropertyByIdRequest(int propertyId)
        {
            PropertyId = propertyId;
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
