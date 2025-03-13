using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Model.FeedbackDTOs.Request
{
    public class UpdateFeedbackRequestDto
    {
        public string feedbackContent { get; set; }
        public string imageUrl { get; set; }
        public float rating { get; set; }
        public int boxOptionId { get; set; }
    }
}
