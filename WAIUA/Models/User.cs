namespace WAIUA.Models
{
    public class User {
        public string Id {get; set;}
        public Region Region {get; set;}
        public int AccountLevel { get; set; }
        public Rank Rank { get; set; }
    }
}