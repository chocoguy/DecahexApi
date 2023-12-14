namespace Api.Models
{
    public class Broadcast
    {
        public string day_of_the_week { get; set; }
        public string start_time { get; set; }
    }

    public class MainPicture
    {
        public string medium { get; set; }
        public string large { get; set; }
    }
    public class MALRecord
    {
        public int id { get; set; }
        public string title { get; set; }
        public MainPicture? main_picture { get; set; }
        public int num_episodes { get; set; }
        public string? source { get; set; }
        public string media_type { get; set; }
        public StartSeason? start_season { get; set; }
        public Broadcast? broadcast { get; set; }
        public string? start_date { get; set; }
        public string? end_date { get; set; }
        public string? synopsis { get; set; }
        //nullable
        public int rank { get; set; }
        public string status { get; set; }
        public List<Studio> studios { get; set; }
        //nullable
        public double mean { get; set; }
        public Statistics statistics { get; set; }
    }

    public class StartSeason
    {
        public int year { get; set; }
        public string season { get; set; }
    }

    public class Statistics
    {
        public Status status { get; set; }
        public int num_list_users { get; set; }
    }

    public class Status
    {
        public string watching { get; set; }
        public string completed { get; set; }
        public string on_hold { get; set; }
        //nullable
        public string dropped { get; set; }
        public string plan_to_watch { get; set; }
    }

    public class Studio
    {
        public int id { get; set; }
        public string name { get; set; }
    }

}
