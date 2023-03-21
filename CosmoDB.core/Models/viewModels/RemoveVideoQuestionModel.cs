using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CosmoDB.core.Models.viewModels
{
    public class RemoveVideoQuestionModel: RemoveStageModel
    {
        [Required]
        public string VideoQuestionId { get; set; } = "";
    }
}
