using Api.Models;
using API.Models;
using Microsoft.AspNetCore.Mvc;
using System.Text;

namespace API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class FourCbmbController : ControllerBase 
    {
        private readonly ILogger<FourCbmbController> _logger;
        private readonly IHttpClientFactory _clientFactory;

        public FourCbmbController(ILogger<FourCbmbController> logger, IHttpClientFactory clientFactory)
        {
            _logger = logger;
            _clientFactory = clientFactory;
        }

        [HttpGet("GetThreadTest")]
        public async Task<List<FourCbmbPost>> Get()
        {
            //placeholder
            string board = "a";
            int threadId = 253900388;

            var client = _clientFactory.CreateClient("fourcbmbthread");
            var response = await client.GetAsync($"https://a.4cdn.org/{board}/thread/{threadId}.json");
            var fetchedPosts = await response.Content.ReadFromJsonAsync<FourChanPostRoot>();
            var fourCbmbPosts = new List<FourCbmbPost>();
            foreach (var post in fetchedPosts.posts)
            {
                var fourCbmbPost = new FourCbmbPost();
                fourCbmbPost.Id = post.no;
                fourCbmbPost.IdRepliedTo = post.resto;
                fourCbmbPost.IsSticky = post.sticky == 1 ? true : false;
                fourCbmbPost.IsClosed = post.closed == 1 ? true : false;
                fourCbmbPost.DatePostedString = post.now;
                fourCbmbPost.Title = post.sub;
                fourCbmbPost.Content = post.com;

                if(post.tim != null)
                {
                    fourCbmbPost.ImageName = post.filename;
                    fourCbmbPost.ImageSize = post.fsize;
                    fourCbmbPost.ImageWidth = post.w;
                    fourCbmbPost.ImageHeight = post.h;
                    fourCbmbPost.ThumbnailImageWidth = post.tn_w;
                    fourCbmbPost.ThumbnailImageHeight = post.tn_h;
                    fourCbmbPost.ImageDeleted = post.filedeleted == 1 ? true : false;
                    fourCbmbPost.ImageSpoiler = post.spoiler == 1 ? true : false;
                    fourCbmbPost.ImageUrl = $"https://i.4cdn.org/{board}/{post.tim}{post.ext}";
                    //temp hardcoded
                    fourCbmbPost.ThumbnailUrl = $"https://localhost:7285/FourCbmb/thumbnailimage/{board}/{post.tim}";
                }

                fourCbmbPost.Replies = (int)(post.replies != null ? post.replies : -1);
                fourCbmbPost.ImageReplies = (int)(post.images != null ? post.images : -1);

                fourCbmbPosts.Add(fourCbmbPost);
            }
            return fourCbmbPosts;
        }

        [HttpGet("GetPageTest")]
        public async Task<List<FourCbmbThread>> GetPage()
        {
            //placeholder vals
            string board = "a";
            int page = 1;

            var client = _clientFactory.CreateClient("fourcbmbpage");
            var response = await client.GetAsync($"https://a.4cdn.org/{board}/{page}.json");
            var fetchedThreads = await response.Content.ReadFromJsonAsync<FourChanPageRoot>();
            var fourCbmbThreads = new List<FourCbmbThread>();
            foreach (var thread in fetchedThreads.threads)
            {
                var fourCbmbThread = new FourCbmbThread();
                fourCbmbThread.posts = new List<FourCbmbPost>();
                foreach (var post in thread.posts)
                {
                    var fourCbmbPost = new FourCbmbPost();
                    fourCbmbPost.Id = post.no;
                    fourCbmbPost.IdRepliedTo = post.resto;
                    fourCbmbPost.IsSticky = post.sticky == 1 ? true : false;
                    fourCbmbPost.IsClosed = post.closed == 1 ? true : false;
                    fourCbmbPost.DatePostedString = post.now;
                    fourCbmbPost.Title = post.sub;
                    fourCbmbPost.Content = post.com;

                    if(post.tim != null)
                    {
                        fourCbmbPost.ImageName = post.filename;
                        fourCbmbPost.ImageSize = post.fsize;
                        fourCbmbPost.ImageWidth = post.w;
                        fourCbmbPost.ImageHeight = post.h;
                        fourCbmbPost.ThumbnailImageWidth = post.tn_w;
                        fourCbmbPost.ThumbnailImageHeight = post.tn_h;
                        fourCbmbPost.ImageDeleted = post.filedeleted == 1 ? true : false;
                        fourCbmbPost.ImageSpoiler = post.spoiler == 1 ? true : false;
                        fourCbmbPost.ImageUrl = $"https://i.4cdn.org/{board}/{post.tim}{post.ext}";
                        //temp hardcoded
                        fourCbmbPost.ThumbnailUrl = $"https://localhost:7285/FourCbmb/thumbnailimage/{board}/{post.tim}";
                    }

                    fourCbmbPost.Replies = (int)(post.replies != null ? post.replies : -1);
                    fourCbmbPost.ImageReplies = (int)(post.images != null ? post.images : -1);

                    fourCbmbThread.posts.Add(fourCbmbPost);
                }
                fourCbmbThreads.Add(fourCbmbThread);
            }
            return fourCbmbThreads;
        }


        [HttpGet("thumbnailimage/{board}/{tim}")]
        public async Task<IActionResult> GetThumbnailImage(string board, string tim)
        {
            //We cannot fetch images from the frontend beacause of CORS
            //This endpoint will act as a middleman to fetch the image from 4chan and return it to the frontend
            var client = _clientFactory.CreateClient("fourcbmbimage");
            var response = await client.GetAsync($"https://i.4cdn.org/{board}/{tim}s.jpg");
            var image = await response.Content.ReadAsByteArrayAsync();
            return File(image, "image/jpeg");
        }
    }
}