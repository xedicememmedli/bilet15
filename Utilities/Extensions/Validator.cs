using Bilet_15.Utilities.Enum;
using Microsoft.AspNetCore.Http;

namespace Bilet_15.Utilities.Extensions
{
    public static class Validator
    {
        public static bool ValidateType(this IFormFile file, string fileType)
        {
            if (file.ContentType.Contains(fileType))
            {
                return true;
            }
            return false;
        }
        public static bool ValidateSize(this IFormFile formFile, FileSize fileSize, int size)
        {
            switch (fileSize)
            {
                case FileSize.Kb:
                    return formFile.Length < size * 1024;
                case FileSize.Mb:
                    return formFile.Length < size * 1024 * 1024;
                case FileSize.Gb:
                    return formFile.Length < size * 1024 * 1024 * 1024;
            }
            return false;
        }
        public async static Task<string> CreateFileAsync(this IFormFile file,  params string[] roots)
        {
            string fileName = Guid.NewGuid().ToString() + file.FileName;
            string path = String.Empty;
           for(int i=0; i<roots.Length; i++) 
            {
                path = Path.Combine(path, roots[i]);
            }
            path = Path.Combine(path, fileName);
          
            using (FileStream fileStream = new FileStream(path, FileMode.Create))
            {
                await file.CopyToAsync(fileStream);
            }
            return fileName;
        }
    }
}
