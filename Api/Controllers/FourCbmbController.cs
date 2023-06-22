using Api.Models;
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

        [HttpGet(Name = "GetThreadTest")]
        public async Task<List<FourCbmbPost>> Get()
        {
            //placeholder
            string board = "a";
            int threadId = 253965375;

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
                    fourCbmbPost.ThumbnailUrl = $"https://i.4cdn.org/{board}/{post.tim}s.jpg";
                }

                fourCbmbPost.Replies = (int)(post.replies != null ? post.replies : -1);
                fourCbmbPost.ImageReplies = (int)(post.images != null ? post.images : -1);

                fourCbmbPosts.Add(fourCbmbPost);
            }
            return fourCbmbPosts;
        }
    }
}