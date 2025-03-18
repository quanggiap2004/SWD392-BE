namespace Common.Model.DashboardDTOs.Response
{
    public class DashboardResponseDTO
    {
        public decimal TotalRevenue { get; set; }
        public decimal TotalProfit { get; set; }
        public Dictionary<int, Dictionary<string, MonthlyDataDTO>> MonthlyData { get; set; } = new();
    }

    public class MonthlyDataDTO
    {
        public List<decimal> Revenue { get; set; } = new();
        public List<decimal> Profit { get; set; } = new();
        public List<int> WeeklyOrders { get; set; } = new();
    }
}
