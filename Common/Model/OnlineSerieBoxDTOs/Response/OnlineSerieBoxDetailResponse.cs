using Common.Model.BoxItemDTOs.Response;
using Common.Model.BrandDTOs.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Model.OnlineSerieBoxDTOs.Response
{
    public class OnlineSerieBoxDetailResponse
    {
        public int onlineSerieBoxId { get; set; }
        public decimal priceAfterSecret { get; set; }
        public int priceIncreasePercent { get; set; }
        public decimal basePrice { get; set; }
        public bool isPublished { get; set; }
        public int maxTurn { get; set; }
        public int turn { get; set; }
        public string imageUrl { get; set; }
        public bool isSecretOpen { get; set; }
        public required BrandDtoResponse brandDtoResponse { get; set; }
        public BoxOptionResponse boxOption { get; set; }
        public IEnumerable<BoxItemResponseDto> boxItemResponseDtos { get; set; }
    }
}
