using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Domain.Entities
{
    public class UserRolledItem
    {
        [Key]
        public int UserRolledItemId { get; set; }
        public int OnlineSeriesBoxId { get; set; }
        public int UserId { get; set; }
        public int BoxItemId { get; set; }
        public bool IsCheckOut { get; set; } = false;
        public virtual OnlineSerieBox OnlineSerieBox { get; set; }
        public virtual User User { get; set; }
        public virtual BoxItem BoxItem { get; set; }
    }
}
