using CosmoDB.core.ModelValidators;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CosmoDB.core.Models.viewModels
{
    public class UploadImageModel: ProgramIdModel
    {
        [Required]
        [ValidateImageFile]
        public IFormFile? coverImage { get; set; }

    }
}
