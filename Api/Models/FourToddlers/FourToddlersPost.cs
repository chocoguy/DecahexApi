namespace Api.Models.FourToddlers
{
    public class FourToddlersPost
    {
        public int id { get; set; } = 0;
        public int idRepliedTo { get; set; } = 0;
        public bool isSticky { get; set; } = false;
        public string datePosted { get; set; } = "";
        public string? title { get; set; } = "";
        public string? post { get; set; } = "";
        public string? imageCaption { get; set; } = "";
        public string? imageLink { get; set; } = "";
        public string? smallImageLink { get; set; } = "";
        public bool imageDeleted { get; set; } = false;
        public bool imageSpoiler { get; set; } = false;
        public int replies { get; set; } = 0;
        public int imageReplies { get; set; } = 0;
        public bool imageIsWebm { get; set; } = false;
    }
}
