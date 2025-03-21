using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Model.FeedbackDTOs.Response
{
    public class FullFeedbackResponseDto
    {
        public int feedbackId { get; set; }
        public string feedbackContent { get; set; }
        public DateTime? updatedAt { get; set; }
        public DateTime createdAt { get; set; }
        public string imageUrl { get; set; }

        public int orderItemId { get; set; }
        public float rating { get; set; }
        public int userId { get; set; }
        public string userName { get; set; }
        public string email { get; set; }
        public string boxOptionName { get; set; }
        public int boxOptionId { get; set; }
        public string? avatarUrl {  get; set; }
    }
}
