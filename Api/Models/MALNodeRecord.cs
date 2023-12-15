namespace Api.Models
{
    public class Datum
    {
        public MALNodeRecord node { get; set; }
    }

    public class MALNodeRecord
    {
        public int id { get; set; }
        public string title { get; set; }
        public MainPicture main_picture { get; set; }
    }
}
