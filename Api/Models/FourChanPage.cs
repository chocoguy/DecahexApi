using Api.Models;

namespace API.Models
{
    public class FourChanPageRoot
    {
        public List<Thread> threads { get; set; }
    }

    public class Thread 
    {
        public List<FourChanPost> posts { get; set; }
    }
}