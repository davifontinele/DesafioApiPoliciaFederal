# Desafio Técnico: Network Discovery & Documentation API

Este projeto é uma API REST desenvolvida para simular o processo de documentação da infraestrutura de rede de uma empresa, integrando com a ferramenta NetBox.

# O que o projeto faz 📚
<ul>
  <li>Recebe uma lista de IPs via Https</li>
  <li>Busca dispositivos via SNMP</li>
  <li>Verifica se o dispositivo já existe no inventário do NetBox</li>
  <ul>
    <li>Caso exista, cria ou atualiza os dispositivos, interfaces e IPs</li>
  </ul>
  <li>Faz buscas com base no nome dos dispositivos</li>
</ul>

# Como configurar o ambiente ⚙️

## Pré-Requisitos

<ul>
  <li>Docker</li>
  <li>Docker Compose</li>
  <li>Git</li>
  <li>Visual Studio</li>
  <li>C# .NET</li>
</ul>

# Como executar a aplicação 🚀

<ol>
  <li>Clone o repositório</li>
  <li>Execute todos os serviços com Docker Compose</li>
  <ul>
    <li>docker-compose up --build</li>
  </ul>
  <li>Isso iniciará:</li>
  <ul>
    <li>NetBox</li>
    <li>Worker e housekeeping do NetBox</li>
    <li>PostgreSQL</li>
    <li>Redis</li>
    <li>API</li>
  </ul>
  <li>Acesse:</li>
  <ul>
    <li>NetBox: http://localhost:8000</li>
    <li>API: http://localhost:5000/swagger</li>
  </ul>
</ol>

# Como usar a API 🧪

## 📌 Endpoint: POST /Discover

Envia uma lista de IPs para descoberta e registro.

## Exemplo com curl:
```bash
curl -X 'POST' \
  'http://localhost:5000/api/v1/Dispositivo' \
  -H 'accept: */*' \
  -H 'Content-Type: application/json' \
  -d '[
  "192.168.1.1",
  "10.0.0.1"
]'
```
<br>

## 📌 Endpoint: GET /GetByName

Retorna o dispositivo especificado.

## Exemplo com curl:
```bash
curl -X 'GET' \
  'https://localhost:7130/api/v1/Dispositivo/GetByName?Name=core-router-sp-01' \
  -H 'accept: */*'
```
## Retorno (Formatado com ToString)
```bash
[192.168.1.1, Nome: core-router-sp-01
Descrição: Cisco IOS XE Software, Version 16.09.03, Catalyst L3 Switch Software.
Id: 1.3.6.1.4.1.9.1.2298
Tempo Ativo: 157680000
Contato: NOC Team - noc@empresa.com.br
Localização: Data Center Sao Paulo - Rack A12

Interface 1:
Descrição: GigabitEthernet0/0
Tipo: 6
Velocidade: 1000000000
PhysAddress: 00:1a:2b:3c:4d:00
Status Admin: 1
OperStatus: 1
IpAddress: 192.168.1.1
Netmask: 255.255.255.248

Interface 2:
Descrição: GigabitEthernet0/1
Tipo: 6
Velocidade: 1000000000
PhysAddress: 00:1a:2b:3c:4d:01
Status Admin: 1
OperStatus: 1
IpAddress: 10.0.0.1
Netmask: 255.255.255.252

Interface 3:
Descrição: GigabitEthernet0/2
Tipo: 6
Velocidade: 1000000000
PhysAddress: 00:1a:2b:3c:4d:02
Status Admin: 1
OperStatus: 1
IpAddress: 10.0.1.1
Netmask: 255.255.255.252]
```

<br>

## 📌 Endpoint: GET /GetByListName

Retorna uma lista de dispositivos especificados.

## Exemplo com curl:
```bash
curl -X 'GET' \
  'https://localhost:7130/api/v1/Dispositivo/GetByListName?Name=wireless-controller-01&Name=access-switch-floor5-02' \
  -H 'accept: */*'
```

