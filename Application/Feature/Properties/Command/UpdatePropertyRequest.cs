using Application.Models;
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
    public class UpdatePropertyRequest : IRequest<bool>
    {
        public UpdateProperty UpdateProperty { get; set; }

        public UpdatePropertyRequest(UpdateProperty updateProperty)
        {
            UpdateProperty = updateProperty;
        }

        public class UpdatePropertyRequestHanler : IRequestHandler<UpdatePropertyRequest, bool>
        {
            private readonly IPropertyRepon _propertyRepon;
            public UpdatePropertyRequestHanler(IPropertyRepon propertyRepon)
            {
                _propertyRepon = propertyRepon;
            }

            public async Task<bool> Handle(UpdatePropertyRequest request, CancellationToken cancellationToken)
            {
                //check if exists in database
                Property propertyInDb = await _propertyRepon.GetByIdAsync(request.UpdateProperty.Id);
                //update
                if (propertyInDb != null)
                {
                    propertyInDb.Name = request.UpdateProperty.Name;
                    propertyInDb.Louge = request.UpdateProperty.Louge;
                    propertyInDb.Dining = request.UpdateProperty.Dining;
                    propertyInDb.Rates = request.UpdateProperty.Rates;
                    propertyInDb.Bathrooms = request.UpdateProperty.Bathrooms;
                    propertyInDb.Bedrooms = request.UpdateProperty.Bedrooms;
                    propertyInDb.Address = request.UpdateProperty.Address;
                    propertyInDb.Description = request.UpdateProperty.Description;
                    propertyInDb.ErfSize = request.UpdateProperty.ErfSize;
                    propertyInDb.FloorSize = request.UpdateProperty.FloorSize;
                    propertyInDb.Kitchens = request.UpdateProperty.Kitchens;
                    propertyInDb.Levies = request.UpdateProperty.Levies;
                    propertyInDb.PetsAllowed = request.UpdateProperty.PetsAllowed;
                    propertyInDb.Price = request.UpdateProperty.Price;
                    propertyInDb.Type = request.UpdateProperty.Type;
                    await _propertyRepon.UpdateAsync(propertyInDb);
                    return true;
                }
                return false;
            }

        }
    }
}
