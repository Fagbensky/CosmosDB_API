using AutoMapper.Execution;
using CosmoDB.core.Models.DTOs;
using CosmoDB.core.ModelValidators;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CosmoDB.core.Models.viewModels
{
    public class UploadQuestionModel: ProgramIdModel
    {
        [Required]
        // Should ideally be a int with the corresponding label and value in a DB to be fetched by the client side.
        [ValidIfIncluded(new string[] { "PARAGRAPH", "SHORT_ANSWER" , "YES_NO", "DROPDOWN", "MULTIPLE_CHOICE", "DATE", "NUMBER", "FILE_UPLOAD", "VIDEO_QUESTION" })]
        public string? type { get; set; }
        [Required]
        public string? question { get; set; }   
        public List<choice>? options { get; set; }
        public bool enableOtherOption { get; set; } = false;
        public int maxChoice { get; set; }
        public bool disqualifyIfFalse { get; set; } = false;
        [Required]
        // Should ideally be a int with the corresponding label and value in a DB to be fetched by the client side.
        [ValidIfIncluded(new string[] { QuestionSections.PersonalInformation, QuestionSections.Profile, QuestionSections.AdditionalQuestion })]
        public string section { get; set; } = "";
        public bool mandatory { get; set; } = false;
        public bool hide { get; set; } = false;
        public bool isInternal { get; set; } = false;
    }

    public class choice
    {
        public string? option { get; set; }
        public int id { get; set; }
    }
}
