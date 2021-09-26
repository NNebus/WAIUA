namespace WAIUA.Models
{
    public class RankedMatch
    {
        public string Id { get; set; }
        public int RankedRatingAfterUpdate { get; set; }
        public int RankedRatingBeforeUpdate { get; set; }
        public int RankedRatingEarned { get; set; }
        public int TierAfterUpdate { get; set; }
        public int TierBeforeUpdate { get; set; }
        public string SessionId { get; set; }
        public string MapId { get; set; }
    }
}
