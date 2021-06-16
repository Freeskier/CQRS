using System;
using Microsoft.AspNetCore.Http;

namespace Domain.Dtos
{
    public class MainPhotoToUpdateRequest
    {     
        public IFormFile File { get; set; }
    }
}