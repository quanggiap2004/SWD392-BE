using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Model.BoxItemDTOs.Response
{
    public class BoxItemPaymentResponseDto
    {
        public BoxItemResponseDto boxItemResponseDto { get; set; }
        public int boxOptionId { get; set; }
    }
}
