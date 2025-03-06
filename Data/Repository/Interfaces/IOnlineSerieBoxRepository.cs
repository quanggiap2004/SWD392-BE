using Domain.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repository.Interfaces
{
    public interface IOnlineSerieBoxRepository
    {
        Task<OnlineSerieBox> AddOnlineSerieBoxAsync(OnlineSerieBox onlineSerieBox);
        Task<OnlineSerieBox?> GetOnlineSerieBoxByIdAsync(int id);
        Task<OnlineSerieBox> UpdateOnlineSerieBoxAsync(OnlineSerieBox onlineSerieBox);

    }
}
