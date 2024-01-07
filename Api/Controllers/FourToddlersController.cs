using Api.Models;
using Api.Models.FourToddlers;
using API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class FourToddlersController : ControllerBase
    {
        private readonly IHttpClientFactory _clientFactory;
        private readonly IConfiguration _configuration;
        private string localBaseURL;

        public FourToddlersController(IHttpClientFactory clientFactory, IConfiguration configuration)
        {
            _clientFactory = clientFactory;
            _configuration = configuration;
            localBaseURL = _configuration.GetValue<string>("HostedURLDev");
        }

        [HttpGet("thread/{board}/{threadId}")]
        public async Task<List<FourToddlersPost>> GetThread(string board, int threadId)
        {
            var client = _clientFactory.CreateClient("fourtoddlersthread");
            var response = await client.GetAsync($"https://a.4cdn.org/{board}/thread/{threadId}.json");
            var fetchedPosts = await response.Content.ReadFromJsonAsync<FourChanPostRoot>();
            var fourToddlersPosts = new List<FourToddlersPost>();

            foreach (var post in  fetchedPosts.posts)        
            {
                FourToddlersPost FourTPost = new();

                FourTPost.id = post.no;
                FourTPost.idRepliedTo = post.resto;
                FourTPost.isSticky = post.sticky == 1 ? true : false;
                FourTPost.datePosted = post.now;
                FourTPost.title = post.sub;
                FourTPost.post = post.com;
                FourTPost.replies = (int)(post.replies != null ? post.replies : -1);
                FourTPost.imageReplies = (int)(post.images != null ? post.images : -1);

                if (post.tim != null)
                {
                    FourTPost.imageSpoiler = post.spoiler == 1 ? true : false;
                    FourTPost.imageDeleted = post.filedeleted == 1 ? true : false;
                    FourTPost.imageCaption = post.filename;
                    if(post.ext == ".webm")
                    {
                        FourTPost.imageIsWebm = true;
                    }
                    FourTPost.imageLink = $"https://i.4cdn.org/{board}/{post.tim}{post.ext}";
                    FourTPost.smallImageLink = $"https://i.4cdn.org/{board}/{post.tim}s.jpg";
                }

                fourToddlersPosts.Add(FourTPost);

            }

            return fourToddlersPosts;

        }

        [HttpGet("page/{board}/{pageNumber}")]
        public async Task<List<FourToddlersPost>> GetPage(string board, int pageNumber)
        {
            var client = _clientFactory.CreateClient("fourtoddlerspage");
            var response = await client.GetAsync($"https://a.4cdn.org/{board}/{pageNumber}.json");
            var fetchedThreads = await response.Content.ReadFromJsonAsync<FourChanPageRoot>();
            var fourToddlersPosts = new List<FourToddlersPost>();

            foreach (var thread in fetchedThreads.threads)
            {

                //OP only
                    FourToddlersPost FourTPost = new();

                    FourTPost.id = thread.posts[0].no;
                    FourTPost.idRepliedTo = thread.posts[0].resto;
                    FourTPost.isSticky = thread.posts[0].sticky == 1 ? true : false;
                    FourTPost.datePosted = thread.posts[0].now;
                    FourTPost.title = thread.posts[0].sub;
                    FourTPost.post = thread.posts[0].com;
                    FourTPost.replies = (int)(thread.posts[0].replies != null ? thread.posts[0].replies : -1);
                    FourTPost.imageReplies = (int)(thread.posts[0].images != null ? thread.posts[0].images : -1);

                    if (thread.posts[0].tim != null)
                    {
                        FourTPost.imageSpoiler = thread.posts[0].spoiler == 1 ? true : false;
                        FourTPost.imageDeleted = thread.posts[0].filedeleted == 1 ? true : false;
                        FourTPost.imageCaption = thread.posts[0].filename;
                        if (thread.posts[0].ext == ".webm")
                        {
                            FourTPost.imageIsWebm = true;
                        }
                        FourTPost.imageLink = $"https://i.4cdn.org/{board}/{thread.posts[0].tim}{thread.posts[0].ext}";
                        FourTPost.smallImageLink = $"https://i.4cdn.org/{board}/{thread.posts[0].tim}s.jpg";
                    }

                fourToddlersPosts.Add(FourTPost);

            }

            return fourToddlersPosts;

        }

        [HttpGet("catalog/{board}")]
        public async Task<List<FourToddlersPost>> GetCatalog(string board)
        {
            var client = _clientFactory.CreateClient("fourtoddlerscatalog");
            var response = await client.GetAsync($"https://a.4cdn.org/{board}/catalog.json");
            var fetchedCatalog = await response.Content.ReadFromJsonAsync<List<FourChanCatalogRoot>>();
            var fourToddlersPosts = new List<FourToddlersPost>();

            foreach(var page in fetchedCatalog)
            {
                foreach(var post in page.threads)
                {
                    FourToddlersPost FourTPost = new();

                    FourTPost.id = post.no;
                    FourTPost.idRepliedTo = post.resto;
                    FourTPost.isSticky = post.sticky == 1 ? true : false;
                    FourTPost.datePosted = post.now;
                    FourTPost.title = post.sub;
                    FourTPost.post = post.com;
                    FourTPost.replies = (int)(post.replies != null ? post.replies : -1);
                    FourTPost.imageReplies = (int)(post.images != null ? post.images : -1);

                    if (post.tim != null)
                    {
                        FourTPost.imageSpoiler = post.spoiler == 1 ? true : false;
                        FourTPost.imageDeleted = post.filedeleted == 1 ? true : false;
                        FourTPost.imageCaption = post.filename;
                        if (post.ext == ".webm")
                        {
                            FourTPost.imageIsWebm = true;
                        }
                        FourTPost.imageLink = $"https://i.4cdn.org/{board}/{post.tim}{post.ext}";
                        FourTPost.smallImageLink = $"https://i.4cdn.org/{board}/{post.tim}s.jpg";
                    }

                    fourToddlersPosts.Add(FourTPost);
                }
            }

            return fourToddlersPosts;

        }



    }
}
