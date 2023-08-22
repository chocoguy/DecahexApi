using System;

namespace Api.Helpers
{
    public class FourCbmbFilterMachine
    {


        public bool FilterAnimeThreadByTitle(string threadTitle)
        {
            if(threadTitle.Contains("Thing I dont like"))
            {
                return false;
            }
            else
            {
                return true;
            }
        }

    }
}