using System.Text;
using System.Text.Json;

namespace DesafioPF.Services
{
    public static class NetboxService
    {
        private static readonly HttpClient client = new HttpClient();
        private static readonly string api = "https://tosu1691.cloud.netboxapp.com/";
        private static readonly string chave = "d40c28b5528138770029b8f62ca6bdb8a5c110cf";

        static NetboxService()
        {
            client.DefaultRequestHeaders.Add("Authorization", $"Token {chave}");
            client.DefaultRequestHeaders.Add("Accept", "application/json");
        }
        public static async Task<bool> DispositivoExiste(string deviceName)
        {
            var response = await client.GetAsync($"{api}/dcim/devices/?name={deviceName}");
            var content = await response.Content.ReadAsStringAsync();
            return content.Contains(deviceName);
        }
        public static async Task CriarDispositivo(string name, int deviceTypeId, int deviceRoleId, int siteId)
        {
            var dispositivo = new
            {
                name = name,
                device_type = deviceTypeId,
                device_role = deviceRoleId,
                site = siteId
            };

            var json = JsonSerializer.Serialize(dispositivo);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            await client.PostAsync($"{api}/dcim/devices/", content);
        }
        public static async Task AtualizarDispositivo(int deviceId, string newDescription)
        {
            var dispositivo = new
            {
                comments = newDescription
            };

            var json = JsonSerializer.Serialize(dispositivo);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            await client.PatchAsync($"{api}/dcim/devices/{deviceId}/", content);
        }
        public static async Task CriarInterface(int deviceId, string name, int typeId)
        {
            var dispositivoInterface = new
            {
                device = deviceId,
                name = name,
                type = typeId
            };

            var json = JsonSerializer.Serialize(dispositivoInterface);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            await client.PostAsync($"{api}/dcim/interfaces/", content);
        }
        public static async Task CriarIpAcesso(string address, int interfaceId)
        {
            var dispositivoIpAcesso = new
            {
                address = address,
                assigned_object_type = "dcim.interface",
                assigned_object_id = interfaceId
            };

            var json = JsonSerializer.Serialize(dispositivoIpAcesso);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            await client.PostAsync($"{api}/ipam/ip-addresses/", content);
        }
    }
}
