using System;
using Microsoft.AspNetCore.Http;

namespace Domain.Dtos
{
    public class PhotoToAddRequest
    {      
        public IFormFile File { get; set; }
        public string Description { get; set; }
    }
}