using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Domain.Entities
{
    public class Transaction
    {
        [Key]
        public int TransactionId { get; set; }
        public int WalletId { get; set; }
        public float Amount { get; set; }
        public float BalanceAfter { get; set; }
        public string TransactionType { get; set; }
        public DateTime TransactionDate { get; set; }

        [ForeignKey("WalletId")]
        public virtual UserWallet UserWallet { get; set; }
    }
}
