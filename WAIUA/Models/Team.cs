using System.Runtime.Serialization;

namespace WAIUA.Models
{
    public enum Team {
        [EnumMember(Value = "blue")]
        Blue, 
        [EnumMember(Value = "red")]
        Red
    }
}