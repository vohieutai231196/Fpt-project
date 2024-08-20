using Application.Models;
using Application.Responsitories;
using AutoMapper;
using Domain;
using MediatR;

namespace Application.Feature.Command
{
    public class CreatePropertyRequest : IRequest<bool>
    {
        public NewProperty PropertyRequest { get; set; }
        public CreatePropertyRequest(NewProperty newPropertyRequest)
        {
            PropertyRequest = newPropertyRequest;
        }
    }
    public class CreatePropertyRequestHandler : IRequestHandler<CreatePropertyRequest, bool>
    {
        private readonly IPropertyRepon _propertyRepon;
        private readonly IMapper _mapper;

        public CreatePropertyRequestHandler(IPropertyRepon propertyRepon, IMapper mapper)
        {
            _propertyRepon = propertyRepon;
            _mapper = mapper;
        }

        public async Task<bool> Handle(CreatePropertyRequest request, CancellationToken cancellationToken)
        {
            Property property = _mapper.Map<Property>(request.PropertyRequest);
            await _propertyRepon.AddNewAsync(property);
            return true;
        }
    }
}
