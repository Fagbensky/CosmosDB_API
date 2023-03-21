using CosmoDB.core.Models.DTOs;
using CosmoDB.core.Models.viewModels;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CosmoDB.core.Repositories.BlobRepository
{
    public interface IBlobRepository
    {
        Task<CoverImageDTO> UploadApplicationImage(UploadImageModel imageDetail);
        Task<bool> DeleteApplicationImage(string imageName);
    }
}
