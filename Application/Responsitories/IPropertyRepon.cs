using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Responsitories
{
    public interface IPropertyRepon
    {
        Task AddNewAsync(Property property);
        Task DeleteAsync(Property property);
        Task<List<Property>> GetAllAsync();
        Task UpdateAsync(Property property);
        Task<Property> GetByIdAsync(int id);
    }
}
