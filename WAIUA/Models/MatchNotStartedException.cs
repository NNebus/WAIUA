using System;

namespace WAIUA.Models
{
    public class MatchNotStartedException : Exception
    {
        public MatchNotStartedException(string message) : base(message)
        {

        }
    }
}
