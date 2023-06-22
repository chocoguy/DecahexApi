namespace Api.Models 
{
    public class FourCbmbPost
    {
        public int Id { get; set; } = 0;
        public int IdRepliedTo { get; set; } = 0;
        public bool IsSticky { get; set; } = false;
        public bool IsClosed { get; set; } = false;
        public string ?DatePostedString { get; set; }
        public string ?Title { get; set; }
        public string ?Content { get; set; }
        public string ?ImageName { get; set; }
        public int ?ImageSize { get; set; }
        public int ?ImageWidth { get; set; }
        public int ?ImageHeight { get; set; }
        public int ?ThumbnailImageWidth { get; set; }
        public int ?ThumbnailImageHeight { get;set; }
        public bool ImageDeleted { get; set; } = false;
        public bool ImageSpoiler { get; set; } = false;
        public int Replies { get; set; } = 0;
        public int ImageReplies { get; set; } = 0;
        public string ?ImageUrl { get; set; } 
        public string ?ThumbnailUrl { get; set; }
    }
}