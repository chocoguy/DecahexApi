using Api.Models;
using API.Controllers;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class MALController : ControllerBase
    {
        private readonly ILogger<MALController> _logger;
        private readonly IHttpClientFactory _clientFactory;
        private readonly IConfiguration _configuration;

        public MALController(ILogger<MALController> logger, IHttpClientFactory clientFactory, IConfiguration configuration)
        {
            _logger = logger;
            _clientFactory = clientFactory;
            _configuration = configuration;
        }

        [HttpGet("Anime/{malId}")]
        public async Task<AnimeRecord> GetAnimeInfo(int malId)
        {
            AnimeRecord currAnimeRecord = new AnimeRecord();


            return currAnimeRecord;
        }

    }
}
