namespace Api.Models
{
    public class AnimeRecord
    {
        public int id_MAL { get; set; } = 0;
        public string title { get; set; } = "";
        public int episodes { get; set; } = 0;
        public string derivedSource { get; set; } = "";
        public string mediaType { get; set; } = "";
        public int year { get; set; } = 0;
        public string season { get; set; } = "";
        public string broadcastDay { get; set; } = "";
        public string poster_MAL { get; set; } = "";
        public DateTime started_MAL { get; set; } = DateTime.Parse("6/16/2002");
        public DateTime ended_MAL { get; set; } = DateTime.Parse("6/16/2002");
        public string description_MAL { get; set; } = "";
        public int rank_MAL { get; set; } = 0;
        public string airingStatus_MAL { get; set; } = "";
        public string studios_MAL { get; set; } = "";
        public float score_MAL { get; set; } = 0;
        public int usersWhoDropped_MAL { get; set; } = 0;
    }
}
