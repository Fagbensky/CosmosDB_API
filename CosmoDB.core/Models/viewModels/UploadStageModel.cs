using CosmoDB.core.ModelValidators;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CosmoDB.core.Models.viewModels
{
    public class UploadStageModel: ProgramIdModel
    {
        [Required]
        public string Name { get; set; } = "";
        [Required]
        [ValidIfIncluded(new string[] { "SHORTLIST", "VIDEO_INTERVIEW", "PLACEMENT" })]
        public string Type { get; set; } = "";
        public bool ShowCandidate { get; set; } = false;
        public List<VideoQuestionModel>? VideoQuestions { get; set; }
    }
}
