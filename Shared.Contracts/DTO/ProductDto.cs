using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Contracts.DTO
{
    public class ProductDto
    {
        public int Guid { get; set; }
        public string? Name { get; set; }
        public decimal Price { get; set; }
    }
}
