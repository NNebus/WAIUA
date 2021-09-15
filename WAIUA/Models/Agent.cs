using System.Runtime.Serialization;

namespace WAIUA.Models
{
    public enum Agent {
        [EnumMember(Value = "Brimstone")]
        Brimstone,
        [EnumMember(Value = "Viper")]
        Viper, 
        [EnumMember(Value = "Omen")]
        Omen,
        [EnumMember(Value = "Killjoy")]
        Killjoy,
        [EnumMember(Value = "Cypher")]
        Cypher,
        [EnumMember(Value = "Sova")]
        Sova,
        [EnumMember(Value = "Sage")]
        Sage, 
        [EnumMember(Value = "Phoenix")]
        Phoenix,
        [EnumMember(Value = "Jett")]
        Jett,
        [EnumMember(Value = "Reyna")]
        Reyna, 
        [EnumMember(Value = "Raze")]
        Raze,
        [EnumMember(Value = "Breach")]
        Breach,
        [EnumMember(Value = "Syke")]
        Skye, 
        [EnumMember(Value = "Yoru")]
        Yoru, 
        [EnumMember(Value = "Astra")]
        Astra,
        [EnumMember(Value = "Kay\\O")]
        KayO
    }
}