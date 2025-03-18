using Application.Services.Interfaces;
using Common.Model.BoxDTOs.ResponseDTOs;
using Common.Model.DashboardDTOs.Response;
using Data.Repository.Interfaces;
using System.Globalization;

namespace Application.Services.Implementations
{
    public class DashboardService : IDashboardService
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IBoxRepository _boxRepository;


        public DashboardService(IOrderRepository orderRepository, IBoxRepository boxRepository)
        {
            _orderRepository = orderRepository;
            _boxRepository = boxRepository;
        }

        public async Task<DashboardResponseDTO> GetDashboardDataAsync()
        {
            var orders = await _orderRepository.GetAllOrderForDasboard();
            var response = new DashboardResponseDTO();

            foreach (var order in orders)
            {
                var year = order.OrderCreatedAt.Year;
                var month = order.OrderCreatedAt.ToString("MMMM", CultureInfo.InvariantCulture);
                var week = GetWeekOfMonth(order.OrderCreatedAt);

                if (!response.MonthlyData.ContainsKey(year))
                {
                    response.MonthlyData[year] = new Dictionary<string, MonthlyDataDTO>();
                }

                if (!response.MonthlyData[year].ContainsKey(month))
                {
                    response.MonthlyData[year][month] = new MonthlyDataDTO
                    {
                        Revenue = new List<decimal> { 0, 0, 0, 0 },
                        Profit = new List<decimal> { 0, 0, 0, 0 },
                        WeeklyOrders = new List<int> { 0, 0, 0, 0 }
                    };
                }

                response.TotalRevenue += order.TotalPrice;
                response.TotalProfit += order.Revenue;
                if (week >= 1 && week <= 4)
                {
                    response.MonthlyData[year][month].Revenue[week - 1] += order.TotalPrice;
                    response.MonthlyData[year][month].Profit[week - 1] += order.Revenue;
                    response.MonthlyData[year][month].WeeklyOrders[week - 1] += 1;
                }

            }

            return response;
        }

        public async Task<IEnumerable<BestSellerBoxesDto>> GetBestSellerBoxForDashboard()
        {
            var boxes = await _boxRepository.GetBestSellerBox(10);
            var response = boxes.Select(box => new BestSellerBoxesDto
            {
                boxId = box.boxId,
                boxName = box.boxName,
                soldQuantity = box.soldQuantity,
            });

            return response;
        }

        private int GetWeekOfMonth(DateTime date)
        {
            var firstDay = new DateTime(date.Year, date.Month, 1);
            return (date.Day + (int)firstDay.DayOfWeek) / 7 + 1;
        }
    }
}
