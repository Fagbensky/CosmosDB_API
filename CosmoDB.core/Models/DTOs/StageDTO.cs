using CosmoDB.core.Models.viewModels;
using CosmoDB.core.ModelValidators;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CosmoDB.core.Models.DTOs
{
    public class StageDTO
    {
        public string Id { get; set; } = "";
        public string Name { get; set; } = "";
        public string Type { get; set; } = "";
        public bool ShowCandidate { get; set; } = false;
        public List<VideoQuestionDTO>? VideoQuestions { get; set; }
    }
}
