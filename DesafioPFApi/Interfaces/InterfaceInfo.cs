using System.Text.Json.Serialization;

namespace DesafioPF.Interfaces
{
    public class InterfaceInfo
    {
        [JsonPropertyName("ifIndex")]
        public int IfIndex { get; set; }

        [JsonPropertyName("ifDescr")]
        public string IfDescr { get; set; }

        [JsonPropertyName("ifType")]
        public int IfType { get; set; }

        [JsonPropertyName("ifSpeed")]
        public long IfSpeed { get; set; }

        [JsonPropertyName("ifPhysAddress")]
        public string IfPhysAddress { get; set; }

        [JsonPropertyName("ifAdminStatus")]
        public int IfAdminStatus { get; set; }

        [JsonPropertyName("ifOperStatus")]
        public int IfOperStatus { get; set; }

        [JsonPropertyName("ipAddress")]
        public string IpAddress { get; set; }

        [JsonPropertyName("ipNetmask")]
        public string IpNetmask { get; set; }
    }
}
