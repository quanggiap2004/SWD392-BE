using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Model.OrderItem.Request
{
    public class UpdateOrderItemRefundDetailsRequestDto
    {
        public string? note { get; set; }
        public int numOfRefund { get; set; }
    }
}
