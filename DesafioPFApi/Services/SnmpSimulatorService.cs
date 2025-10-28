using DesafioPF.Models;
using Newtonsoft.Json;

namespace DesafioPF.Services
{
    public static class SnmpSimulatorService
    {
        public static Dictionary<string, Dispositivo> GetAllDevices()
        {
            var json = File.ReadAllText("simulated_snmp.json");
            return JsonConvert.DeserializeObject<Dictionary<string, Dispositivo>>(json);
        }
    }
}
