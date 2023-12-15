namespace Api.Models
{
    public class Paging
    {
        public string next { get; set; }
    }

    public class MALSearch
    {
        public List<MALNodeRecord> data { get; set; }
        public Paging paging { get; set; }
    }
}
