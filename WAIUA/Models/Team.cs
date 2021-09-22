using System.Runtime.Serialization;

namespace WAIUA.Models
{
    public enum Team {
        [EnumMember(Value = "unknown")]
        Unknown,
        [EnumMember(Value = "blue")]
        Blue, 
        [EnumMember(Value = "red")]
        Red
    }

    public static class TeamHelper {
        public static Team GetTeamFromString(string team) => team.ToLower() == "blue" ? Team.Blue : team.ToLower() == "red" ? Team.Red : Team.Unknown;
    }
}