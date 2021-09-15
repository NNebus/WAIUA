using System.Runtime.Serialization;

namespace WAIUA.Models
{
    public enum Team {
        [EnumMember(Value = "blue")]
        blue, 
        [EnumMember(Value = "red")]
        red
    }
}