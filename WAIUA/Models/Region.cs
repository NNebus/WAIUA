using System.Runtime.Serialization;

namespace WAIUA.Models
{
    public enum Region {
        [EnumMember(Value = "na")]
        NorthAmerica,
        [EnumMember(Value = "de")]
        Germany,
        [EnumMember(Value = "unknown")]
        Region
    }
}