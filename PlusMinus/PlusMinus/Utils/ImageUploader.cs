using System;
using System.IO;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;

namespace PlusMinus.Utils
{
    public static class ImageUploader
    {
        public static string CreatePath(IFormFile file, IWebHostEnvironment webHostEnvironment)
        {
            
            string filePath = null;
            string path = null;

            if (file != null)
            {
                path = "/images/";
                string uniqueFileName = Guid.NewGuid().ToString() + "_" + file.FileName;
                path += uniqueFileName;
                filePath = Path.Combine(Path.Combine(webHostEnvironment.WebRootPath, "images"), uniqueFileName);
                using var fileStream = new FileStream(filePath, FileMode.Create);
                file.CopyTo(fileStream);
            }

            return path;
        }
    }
}