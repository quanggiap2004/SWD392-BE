using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Model.FeedbackDTOs.Request
{
    public class FeedbackRequestDto
    {
        public int userId { get; set; }
        public string feedbackContent { get; set; }
        public DateTime? updatedAt { get; set; }
        public DateTime createdAt { get; set; }
        public string imageUrl { get; set; }

        public int orderItemId { get; set; }
        public int boxOptionId { get; set; }
        public int rating { get; set; }
    }
}
