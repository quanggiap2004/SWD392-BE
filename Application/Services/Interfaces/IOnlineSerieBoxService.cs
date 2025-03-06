using Domain.Domain.Model.OnlineSerieBoxDTOs.Request;
using Domain.Domain.Model.OnlineSerieBoxDTOs.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.Interfaces
{
    public interface IOnlineSerieBoxService
    {
        Task<CreateBoxOptionAndOnlineSerieBoxResponse> CreateBoxOptionAndOnlineSerieBoxAsync(CreateBoxOptionAndOnlineSerieBoxRequest request);
        Task<UpdateOnlineSerieBoxResponse> UpdateOnlineSerieBoxAsync(int onlineSerieBoxId, UpdateOnlineSerieBoxRequest request);

    }
}
