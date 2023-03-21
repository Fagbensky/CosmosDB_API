using CosmoDB.core.ModelValidators;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CosmoDB.core.Models.DTOs
{
    public class ProgramDTO
    {
        public string? Title { get; set; }
        public string? Summary { get; set; }
        public string? Description { get; set; }
        public string[]? Skills { get; set; } 
        public string? Benefits { get; set; }
        public string? ApplicationCriteria { get; set; }
        public string? Type { get; set; }
        public DateTime Start { get; set; }
        public DateTime ApplicationOpen { get; set; }
        [Required]
        public DateTime ApplicationClose { get; set; }
        public string? Duration { get; set; }
        public string? Location { get; set; }
        public string? MinQualifications { get; set; }
        public Int32? MaxNoApplicants { get; set; }
    }
}
