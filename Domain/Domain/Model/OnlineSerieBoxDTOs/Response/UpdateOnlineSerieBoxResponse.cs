using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Domain.Model.OnlineSerieBoxDTOs.Response
{
    public class UpdateOnlineSerieBoxResponse
    {
        public int onlineSerieBoxId { get; set; }
        public decimal priceAfterSecret { get; set; }
        public int priceIncreasePercent { get; set; }
        public decimal displayPrice { get; set; }
        public decimal originPrice { get; set; }
        public bool isPublished { get; set; }
    }
}
