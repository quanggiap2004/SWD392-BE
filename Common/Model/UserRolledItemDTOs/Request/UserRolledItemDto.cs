using Common.Model.BoxItemDTOs.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Model.UserRolledItemDTOs.Request
{
    public class UserRolledItemDto
    {
        public int id { get; set; }
        public bool isCheckout { get; set; }
        public BoxItemResponseDto boxItem { get; set; }
        public int userId { get; set; }
        public string userName { get; set; }
        public int quantity { get; set; } = 1;
        public decimal price { get; set; } = 0;
        public int boxOptionId { get; set; }
        public string boxOptionName { get; set; }
        public decimal originPrice { get; set; } = 0;
        public bool isOnlineSerieBox { get; set; } = false;
        public int orderItemOpenRequestNumber { get; set; } = 0;
        public string brandName {  get; set; }
        public int brandId {  get; set; }
    }
}
