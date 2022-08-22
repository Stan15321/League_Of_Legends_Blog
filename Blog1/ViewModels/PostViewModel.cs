using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog1.ViewModels
{
    public class PostViewModel
    {
        public int Id { get; internal set; }

        public string Title { get; set; } = "";
        public string Body { get; set; } = "";

        public string Descriptions { get; set; } = "";
        public string Tags { get; set; } = "";
        public string Category { get; set; } = "";

        public string CurrentImage { get; set; } = "";

        public IFormFile Image { get; set; } =null;
        
    }
}
