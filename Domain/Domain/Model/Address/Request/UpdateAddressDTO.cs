using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Domain.Model.Address.Request
{
    public class UpdateAddressDTO
    {
        public int addressId { get; set; }
        public string province { get; set; }
        public string district { get; set; }
        public string ward { get; set; }
        public int ProvinceId { get; set; }
        public int DistrictId { get; set; }
        public int WardCode { get; set; }
        public string addressDetail { get; set; }
        public string phoneNumber { get; set; }
        public string name { get; set; }
        public string? note { get; set; }
    }
}
