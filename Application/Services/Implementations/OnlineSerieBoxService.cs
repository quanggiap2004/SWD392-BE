using Application.Services.Interfaces;
using AutoMapper;
using Common.Exceptions;
using Common.Model.BoxItemDTOs.Response;
using Common.Model.BoxOptionDTOs.Response;
using Common.Model.CurrentRolledITemDTOs.Request;
using Common.Model.OnlineSerieBoxDTOs.Request;
using Common.Model.OnlineSerieBoxDTOs.Response;
using Common.Model.UserRolledItemDTOs.Response;
using Data.Repository.Interfaces;
using Domain.Domain.Entities;
using System.ComponentModel.DataAnnotations;
using static Common.Exceptions.CustomExceptions;

namespace Application.Services.Implementations
{
    public class OnlineSerieBoxService : IOnlineSerieBoxService
    {
        private readonly IOnlineSerieBoxRepository _onlineSerieBoxRepository;
        private readonly IBoxOptionRepository _boxOptionRepository;
        private readonly IMapper _mapper;
        private readonly IBoxItemService _boxItemService;
        private readonly IBoxService _boxService;
        private readonly ICurrentRolledItemService _currentRolledItemService;
        private readonly IUserRolledItemService _userRolledItemService;

        public OnlineSerieBoxService(IOnlineSerieBoxRepository onlineSerieBoxRepository, IBoxOptionRepository boxOptionRepository, IMapper mapper, IBoxItemService boxItemService, IBoxService boxService, ICurrentRolledItemService currentRolledItemService, IUserRolledItemService userRolledItemService)
        {
            _onlineSerieBoxRepository = onlineSerieBoxRepository;
            _boxOptionRepository = boxOptionRepository;
            _mapper = mapper;
            _boxItemService = boxItemService;
            _boxService = boxService;
            _currentRolledItemService = currentRolledItemService;
            _userRolledItemService = userRolledItemService;
        }
        public async Task<CreateBoxOptionAndOnlineSerieBoxResponse> CreateBoxOptionAndOnlineSerieBoxAsync(CreateBoxOptionAndOnlineSerieBoxRequest request)
        {
            var isValidToCreate = await _boxService.CheckCreateOnlineSerieBoxCriteria(request.createBoxOptionRequest.boxId);

   
            if (!isValidToCreate)
            {
                throw new BadRequestException("Box do not satisfy criteria to be created OnlineSerieBox for this Box.");
            }
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
                PriceAfterSecret = request.priceAfterSecret,
                PriceIncreasePercent = request.priceIncreasePercent,
                BasePrice = request.createBoxOptionRequest.displayPrice,
                ImageUrl = request.imageUrl,
            };

            var createdOnlineSerieBox = await _onlineSerieBoxRepository.AddOnlineSerieBoxAsync(onlineSerieBox);
            CreateBoxOptionResponse createBoxOptionResponse = _mapper.Map<CreateBoxOptionResponse>(createdBoxOption);
            var response = new CreateBoxOptionAndOnlineSerieBoxResponse
            {
                maxTurn = createdOnlineSerieBox.MaxTurn,
                priceAfterSecret = createdOnlineSerieBox.PriceAfterSecret,
                priceIncreasePercent = createdOnlineSerieBox.PriceIncreasePercent,
                basePrice = createdOnlineSerieBox.BasePrice,
                turn = createdOnlineSerieBox.Turn,
                createBoxOptionResponse = createBoxOptionResponse,
                imageUrl = createdOnlineSerieBox.ImageUrl,
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
            onlineSerieBox.ImageUrl = request.imageUrl;
            onlineSerieBox.BasePrice = request.displayPrice;

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
                isPublished = updatedOnlineSerieBox.IsPublished,
                imageUrl = updatedOnlineSerieBox.ImageUrl,
                basePrice = updatedOnlineSerieBox.BasePrice
                
            };

            return response;
        }

        private int CalculateMaxTurn(int boxId)
        {
            var boxItemList = _boxItemService.GetBoxItemByBoxId(boxId);
            return boxItemList.Result.Count;
        }

        public async Task<IEnumerable<GetAllOnlineSerieBoxResponse>> GetAllOnlineSerieBoxesAsync()
        {
            return await _onlineSerieBoxRepository.GetAllOnlineSerieBoxesAsync();
        }

