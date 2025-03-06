using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Domain.Model.BoxOptionDTOs.Response
{
    public class CreateBoxOptionResponse
    {
        public int boxId { get; set; }
        public string boxOptionId { get; set; }
        public string boxOptionName { get; set; }
        public int boxOptionStock { get; set; }
        public decimal originPrice { get; set; }
        public decimal displayPrice { get; set; }
        public bool isOnlineSerieBox { get; set; } = true;
    }
}
