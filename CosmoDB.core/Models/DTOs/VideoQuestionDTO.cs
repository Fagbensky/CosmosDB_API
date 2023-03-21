using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CosmoDB.core.Models.DTOs
{
    public class VideoQuestionDTO
    {
        public string Id { get; set; } = "";
        public string Question { get; set; } = "";
        public string? AdditionalInfo { get; set; }
        public int? MaxDuration { get; set; }
        public string? MaxDurationTime { get; set; }
        public int? DeadlineDays { get; set; }
    }
}
