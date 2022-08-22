using Blog1.Data;
using Blog1.Data.FileManager;
using Blog1.Data.Repository;
using Blog1.Models;
using Blog1.Models.Comments;
using Blog1.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace Blog1.Controllers
{
    public class HomeController : Controller
    {
        private IRepository _repo;
        private IFileManager _fileManager;

        public HomeController(
            IRepository repo,
            IFileManager fileManager)
        {
            _repo = repo;
            _fileManager = fileManager;
           
        }
        private readonly ILogger<HomeController> _logger;
        private AppDbContext _ctx;

        

        public IActionResult Index(string Category)
        {

            var posts = string.IsNullOrEmpty(Category) ? _repo.GetAllPosts() : _repo.GetAllPosts(Category);
            //boolean ? true : false 1=1? run : ignore;
            return View(posts);
        }

        public IActionResult Privacy()
        {
            return View();
        }
        public IActionResult Post(int id)
        {
            var post = _repo.GetPost(id);
            return View(post);
        }
        [HttpGet("/Image/{image}")]
        public IActionResult Image(string image)
        {
            var mime = image.Substring(image.LastIndexOf('.') + 1);
            return new FileStreamResult(_fileManager.ImageStream(image), $"image/{mime}");
        }
        [HttpPost]
        public async Task <IActionResult> Comment(CommentViewModel vm) 
        {
            if (!ModelState.IsValid)
            {
                return RedirectToAction("Post",new { id = vm.PostId });
            }
            var post = _repo.GetPost(vm.PostId);
            if (vm.MainCommentId == 0)
            {
                post.MainComments = post.MainComments ?? new List<MainComment>();

                post.MainComments.Add(new MainComment
                {
                    Message = vm.Message,
                    Created = DateTime.Now,
                });
                _repo.UpdatePost(post);
            }
            else
            {
                var comment = new SubComment
                {
                    MainCommentId = vm.MainCommentId,
                    Message = vm.Message,
                    Created = DateTime.Now,
                };
                _repo.AddSubComment(comment);
            }
            await _repo.SaveChangesAsync();
            return RedirectToAction("Post", new { id = vm.PostId });
        }

   
            [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
