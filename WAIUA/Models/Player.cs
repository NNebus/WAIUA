using System.Collections.Generic;

namespace WAIUA.Models
{
    public class Player {
        public User User { get; set; }
        public string Region {  get; set; }
        public Agent Agent { get; set; }
        public string PartyId { get; set; }
        public Team Team { get; set; }
        public string Rank { get; set; }
        public string TrackerUrl { get; set; }

        public string Name { get; set; }
        public string Tag { get; set; }
        public string NameWithTag => $"{Name}#{Tag}";
        public string CardId { get; set; }
        public string TitleId { get; set; }
        public int AccountLevel { get; set; }
        public bool Incognito {  get; set; }
        public bool HideAccountLevel { get; set; }
        public List<MatchHistoryEntry> MatchHistory { get; set; }
    }
}