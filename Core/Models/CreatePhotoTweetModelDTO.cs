using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace Core.Models
{
    public class CreatePhotoTweetModelDTO
    {
        public string UserId { get; set; }
        public IFormFile Photo { get; set; }
    }
}
