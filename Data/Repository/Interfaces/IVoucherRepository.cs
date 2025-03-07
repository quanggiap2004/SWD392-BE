namespace Data.Repository.Interfaces
{
    public interface IVoucherRepository
    {
        Task ReduceVoucherQuantity(int? voucherId);
    }
}
