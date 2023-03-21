using CosmoDB.core.ModelValidators;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CosmoDB.core.Models.viewModels
{
    public class VideoQuestionModel
    {
        [Required]
        public string Question { get; set; } = "";
        public string? AdditionalInfo { get; set; }
        [Required]
        public int? MaxDuration { get; set; }
        [Required]
        [ValidIfIncluded(new string[] { "SEC", "VIDEO" })]
        public string? MaxDurationTime { get; set; }
        public int? DeadlineDays { get; set; }
    }
}