## Retorno
```bash
[
  {
    "sysName": "access-switch-floor5-02",
    "sysDescr": "HP J9773A 2530-48G-PoE+ Switch, revision WB.16.10.0015, ROM WB.16.03",
    "sysObjectID": "1.3.6.1.4.1.11.2.3.7.11.157",
    "sysUpTime": "123456789",
    "sysContact": "Facilities - facilities@empresa.com.br",
    "sysLocation": "Escritorio SP - 5o Andar - Armario Rede",
    "interfaces": [
      {
        "ifIndex": 1,
        "ifDescr": "Port1",
        "ifType": 6,
        "ifSpeed": 1000000000,
        "ifPhysAddress": "b4:39:d6:22:44:55",
        "ifAdminStatus": 1,
        "ifOperStatus": 1,
        "ipAddress": "192.168.1.5",
        "ipNetmask": "255.255.255.248"
      },
      {
        "ifIndex": 2,
        "ifDescr": "Port2",
        "ifType": 6,
        "ifSpeed": 1000000000,
        "ifPhysAddress": "b4:39:d6:22:44:56",
        "ifAdminStatus": 1,
        "ifOperStatus": 1,
        "ipAddress": null,
        "ipNetmask": null
      },
      {
        "ifIndex": 3,
        "ifDescr": "Port3",
        "ifType": 6,
        "ifSpeed": 1000000000,
        "ifPhysAddress": "b4:39:d6:22:44:57",
        "ifAdminStatus": 1,
        "ifOperStatus": 1,
        "ipAddress": null,
        "ipNetmask": null
      }
    ]
  },
  {
    "sysName": "wireless-controller-01",
    "sysDescr": "Cisco Wireless LAN Controller, AIR-CT5520-K9, Version 8.10.151.0",
    "sysObjectID": "1.3.6.1.4.1.9.1.2170",
    "sysUpTime": "778899001",
    "sysContact": "Wireless Team - wireless@empresa.com.br",
    "sysLocation": "Data Center Sao Paulo - Rack D08",
    "interfaces": [
      {
        "ifIndex": 1,
        "ifDescr": "management",
        "ifType": 6,
        "ifSpeed": 1000000000,
        "ifPhysAddress": "70:db:98:44:77:88",
        "ifAdminStatus": 1,
        "ifOperStatus": 1,
        "ipAddress": "10.0.0.20",
        "ipNetmask": "255.255.255.0"
      },
      {
        "ifIndex": 2,
        "ifDescr": "service-port",
        "ifType": 6,
        "ifSpeed": 1000000000,
        "ifPhysAddress": "70:db:98:44:77:89",
        "ifAdminStatus": 1,
        "ifOperStatus": 1,
        "ipAddress": "192.168.100.1",
        "ipNetmask": "255.255.255.0"
      }
    ]
  }
]
```
<br>

## 📌 Endpoint: GET /GetAllDevices

Retorna uma lista de todos os dispositivos.

## Exemplo com curl:
```bash
curl -X 'GET' \
  'https://localhost:7130/api/v1/Dispositivo/GetAllDevices' \
  -H 'accept: */*'
```
## Retorno
```bash
{
  "192.168.1.1": {
    "sysName": "core-router-sp-01",
    "sysDescr": "Cisco IOS XE Software, Version 16.09.03, Catalyst L3 Switch Software",
    "sysObjectID": "1.3.6.1.4.1.9.1.2298",
    "sysUpTime": "157680000",
    "sysContact": "NOC Team - noc@empresa.com.br",
    "sysLocation": "Data Center Sao Paulo - Rack A12",
    "interfaces": [
      {
        "ifIndex": 1,
        "ifDescr": "GigabitEthernet0/0",
        "ifType": 6,
        "ifSpeed": 1000000000,
        "ifPhysAddress": "00:1a:2b:3c:4d:00",
        "ifAdminStatus": 1,
        "ifOperStatus": 1,
        "ipAddress": "192.168.1.1",
        "ipNetmask": "255.255.255.248"
      },
      {
        "ifIndex": 2,
        "ifDescr": "GigabitEthernet0/1",
        "ifType": 6,
        "ifSpeed": 1000000000,
        "ifPhysAddress": "00:1a:2b:3c:4d:01",
        "ifAdminStatus": 1,
        "ifOperStatus": 1,
        "ipAddress": "10.0.0.1",
        "ipNetmask": "255.255.255.252"
      },
      {
        "ifIndex": 3,
        "ifDescr": "GigabitEthernet0/2",
        "ifType": 6,
        "ifSpeed": 1000000000,
        "ifPhysAddress": "00:1a:2b:3c:4d:02",
        "ifAdminStatus": 1,
        "ifOperStatus": 1,
        "ipAddress": "10.0.1.1",
        "ipNetmask": "255.255.255.252"
      }
    ]
  },
  "192.168.1.2": {
    "sysName": "access-switch-floor3-01",
    "sysDescr": "Juniper Networks, Inc. ex4300-48t Ethernet Switch, kernel JUNOS 20.4R1.12",
    "sysObjectID": "1.3.6.1.4.1.2636.1.1.1.2.43",
    "sysUpTime": "98765432",
    "sysContact": "Infraestrutura - infra@empresa.com.br",
    "sysLocation": "Escritorio SP - 3o Andar - Sala Servidores",
    "interfaces": [
      {
        "ifIndex": 1,
        "ifDescr": "ge-0/0/0",
        "ifType": 6,
        "ifSpeed": 1000000000,
        "ifPhysAddress": "00:5e:8c:a1:b2:00",
        "ifAdminStatus": 1,
        "ifOperStatus": 1,
        "ipAddress": "192.168.1.2",
        "ipNetmask": "255.255.255.248"
      },
      {
        "ifIndex": 2,
        "ifDescr": "ge-0/0/1",
        "ifType": 6,
        "ifSpeed": 1000000000,
        "ifPhysAddress": "00:5e:8c:a1:b2:01",
        "ifAdminStatus": 1,
        "ifOperStatus": 1,
        "ipAddress": null,
        "ipNetmask": null
      },
      {
        "ifIndex": 3,
        "ifDescr": "ge-0/0/2",
        "ifType": 6,
        "ifSpeed": 1000000000,
        "ifPhysAddress": "00:5e:8c:a1:b2:02",
        "ifAdminStatus": 1,
        "ifOperStatus": 2,
        "ipAddress": null,
        "ipNetmask": null
      },
      {
        "ifIndex": 4,
        "ifDescr": "ge-0/0/3",
        "ifType": 6,
        "ifSpeed": 1000000000,
        "ifPhysAddress": "00:5e:8c:a1:b2:03",
        "ifAdminStatus": 1,
        "ifOperStatus": 1,
        "ipAddress": null,
        "ipNetmask": null
      }
    ]
  }...
```
<br>

