using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Domain.Entities
{
    public class CurrentRolledItem
    {
        [Key, ForeignKey("BoxItem")]
        public int CurrentRolledItemId { get; set; }
        public int OnlienSeriesBoxId { get; set; }
        public virtual OnlineSerieBox OnlineSerieBox { get; set; }
        public virtual BoxItem BoxItem { get; set; }
    }
}
