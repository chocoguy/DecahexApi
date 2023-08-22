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
        private readonly IConfiguration _configuration;
        private string localBaseURL;

        public FourCbmbController(ILogger<FourCbmbController> logger, IHttpClientFactory clientFactory, IConfiguration configuration)
        {
            _logger = logger;
            _clientFactory = clientFactory;
            _configuration = configuration;
            localBaseURL = _configuration.GetValue<string>("HostedURLDev");
        }

        [HttpGet("Thread/{board}/{threadId}")]
        public async Task<List<FourCbmbPost>> GetThread(string board, int threadId)
        {
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
                    if(post.ext == ".webm")
                    {
                        fourCbmbPost.ImageUrl = $"{localBaseURL}FourCbmb/webm/{board}/{post.tim}";
                    }
                    else
                    {
                        fourCbmbPost.ImageUrl = $"https://i.4cdn.org/{board}/{post.tim}{post.ext}";
                    }
                    //temp hardcoded
                    //fourCbmbPost.ThumbnailUrl = $"{localBaseURL}FourCbmb/thumbnailimage/{board}/{post.tim}";
                    fourCbmbPost.ThumbnailUrl = $"https://i.4cdn.org/{board}/{post.tim}s.jpg";
                }

                fourCbmbPost.Replies = (int)(post.replies != null ? post.replies : -1);
                fourCbmbPost.ImageReplies = (int)(post.images != null ? post.images : -1);

                fourCbmbPosts.Add(fourCbmbPost);
            }
            return fourCbmbPosts;
        }

        [HttpGet("Page/{board}/{page}")]
        public async Task<List<FourCbmbThread>> GetPage(string board, int page)
        {
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
                        if(post.ext == ".webm")
                        {
                            fourCbmbPost.ImageUrl = $"{localBaseURL}FourCbmb/webm/{board}/{post.tim}";
                        }
                        else
                        {
                            fourCbmbPost.ImageUrl = $"https://i.4cdn.org/{board}/{post.tim}{post.ext}";
                        }
                        //temp hardcoded
                        //fourCbmbPost.ThumbnailUrl = $"{localBaseURL}FourCbmb/thumbnailimage/{board}/{post.tim}";
                        fourCbmbPost.ThumbnailUrl = $"https://i.4cdn.org/{board}/{post.tim}s.jpg";
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

        [HttpGet("webm/{board}/{tim}")]
        public async Task<IActionResult> GetWebm(string board, string tim)
        {
            var client = _clientFactory.CreateClient("fourcbmbwebm");
            var response = await client.GetAsync($"https://i.4cdn.org/{board}/{tim}.webm");
            var webm = await response.Content.ReadAsByteArrayAsync();
            return File(webm, "video/webm");
        }

    }
}