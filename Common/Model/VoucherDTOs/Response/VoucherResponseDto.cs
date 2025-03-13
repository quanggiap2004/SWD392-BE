using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Model.VoucherDTOs.Response
{
    public class VoucherResponseDto
    {
        public int voucherId { get; set; }
        public string voucherName { get; set; }
        public string voucherCode { get; set; }
        public int voucherDiscount { get; set; }
        public int numOfVoucher { get; set; }
        public DateTime voucherStartDate { get; set; }
        public DateTime voucherEndDate { get; set; }
        public float maxDiscount { get; set; }
        public bool isDeleted { get; set; }
    }
}
