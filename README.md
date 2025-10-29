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
  'http://localhost:5000/api/v1/Dispositivo?Name=core-router-sp-01' \
  -H 'accept: */*'
```
<br>

## 📌 Endpoint: GET /GetByListName

Retorna uma lista de dispositivos especificados.

## Exemplo com curl:
```bash
curl -X 'GET' \
  'https://localhost:7130/api/v1/Dispositivo/GetByListName?Name=core-router-sp-01&Name=access-switch-floor3-01&Name=dist-switch-dc-01' \
  -H 'accept: */*'
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
<br>

## 📌 Endpoint: GET /GetAllInterfaceDevice

Retorna uma lista de todas as interfaces de um dispositivo especificado.

## Exemplo com curl:
```bash
curl -X 'GET' \
  'https://localhost:7130/api/v1/Dispositivo/GetAllInterfaceDevice?name=dist-switch-dc-01' \
  -H 'accept: */*'
```
<br>

## 📌 Endpoint: GET /GetInterfaceDevice

Retorna uma interface especifica de um dispositivo.

## Exemplo com curl:
```bash
curl -X 'GET' \
  'https://localhost:7130/api/v1/Dispositivo/GetInterfaceDevice?name=dist-switch-dc-01&interfaceIndex=2' \
  -H 'accept: */*'
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
<br>

## 📌 Endpoint: GET /GetDeviceLocation

Retorna a localização de um dispositivo.

## Exemplo com curl:
```bash
curl -X 'GET' \
  'https://localhost:7130/api/v1/Dispositivo/GetDeviceLocation?name=dist-switch-dc-01' \
  -H 'accept: */*'
```
