namespace Api.Models
{
    public class FourChanPost
    {
        public int no { get; set; }
        public int resto { get; set; }
        public int ?sticky { get; set; } //op only if sticky
        public int ?closed { get; set; } //op only if thread closed
        public string now { get; set; } = "2023";
        public int time { get; set; } = 0;
        public string name { get; set; } = "Unknown";
        public string ?trip { get; set; } //if it has tripcode
        public string ?id { get; set; } //if it has id
        public string ?capcode { get; set; } //if it has capcode
        public string ?country { get; set; } //if country flags are enabled
        public string ?country_name { get; set; } //if country flags are enabled
        public string ?board_flag { get; set; } //if board flags are enabled
        public string ?flag_name { get; set; } //if board flags are enabled
        public string ?sub { get; set; } //OP Only if subject included
        public string ?com { get; set; } //if comment included
        //fields below appear if post has attachment
        public object tim { get; set; }
        public string ?filename { get; set; }
        public string ?ext { get; set; }
        public int fsize { get; set; }
        public string ?md5 { get; set; }
        public int w { get; set; }
        public int h { get; set; }
        public int tn_w { get; set; }
        public int tn_h { get; set; }
        public int filedeleted { get; set; }
        public int spoiler { get; set; }
        public int custom_spoiler { get; set; }
        //end
        public int ?replies { get; set; } //OP only
        public int ?images { get; set; } //OP only
        public int ?bumplimit { get; set; } //OP only
        public int? imagelimit { get; set; } //OP only
        public string? tag { get; set; } //OP only in swf
        public string? semantic_url { get; set; } //OP only
        public int ?since4pass { get; set; } //if user put 4pass in options
        public int ?unique_ips { get; set; } //OP only
        public int ?m_img { get; set; } //post that has mobile image
        public int ?archived { get; set; } //OP only
        public int ?archived_on { get; set; } //OP only

    }
    public class FourChanPostRoot
    {
        public List<FourChanPost> posts { get; set; }
    }
}