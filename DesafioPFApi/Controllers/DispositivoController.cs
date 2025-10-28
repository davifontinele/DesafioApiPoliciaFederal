using DesafioPF.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace DesafioPF.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class DispositivoController : ControllerBase
    {
        private readonly ILogger<DispositivoController> _logger;

        public DispositivoController(ILogger<DispositivoController> logger)
        {
            _logger = logger;
        }

        [HttpPost]
        public async Task<IActionResult> Discover([FromBody] List<string> ip)
        {
            var todosDispositivos = SnmpSimulatorService.GetAllDevices();

            if (todosDispositivos == null)
            {
                return NotFound("Base de dados não encontrada.");
            }

            foreach (var item in ip)
            {
                var dispositivos = todosDispositivos.Values.FirstOrDefault(i => i.Interfaces.Any(a => a.IpAddress != null && a.IpAddress.Equals(ip)));

                if (dispositivos == null)
                {
                    continue;
                }

                var ipEncontrado = dispositivos.Interfaces.FirstOrDefault(i => i.IpAddress != null && i.IpAddress.Equals(ip));
                
                if (ipEncontrado != null)
                {
                    if (await NetboxService.DispositivoExiste(dispositivos.SysName))
                    {
                        await NetboxService.AtualizarDispositivo(1, dispositivos.SysDescr);
                    }
                    else
                    {
                        await NetboxService.CriarDispositivo(dispositivos.SysName, 1, 1, 1);
                    }
                    await NetboxService.CriarInterface(1, ipEncontrado.IfDescr, ipEncontrado.IfType);
                    await NetboxService.CriarIpAcesso(ipEncontrado.IpAddress + "/24", 1);
                }
            }
            return Ok("Processamento concluído.");
        }

        [HttpGet]
        public IActionResult ObterPorNome(string Name)
        {
            var todosDispositivos = SnmpSimulatorService.GetAllDevices();

            if (todosDispositivos == null)
            {
                return NotFound("Base de dados não encontrada.");
            }

            var dispositivo = todosDispositivos.FirstOrDefault(d => d.Value.SysName == Name);

            if (dispositivo.Value == null)
            {
                return NotFound("Dispositivo não encontrado.");
            }
            else
            {
                return Ok($"Nome: {dispositivo.Value.SysName}\n" +
                    $"Descrição: {dispositivo.Value.SysDescr}.\n" +
                    $"Id: {dispositivo.Value.SysObjectID}\n" +
                    $"Tempo Ativo: {dispositivo.Value.SysUpTime}\n" +
                    $"Contato: {dispositivo.Value.SysContact}\n" +
                    $"Localização: {dispositivo.Value.SysLocation}\n" +
                    $"{string.Join("\n",dispositivo.Value.Interfaces.Select(i => $"\nInterface {i.IfIndex}:\n" +
                    $"Descrição: {i.IfDescr}\n" +
                    $"Tipo: {i.IfType}\n" +
                    $"Velocidade: {i.IfSpeed}\n" +
                    $"PhysAddress: {i.IfPhysAddress}\n" +
                    $"Status Admin: {i.IfAdminStatus}\n" +
                    $"OperStatus: {i.IfOperStatus}\n" +
                    $"IpAddress: {i.IpAddress}\n" +
                    $"Netmask: {i.IpNetmask}"))}");
            }
        }
    }
}
