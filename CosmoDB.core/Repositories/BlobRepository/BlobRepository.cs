using Azure;
using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using CosmoDB.core.Configurations;
using CosmoDB.core.Exceptions;
using CosmoDB.core.Logger;
using CosmoDB.core.Models.DTOs;
using CosmoDB.core.Models.viewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace CosmoDB.core.Repositories.BlobRepository
{
    public class BlobRepository: IBlobRepository
    {
        private readonly BlobContainerClient _blobContainer;
        private readonly BlobConnectionSettings _blobConnectionSettings;

        public BlobRepository(IOptions<BlobConnectionSettings> blobConectionSettings)
        {
            _blobConnectionSettings = blobConectionSettings.Value;  
            _blobContainer = new BlobContainerClient(_blobConnectionSettings.connectionString, _blobConnectionSettings.containername);
        }

        public async Task<CoverImageDTO> UploadApplicationImage(UploadImageModel imageDetail)
        {
            CoverImageDTO imageData;
            try
            {
                if(imageDetail.coverImage == null) throw new ErrorOccuredShortCircuitException($"Please upload valid image");
                BlobClient client = _blobContainer.GetBlobClient(imageDetail.ProgramId + System.IO.Path.GetExtension(imageDetail.coverImage.FileName));
                await using (Stream? data = imageDetail.coverImage.OpenReadStream())
                {
                    await client.UploadAsync(data);
                }
                imageData = new CoverImageDTO
                {
                    ImageUrl = client.Uri.AbsoluteUri,
                    FileName = client.Name
                };
            }
            catch (RequestFailedException ex) when (ex.ErrorCode == BlobErrorCode.BlobAlreadyExists) 
            {
                throw new ErrorOccuredShortCircuitException($"Program with id: {imageDetail.ProgramId} already has an image. Please delete current image before uploading.");
            }
            return imageData;
        }

        public async Task<bool> DeleteApplicationImage(string imageName)
        {
            bool isSuccess = false;

            BlobClient file = _blobContainer.GetBlobClient(imageName);

            try
            {
                await file.DeleteAsync();
                isSuccess = true;
            }
            catch (RequestFailedException ex) when (ex.ErrorCode == BlobErrorCode.BlobNotFound)
            {
                throw new ErrorOccuredShortCircuitException($"Image with name {imageName} not found.");
            }
            return isSuccess;
        }
    }
}
