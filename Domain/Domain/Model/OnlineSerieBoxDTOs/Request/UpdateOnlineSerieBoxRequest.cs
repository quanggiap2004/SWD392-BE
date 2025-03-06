using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Domain.Model.OnlineSerieBoxDTOs.Request
{
    public class UpdateOnlineSerieBoxRequest
    {
        public decimal priceAfterSecret { get; set; }
        public int priceIncreasePercent { get; set; }
        public decimal displayPrice { get; set; }
        public decimal originPrice { get; set; }
    }
}
