using DesafioPF.Interfaces;
using System.Text.Json.Serialization;

namespace DesafioPF.Models
{
    public class Dispositivo
    {
        [JsonPropertyName("sysName")]
        public string SysName { get; set; }

        [JsonPropertyName("sysDescr")]
        public string SysDescr { get; set; }

        [JsonPropertyName("sysObjectID")]
        public string SysObjectID { get; set; }

        [JsonPropertyName("sysUpTime")]
        public string SysUpTime { get; set; }

        [JsonPropertyName("sysContact")]
        public string SysContact { get; set; }

        [JsonPropertyName("sysLocation")]
        public string SysLocation { get; set; }

        [JsonPropertyName("interfaces")]
        public List<InterfaceInfo> Interfaces { get; set; }
    }
}
