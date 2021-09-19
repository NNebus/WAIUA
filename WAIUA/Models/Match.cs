namespace WAIUA.Models
{
    public class Match {
        public string Id {get; set;}
        public MatchType MatchType {get; set;}
        public Player[] Players { get; set; }
        public string Map { get; set; }
    }
}