        public async Task<BoxItemResponseDto> OpenOnlineSerieBoxAsync(OpenOnlineSerieBoxRequest request)
        {
            var box = await _boxService.getBoxByBoxOptionId(request.onlineSerieBoxId);
            var boxOption = await _boxOptionRepository.GetBoxOptionByIdWithOnlineSerieBox(request.onlineSerieBoxId);
            if (box == null || boxOption == null || boxOption.BoxOptionStock == 0)
            {
                throw new NotFoundException("box not found or out of stock");
            }
            var onlineSerieBox = boxOption.OnlineSerieBox;
            if(onlineSerieBox.IsPublished == false)
            {
                throw new InvalidOperationException("OnlineSerieBox is not published.");
            }
            var boxItemList = box.boxItems;
            var rolledIds = (await _currentRolledItemService
                                   .GetAllCurrentRolledItemByOnlineSerieBoxId(request.onlineSerieBoxId))
                                   .Select(cri => cri.boxItemId)
                                   .ToHashSet();
            var notRolledItems = boxItemList.Where(bi => !rolledIds.Contains(bi.boxItemId)).ToList();

            if (!notRolledItems.Any())
            {
                throw new InvalidOperationException("All items have been rolled.");
            }
            var weightedItems = notRolledItems.Select(item => new
            {
                Item = item,
                Weight = item.isSecret ? 20 : 25
            }).ToList();

            int totalWeight = weightedItems.Sum(w => w.Weight);

            // Generate a random number between 1 and totalWeight (inclusive).
            var rng = new Random();
            int randomNumber = rng.Next(1, totalWeight + 1);

            // Use cumulative weights (roulette wheel selection) to pick an item.
            int cumulative = 0;
            BoxItemResponseDto selectedItem = null;
            foreach (var wi in weightedItems)
            {
                cumulative += wi.Weight;
                if (randomNumber <= cumulative)
                {
                    selectedItem = wi.Item;
                    break;
                }
            }

            if (selectedItem == null)
            {
                throw new Exception("Failed to select a box item.");
            }

            // Record the selected item in the CurrentRolledItem table.
            var currentRolledItemDto = new CurrentRolledItemDto
            {
                boxItemId = selectedItem.boxItemId,
                onlineSerieBoxId = request.onlineSerieBoxId,
                createdAt = DateTime.UtcNow
            };

            // Add to UserRolledItem
            var userRolledItem = new UserRolledItem
            {
                OnlineSerieBoxId = request.onlineSerieBoxId,
                UserId = request.userId,
                BoxItemId = selectedItem.boxItemId,
                IsCheckOut = false
            };


            // Increase turn
            onlineSerieBox.Turn += 1;

            // Update BoxOption DisplayPrice
            decimal basePrice = onlineSerieBox.BasePrice;
            int turn = onlineSerieBox.Turn;
            int priceIncreasePercent = onlineSerieBox.PriceIncreasePercent;
            decimal priceAfterSecret = onlineSerieBox.PriceAfterSecret;

            if (selectedItem.isSecret)
            {
                // Reset DisplayPrice if secret item is rolled
                boxOption.DisplayPrice = onlineSerieBox.PriceAfterSecret;
                onlineSerieBox.IsSecretOpen = true;
            }
            else if(!onlineSerieBox.IsSecretOpen)
            {
                decimal newPrice = Math.Round(boxOption.DisplayPrice + (boxOption.DisplayPrice * priceIncreasePercent / 100m));
            }

            await _currentRolledItemService.AddCurrentRolledItem(currentRolledItemDto);
            // Check if all items have been rolled
            if (notRolledItems.Count == 1)
            {
                boxOption.BoxOptionStock -= 1;
                boxOption.DisplayPrice = basePrice;
                onlineSerieBox.Turn = 0;
                onlineSerieBox.IsSecretOpen = false;
                await _currentRolledItemService.ResetCurrentRoll(onlineSerieBox.OnlineSerieBoxId);
            }

            var userRolledItemResponse = await _userRolledItemService.AddUserRolledItemAsync(userRolledItem);
            selectedItem.userRolledItem = new UserRolledItemForManageOrder
            {
                boxItemImageUrl = selectedItem.imageUrl,
                userRolledItemId = userRolledItemResponse.UserRolledItemId,
            };
            await _onlineSerieBoxRepository.UpdateOnlineSerieBoxAsync(onlineSerieBox);
            await _boxOptionRepository.UpdateBoxOptionAsync(boxOption);

            return selectedItem;
        }

        public async Task<OnlineSerieBoxDetailResponse> GetOnlineSerieBoxByIdAsync(int onlineSerieBoxId)
        {
            var onlineSerieBox = await _onlineSerieBoxRepository.GetOnlineSerieBoxDetail(onlineSerieBoxId);
            if (onlineSerieBox == null)
            {
                throw new NotFoundException($"OnlineSerieBox with ID {onlineSerieBoxId} not found.");
            }

            return onlineSerieBox;
        }

        public async Task<bool> UpdatePublishStatusAsync(bool status, int id)
        {
            var boxDto = await _boxService.getBoxByBoxOptionId(id);
            var IsStatisfyPublishCriteria = await _boxService.CheckCreateOnlineSerieBoxCriteria(boxDto.boxId);
            return await _onlineSerieBoxRepository.UpdatePublishStatusAsync(status, id);
        }
    }
}
