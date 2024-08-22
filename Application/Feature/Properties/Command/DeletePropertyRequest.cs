using Application.Responsitories;
using Domain;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Feature.Properties.Command
{
    // step 2
    public class DeletePropertyRequest : IRequest<bool>
    {

        public int PropertyId { get; set; }

        public DeletePropertyRequest(int propertyId)
        {
            PropertyId = propertyId;
        }


    }
    public class DeletePropertyRequestHandler : IRequestHandler<DeletePropertyRequest, bool>
    {
        private readonly IPropertyRepon _propertyRepon;

        public DeletePropertyRequestHandler(IPropertyRepon propertyRepon)
        {
            _propertyRepon = propertyRepon;
        }

        public async Task<bool> Handle(DeletePropertyRequest request, CancellationToken cancellationToken)
        {
            Property propertyInDb = await _propertyRepon.GetByIdAsync(request.PropertyId);
            if (propertyInDb != null)
            {
                await _propertyRepon.DeleteAsync(propertyInDb);
                return true;
            }
            return false;
        }
    }
}
