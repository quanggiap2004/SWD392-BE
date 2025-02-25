﻿using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace BlindBoxSystem.Domain.Entities
{
    [Index(nameof(VoucherCode), IsUnique = true)]
    public class Voucher
    {
        [Key]
        public int VoucherId { get; set; }
        public string VoucherName { get; set; }
        public string VoucherCode { get; set; }
        public int VoucherDiscount { get; set; }
        public DateTime VoucherStartDate { get; set; }
        public DateTime VoucherEndDate { get; set; }
        public float VoucherPriceRequirement { get; set; }
        public float MaxDiscount { get; set; }
        public int NumOfVoucher { get; set; }
        public bool IsDeleted { get; set; } = false;
        public virtual ICollection<Order> Orders { get; set; }
    }
}