using Microsoft.AspNetCore.Http;
using System;
using System.IO;

namespace mvc2.Helpers
{
    public class DocumentSettings
    {
        public static string uploadFile(IFormFile file , string folderName)
        {
            string folderPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\Files", folderName);
            string fileName=$"{Guid.NewGuid()}{file.FileName}";
            string filePath = Path.Combine(folderPath,fileName);
            using var fileStreams = new FileStream(filePath, FileMode.Create);
            file.CopyTo(fileStreams);
            return fileName;
        }

        public static void deleteFile(string fileName, string folderName)
        {
            string folderPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\Files", folderName);
            if (File.Exists(folderPath))
            {
                File.Delete(folderPath);
            }
        }
    }
}
