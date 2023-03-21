using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CosmoDB.core.Models.DTOs
{
    public class FullProgramDetails
    {
        public string id { get; set; } = "";
        public ProgramDTO? Details { get; set; }
        public ApplicationFormDTO? Form { get; set; }
        public List<StageDTO>? Stages { get; set; }
    }
}
