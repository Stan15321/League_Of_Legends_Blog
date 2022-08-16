﻿using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog1.Data.FileManager
{
    public class FileManager: IFileManager
    {
        private string _imagePath;

        public FileManager(IConfiguration config)
        {
            _imagePath = config["Path:Images"];
            
        }

        public FileStream ImageStream(string image)
        {
            return new FileStream(Path.Combine(_imagePath, image), FileMode.Open, FileAccess.Read);
            
        }

        public async Task<string> SaveImage(IFormFile image)
        {
            try
            {
                var safe_path = Path.Combine(_imagePath);
                if (Directory.Exists(_imagePath))
                {
                    Directory.CreateDirectory(safe_path);
                }
                var mime = image.FileName.Substring(image.FileName.LastIndexOf('.'));
                var fileName = $"img_{DateTime.Now.ToString("dd-MM-yyyy-HH-mm-ss")}{mime}";

                using (var fileStream = new FileStream(Path.Combine(safe_path, fileName), FileMode.Create))
                {
                    await image.CopyToAsync(fileStream);
                }
                return fileName;
            }
            catch(Exception e)
            {
                Console.WriteLine(e.Message);
                
                return "Error";
            }
        }
    }
}
