using CosmoDB.core.ModelValidators;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CosmoDB.core.Models.DTOs
{
    public class QuestionDTO
    {
        public string Id { get; set; } = "";
        public string Type { get; set; } = "";
        public string Question { get; set; } = "";
        public List<Choice>? Options { get; set; }
        public bool EnableOtherOption { get; set; }
        public int MaxChoice { get; set; }
        public bool DisqualifyIfFalse { get; set; }
        public bool Mandatory { get; set; }
        public bool Hide { get; set; }
        public bool IsInternal { get; set; }
    }

    public class Choice
    {
        public string? Option { get; set; }
        public int? ID { get; set; }
    }
}
