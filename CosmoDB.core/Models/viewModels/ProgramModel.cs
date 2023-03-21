using CosmoDB.core.Configurations;
using CosmoDB.core.ModelValidators;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;

namespace CosmoDB.core.Models.viewModels
{
    public class ProgramModel
    {

        [Required]
        public string title { get; set; } = "";
        [MaxLength(250)]
        public string? summary { get; set; }
        [Required]
        public string description { get; set; } = "";
        // Should ideally be a int with the corresponding label and value in a DB to be fetched by the client side;
        [ValidArrayIfAllIncluded(new string[] { "UI", "UX", "Social Media", "Graphics Design", "Content Writing", "SEO" })]
        public string[] skills { get; set; } = Array.Empty<string>();
        public string? benefits { get; set; }
        public string? applicationCriteria { get; set; }
        [Required]
        // Should ideally be a int with the corresponding label and value in a DB to be fetched by the client side;
        [ValidIfIncluded(new string[] { "Internship", "Job", "Training", "Masterclass", "Webinar", "Course", "Live Seminar", "Volunteering", "Other" })]
        public string? type { get; set; }
        public DateTime? start { get; set; } = null;
        [Required]
        public DateTime applicationOpen { get; set; }
        [Required]
        public DateTime applicationClose { get; set; }
        public string? duration { get; set; }
        public string? location { get; set; }
        // Should ideally be a int with the corresponding label and value in a DB to be fetched by the client side;
        [ValidIfIncluded(new string[] { "High School", "College", "Graduate", "University", "Masters", "Ph.D", "Any" })]
        public string? minqualifications { get; set; }
        public Int32? maxNoApplicants { get; set; }
    }

    //public enum Description
    //{
    //    UI,
    //    UX,
    //    SOCIAL_MEDIA,
    //    GRAPHICS_DESIGN,
    //    CONTENT_WRITING,
    //    SEO
    //}
}
