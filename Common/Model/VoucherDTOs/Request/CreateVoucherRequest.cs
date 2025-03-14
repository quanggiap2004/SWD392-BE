﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Model.VoucherDTOs.Request
{
    public class CreateVoucherRequest
    {
        public string voucherName { get; set; }
        public int voucherDiscount { get; set; }
        public DateTime voucherStartDate { get; set; }
        public DateTime voucherEndDate { get; set; }
        public int numOfVoucher { get; set; }
        public float maxDiscount { get; set; }
    }
}
