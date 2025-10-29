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

        public override string ToString()
        {
            return $"Nome: {SysName}\n" +
                    $"Descrição: {SysDescr}.\n" +
                    $"Id: {SysObjectID}\n" +
                    $"Tempo Ativo: {SysUpTime}\n" +
                    $"Contato: {SysContact}\n" +
                    $"Localização: {SysLocation}\n" +
                    $"{string.Join("\n", Interfaces.Select(i => $"\nInterface {i.IfIndex}:\n" +
                    $"Descrição: {i.IfDescr}\n" +
                    $"Tipo: {i.IfType}\n" +
                    $"Velocidade: {i.IfSpeed}\n" +
                    $"PhysAddress: {i.IfPhysAddress}\n" +
                    $"Status Admin: {i.IfAdminStatus}\n" +
                    $"OperStatus: {i.IfOperStatus}\n" +
                    $"IpAddress: {i.IpAddress}\n" +
                    $"Netmask: {i.IpNetmask}"))}";
        }
    }
}
