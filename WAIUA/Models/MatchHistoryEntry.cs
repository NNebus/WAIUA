using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace WAIUA.Models
{

    public enum MatchResult
    {
        [EnumMember(Value = "Unknown")]
        Unknown,
        [EnumMember(Value = "Win")]
        Win,
        [EnumMember(Value = "Loss")]
        Loss,
        [EnumMember(Value = "Draw")]
        Draw
    }

    public class MatchHistoryEntry
    {
        public MatchResult MatchResult { get; set; }
        public string Id { get; set; }
        public Team Team { get; set; }
        public DateTime StartTime { get; set; }
    }
}
