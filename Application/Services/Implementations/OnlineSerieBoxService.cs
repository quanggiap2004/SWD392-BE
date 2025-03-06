using Application.Services.Interfaces;
using AutoMapper;
using Data;
using Data.Repository.Interfaces;
using Domain.Domain.Entities;
using Domain.Domain.Model.BoxOptionDTOs.Response;
using Domain.Domain.Model.OnlineSerieBoxDTOs.Request;
using Domain.Domain.Model.OnlineSerieBoxDTOs.Response;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using static Common.Exceptions.CustomExceptions;

namespace Application.Services.Implementations
{
    public class OnlineSerieBoxService : IOnlineSerieBoxService
    {
        private readonly IOnlineSerieBoxRepository _onlineSerieBoxRepository;
        private readonly IBoxOptionRepository _boxOptionRepository;
        private readonly IMapper _mapper;
        private readonly IBoxItemService _boxItemService;

        public OnlineSerieBoxService(IOnlineSerieBoxRepository onlineSerieBoxRepository, IBoxOptionRepository boxOptionRepository, IMapper mapper, IBoxItemService boxItemService)
        {
            _onlineSerieBoxRepository = onlineSerieBoxRepository;
            _boxOptionRepository = boxOptionRepository;
            _mapper = mapper;
            _boxItemService = boxItemService;
        }
        public async Task<CreateBoxOptionAndOnlineSerieBoxResponse> CreateBoxOptionAndOnlineSerieBoxAsync(CreateBoxOptionAndOnlineSerieBoxRequest request)
        {

            var boxOption = new BoxOption
            {
                BoxId = request.createBoxOptionRequest.boxId,
                BoxOptionName = request.createBoxOptionRequest.boxOptionName,
                OriginPrice = request.createBoxOptionRequest.originPrice,
                DisplayPrice = request.createBoxOptionRequest.displayPrice,
                BoxOptionStock = request.createBoxOptionRequest.boxOptionStock,
                IsOnlineSerieBox = true
            };

            var createdBoxOption = await _boxOptionRepository.AddBoxOptionAsync(boxOption);

            var onlineSerieBox = new OnlineSerieBox
            {
                OnlineSerieBoxId = createdBoxOption.BoxOptionId,
                IsSecretOpen = false,
                MaxTurn = CalculateMaxTurn(request.createBoxOptionRequest.boxId),
                Name = request.name,
                PriceAfterSecret = request.priceAfterSecret,
                PriceIncreasePercent = request.priceIncreasePercent,
            };

            var createdOnlineSerieBox = await _onlineSerieBoxRepository.AddOnlineSerieBoxAsync(onlineSerieBox);
            CreateBoxOptionResponse createBoxOptionResponse = _mapper.Map<CreateBoxOptionResponse>(createdBoxOption);
            var response = new CreateBoxOptionAndOnlineSerieBoxResponse
            {
                maxTurn = createdOnlineSerieBox.MaxTurn,
                name = createdOnlineSerieBox.Name,
                priceAfterSecret = createdOnlineSerieBox.PriceAfterSecret,
                priceIncreasePercent = createdOnlineSerieBox.PriceIncreasePercent,
                turn = createdOnlineSerieBox.Turn,
                createBoxOptionResponse = createBoxOptionResponse
            };

            return response;
        }

        public async Task<UpdateOnlineSerieBoxResponse> UpdateOnlineSerieBoxAsync(int onlineSerieBoxId, UpdateOnlineSerieBoxRequest request)
        {
            var onlineSerieBox = await _onlineSerieBoxRepository.GetOnlineSerieBoxByIdAsync(onlineSerieBoxId);
            if (onlineSerieBox == null)
            {
                throw new NotFoundException($"OnlineSerieBox with ID {onlineSerieBoxId} not found.");
            }

            if (onlineSerieBox.IsPublished)
            {
                throw new ValidationException("Cannot update a published OnlineSerieBox.");
            }

            onlineSerieBox.PriceAfterSecret = request.priceAfterSecret;
            onlineSerieBox.PriceIncreasePercent = request.priceIncreasePercent;

            var boxOption = await _boxOptionRepository.GetBoxOptionByIdAsync(onlineSerieBox.OnlineSerieBoxId);
            if (boxOption == null)
            {
                throw new NotFoundException($"BoxOption related to OnlineSerieBox ID {onlineSerieBoxId} not found.");
            }

            boxOption.DisplayPrice = request.displayPrice;
            boxOption.OriginPrice = request.originPrice;

            await _boxOptionRepository.UpdateBoxOptionAsync(boxOption);
            var updatedOnlineSerieBox = await _onlineSerieBoxRepository.UpdateOnlineSerieBoxAsync(onlineSerieBox);

            var response = new UpdateOnlineSerieBoxResponse
            {
                onlineSerieBoxId = updatedOnlineSerieBox.OnlineSerieBoxId,
                priceAfterSecret = updatedOnlineSerieBox.PriceAfterSecret,
                priceIncreasePercent = updatedOnlineSerieBox.PriceIncreasePercent,
                displayPrice = boxOption.DisplayPrice,
                originPrice = boxOption.OriginPrice,
                isPublished = updatedOnlineSerieBox.IsPublished
            };

            return response;
        }

        private int CalculateMaxTurn(int boxId)
        {
            var boxItemList = _boxItemService.GetBoxItemByBoxId(boxId);
            return boxItemList.Result.Count;
        }
    }
}
