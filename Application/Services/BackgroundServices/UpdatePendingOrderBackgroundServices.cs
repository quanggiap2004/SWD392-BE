using Application.Services.Implementations;
using Application.Services.Interfaces;
using Common.Constants;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.BackgroundServices
{
    public class UpdatePendingOrderBackgroundServices : BackgroundService
    {
        private readonly ILogger<UpdatePendingOrderBackgroundServices> _logger;
        private readonly IServiceProvider _serviceProvider;
        public UpdatePendingOrderBackgroundServices(
            ILogger<UpdatePendingOrderBackgroundServices> logger,
        IServiceProvider serviceProvider)
        {
            _logger = logger;
            _serviceProvider = serviceProvider;
        }
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _logger.LogInformation("Order Processing Background Service is starting.");

            while (!stoppingToken.IsCancellationRequested && ProjectConstant.BackgroundServiceActivatedStatus)
            {
                try
                {
                    // Create a scope to resolve scoped services
                    using (var scope = _serviceProvider.CreateScope())
                    {
                        var orderService = scope.ServiceProvider.GetRequiredService<IOrderService>();

                        //Update orders
                        await ProcessOrdersAsync(orderService, stoppingToken);
                    }

                    await Task.Delay(TimeSpan.FromMinutes(ProjectConstant.BackgroundUpdatePendingInterval), stoppingToken);
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "An error occurred while processing orders.");
                }
            }

            _logger.LogInformation("Order Processing Background Service is stopping.");
        }
        private async Task ProcessOrdersAsync(IOrderService orderService, CancellationToken stoppingToken)
        {
            _logger.LogInformation("Processing orders...");

            await orderService.UpdateOrderPendingStatus();

            _logger.LogInformation("Orders processed successfully.");
        }
    }
}
