using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public class Property
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Type { get; set; }
        public decimal Price { get; set; } 
        public decimal Rates { get; set; }
        public DateTime Date { get; set; }
        public List<Image> Images { get; set; }
    }
}
