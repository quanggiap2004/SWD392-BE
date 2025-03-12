using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Model.BrandDTOs.Response
{
    public class BrandDtoResponse
    {
        public int brandId { get; set; }
        public string brandName { get; set; }
        public string? imageUrl { get; set; }
    }
}
