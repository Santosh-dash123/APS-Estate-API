using APSEstate.CORE.Enum;
using Microsoft.AspNetCore.Http;
using System;
using System.IO;
using System.Linq;
using System.Text;

namespace APSEstate.CORE.Helper
{
    public static class CommonHelper
    {
        public static string EncodeBase64(string value)
        {
            var bytes = Encoding.UTF8.GetBytes(value);
            return Convert.ToBase64String(bytes);
        }

        public static string DecodeBase64(string value)
        {
            var bytes = Convert.FromBase64String(value);
            return Encoding.UTF8.GetString(bytes);
        }

        public static async Task<string> SaveFileAsync(IFormFile file,string folderName,AllowedFileType fileType,string webRootPath)
        {
            if (file == null || file.Length == 0)
                throw new Exception("File is required.");

            var extension = Path.GetExtension(file.FileName).ToLowerInvariant();

            var allowedExtensions = GetAllowedExtensions(fileType);

            if (Array.IndexOf(allowedExtensions, extension) == -1)
                throw new Exception("Invalid file type.");

            if (!Directory.Exists(webRootPath))
                Directory.CreateDirectory(webRootPath);

            var uploadFolder = Path.Combine(
                webRootPath,
                "Upload",
                folderName);

            if (!Directory.Exists(uploadFolder))
                Directory.CreateDirectory(uploadFolder);

            var fileName = $"{Guid.NewGuid():N}{extension}";

            var filePath = Path.Combine(
                uploadFolder,
                fileName);

            await using var stream = new FileStream(
                filePath,
                FileMode.Create);

            await file.CopyToAsync(stream);

            return fileName;
        }

        private static string[] GetAllowedExtensions(
            AllowedFileType fileType)
        {
            return fileType switch
            {
                AllowedFileType.PDF =>
                    new[] { ".pdf" },

                AllowedFileType.Image =>
                    new[] { ".jpg", ".jpeg", ".png", ".webp" },

                _ => Array.Empty<string>()
            };
        }
    }
}