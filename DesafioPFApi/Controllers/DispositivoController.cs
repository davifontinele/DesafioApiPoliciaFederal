using DesafioPF.Models;
using DesafioPF.Services;
using Microsoft.AspNetCore.Mvc;

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

        [HttpGet("GetByName")]
        public IActionResult GetByName(string Name)
        {
            var todosDispositivos = SnmpSimulatorService.GetAllDevices();

            if (todosDispositivos == null)
            {
                return NotFound("Base de dados não encontrada.");
            }
            else
            {
                var dispositivo = todosDispositivos.FirstOrDefault(d => d.Value.SysName == Name);
                if (dispositivo.Value != null)
                {
                    return Ok(dispositivo.ToString());
                }
                return NotFound("Dispositivo não encontrado.");
            }
        }

        [HttpGet("GetByListName")]
        public IActionResult GetListByName([FromQuery] List<string> Name)
        {
            var todosDispositivos = SnmpSimulatorService.GetAllDevices();

            if (todosDispositivos == null)
            {
                return NotFound("Base de dados não encontrada.");
            }
            else
            {
                List<Dispositivo> encontrados = new List<Dispositivo>();
                foreach (var device in todosDispositivos)
                {
                    foreach (var nome in Name)
                    {
                        if (device.Value.SysName == nome)
                        {
                            encontrados.Add(device.Value);
                        }
                    }
                }
                return Ok(encontrados);
            }
        }

        [HttpGet("GetAllDevices")]
        public IActionResult GetAll()
        {
            var todosDispositivos = SnmpSimulatorService.GetAllDevices();
            if (todosDispositivos == null)
            {
                return NotFound("Base de dados não encontrada.");
            }
            else
            {
                return Ok(todosDispositivos);
            }
        }

        [HttpGet("GetAllInterfaceDevice")]
        public IActionResult GetAllInterfaceDevice(string name)
        {
            var todosDispositivos = SnmpSimulatorService.GetAllDevices();

            if (todosDispositivos == null)
            {
                return NotFound("Base de dados não encontrada.");
            }
            else
            {
                var dispositivo = todosDispositivos.FirstOrDefault(d => d.Value.SysName == name);
                if (dispositivo.Value != null)
                {
                    return Ok(dispositivo.Value.Interfaces);
                }
                return NotFound("Dispositivo não encontrado.");
            }
        }

        [HttpGet("GetInterfaceDevice")]
        public IActionResult GetInterfaceDevice(string name, int interfaceIndex)
        {
            var todosDispositivos = SnmpSimulatorService.GetAllDevices();

            if (todosDispositivos == null)
            {
                return NotFound("Base de dados não encontrada.");
            }
            else
            {
                foreach (var dispositivo in todosDispositivos)
                {
                    if (dispositivo.Value.SysName == name)
                    {
                        if (dispositivo.Value.Interfaces.Count < interfaceIndex)
                        {
                            return NotFound("Interface não encontrada.");
                        }
                        else
                        {
                            return Ok(dispositivo.Value.Interfaces.FirstOrDefault(i => i.IfIndex == interfaceIndex));
                        } 
                    }
                }
                return NotFound("Dispositivo ou interface não encontrado.");
            }
        }

        [HttpGet("GetAllLocations")]
        public IActionResult GetAllLocations()
        {
            var todosDispositivos = SnmpSimulatorService.GetAllDevices();

            if (todosDispositivos == null)
            {
                return NotFound("Base de dados não encontrada.");
            }

            var locations = todosDispositivos.Values.Select(d => d.SysLocation);

            return Ok(locations);
        }

        [HttpGet("GetDeviceLocation")]
        public IActionResult GetDeviceLocation(string name)
        {
            var todosDispositivos = SnmpSimulatorService.GetAllDevices();

            if (todosDispositivos == null)
            {
                return NotFound("Base de dados não encontrada.");
            }
            else
            {
                foreach (var dispositivo in todosDispositivos)
                {
                    if (dispositivo.Value.SysName == name)
                    {
                        return Ok(dispositivo.Value.SysLocation);
                    }
                }
                return NotFound("Dispositivo não encontrado.");
            }
        }
    }
}
