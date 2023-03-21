using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CosmoDB.core.Models.DTOs
{
    public class ApplicationFormDTO
    {
        public CoverImageDTO? Image { get; set; }
        public List<QuestionDTO>? PersonalInformationQuestions { get; set; }
        public List<QuestionDTO>? ProfileQuestions { get; set; }
        public List<QuestionDTO>? AdditionalQuestions { get; set; }

    }
}
