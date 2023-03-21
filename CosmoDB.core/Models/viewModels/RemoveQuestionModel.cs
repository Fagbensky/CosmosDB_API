using CosmoDB.core.Models.DTOs;
using CosmoDB.core.ModelValidators;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Drawing.Printing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CosmoDB.core.Models.viewModels
{
    public class RemoveQuestionModel: ProgramIdModel
    {
        [Required]
        public string QuestionId { get; set; } = "";
        [Required]
        [ValidIfIncluded(new string[] { QuestionSections.PersonalInformation, QuestionSections.PersonalInformation, QuestionSections.AdditionalQuestion })]
        public string Section { get; set; } = "";
    }
}
