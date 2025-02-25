using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Domain.Entities
{
    public class UserWallet
    {
        [Key]
        public int WalletId { get; set; }
        public int UserId { get; set; }
        public float Balance { get; set; }

        [ForeignKey("UserId")]
        public User User { get; set; }
        public ICollection<Transaction> Transactions { get; set; }
    }
}
