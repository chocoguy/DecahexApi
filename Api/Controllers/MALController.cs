using Api.Models;
using API.Controllers;
using Microsoft.AspNetCore.Mvc;
using System.Globalization;
using System.Runtime.CompilerServices;
using System.Text;

namespace Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class MALController : ControllerBase
    {
        private readonly ILogger<MALController> _logger;
        private readonly IHttpClientFactory _clientFactory;
        private readonly IConfiguration _configuration;
        private string _getAnimeInfoFields = "?fields=id,title,num_episodes,source,media_type,start_season,broadcast,main_picture,start_date,end_date,synopsis,rank,status,status,studios,mean,statistics";
        private TextInfo _textInfo = CultureInfo.CurrentCulture.TextInfo;

        public MALController(ILogger<MALController> logger, IHttpClientFactory clientFactory, IConfiguration configuration)
        {
            _logger = logger;
            _clientFactory = clientFactory;
            _configuration = configuration;
        }

        [HttpGet("Anime/{malId}")]
        public async Task<object> GetAnimeInfo(int malId)
        {
            

            try
            {
                var client = _clientFactory.CreateClient("MALClient");
                var res = await client.GetAsync($"/v2/anime/{malId}{_getAnimeInfoFields}");
                MALRecord currMALRecord = await res.Content.ReadFromJsonAsync<MALRecord>();

                AnimeRecord currAnimeRecord = new() {
                    id_MAL = currMALRecord!.id,
                    title = currMALRecord.title,
                    episodes = currMALRecord.num_episodes,
                    derivedSource = currMALRecord.source,
                    mediaType = currMALRecord.media_type,
                    year = currMALRecord.start_season == null ? 0 : currMALRecord.start_season.year,
                    season = currMALRecord.start_season == null ? null : _textInfo.ToTitleCase(currMALRecord.start_season.season),
                    broadcastDay = currMALRecord.broadcast == null ? null : _textInfo.ToTitleCase(currMALRecord.broadcast.day_of_the_week),
                    poster_MAL = currMALRecord.main_picture == null ? null : currMALRecord.main_picture.medium,
                    started_MAL = currMALRecord.start_date,
                    ended_MAL = currMALRecord.end_date,
                    description_MAL = currMALRecord.synopsis,
                    rank_MAL = currMALRecord.rank,
                    airingStatus_MAL = currMALRecord.status,
                    studios_MAL = GenerateStudioString(currMALRecord.studios),
                    score_MAL = (float)currMALRecord.mean,
                    usersWhoDropped_MAL = int.Parse(currMALRecord.statistics.status.dropped)
                };


                return currAnimeRecord;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return StatusCode(500);
            }
        }

        [HttpPost("AnimeSearch")]
        public async Task<object> SearchAnime()
        {
            try
            {
                var client = _clientFactory.CreateClient("MALClient");
                string query = await GetTextFromBody(Request);
                var res = await client.GetAsync($"/v2/anime?q={query}&limit=8");
                MALSearch currMALSearch = await res.Content.ReadFromJsonAsync<MALSearch>();

                return currMALSearch;

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return StatusCode(500);
            }
        }

        private string GenerateStudioString(List<Studio> studios)
        {
            string generatedString = "";

            if(studios == null)
            {
                return "";
            }

            foreach (Studio studio in studios)
            {
                generatedString += $"{studio.name}, ";
            }

            generatedString = generatedString.TrimEnd(',', ' ');

            return generatedString;
        }

        private static async Task<string> GetTextFromBody(HttpRequest req)
        {
            if(!req.Body.CanSeek){req.EnableBuffering();}
            req.Body.Position = 0;

            var reader = new StreamReader(req.Body, Encoding.UTF8);
            var reqString = await reader.ReadToEndAsync().ConfigureAwait(false);

            return reqString;
        }

    }
}
