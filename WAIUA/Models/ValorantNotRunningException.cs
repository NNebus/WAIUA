using System;

namespace WAIUA.Models
{
    public class ValorantNotRunningException : Exception
    {
        public ValorantNotRunningException(string message) : base(message){ 
            
        }
    }
}