## 📌 Endpoint: GET /GetAllInterfaceDevice

Retorna uma lista de todas as interfaces de um dispositivo especificado.

## Exemplo com curl:
```bash
curl -X 'GET' \
  'https://localhost:7130/api/v1/Dispositivo/GetAllInterfaceDevice?name=wireless-controller-01' \
  -H 'accept: */*'
```
## Retorno
```bash
[
  {
    "ifIndex": 1,
    "ifDescr": "management",
    "ifType": 6,
    "ifSpeed": 1000000000,
    "ifPhysAddress": "70:db:98:44:77:88",
    "ifAdminStatus": 1,
    "ifOperStatus": 1,
    "ipAddress": "10.0.0.20",
    "ipNetmask": "255.255.255.0"
  },
  {
    "ifIndex": 2,
    "ifDescr": "service-port",
    "ifType": 6,
    "ifSpeed": 1000000000,
    "ifPhysAddress": "70:db:98:44:77:89",
    "ifAdminStatus": 1,
    "ifOperStatus": 1,
    "ipAddress": "192.168.100.1",
    "ipNetmask": "255.255.255.0"
  }
]
```
<br>

## 📌 Endpoint: GET /GetInterfaceDevice

Retorna uma interface especifica de um dispositivo.

## Exemplo com curl:
```bash
curl -X 'GET' \
  'https://localhost:7130/api/v1/Dispositivo/GetInterfaceDevice?name=core-router-sp-01&interfaceIndex=2' \
  -H 'accept: */*'
```
## Retorno
```bash
{
  "ifIndex": 2,
  "ifDescr": "GigabitEthernet0/1",
  "ifType": 6,
  "ifSpeed": 1000000000,
  "ifPhysAddress": "00:1a:2b:3c:4d:01",
  "ifAdminStatus": 1,
  "ifOperStatus": 1,
  "ipAddress": "10.0.0.1",
  "ipNetmask": "255.255.255.252"
}
```
<br>

## 📌 Endpoint: GET /GetAllLocations

Retorna uma lista de todas as localizações.

## Exemplo com curl:
```bash
curl -X 'GET' \
  'https://localhost:7130/api/v1/Dispositivo/GetAllLocations' \
  -H 'accept: */*'
```
## Retorno
```bash
[
  "Data Center Sao Paulo - Rack A12",
  "Escritorio SP - 3o Andar - Sala Servidores",
  "Data Center Sao Paulo - Rack B05",
  "Filial Rio de Janeiro - Sala Telecom",
  "Escritorio SP - 5o Andar - Armario Rede",
  "Data Center Sao Paulo - Rack C01 - DMZ",
  "Data Center Sao Paulo - Rack A13",
  "Data Center Sao Paulo - Rack D08"
]
```
<br>

## 📌 Endpoint: GET /GetDeviceLocation

Retorna a localização de um dispositivo.

## Exemplo com curl:
```bash
curl -X 'GET' \
  'https://localhost:7130/api/v1/Dispositivo/GetDeviceLocation?name=access-switch-floor3-01' \
  -H 'accept: */*'
```
## Retorno
```bash
Escritorio SP - 3o Andar - Sala Servidores
```
