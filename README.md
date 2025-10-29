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
curl -X 'POST' \
  'http://localhost:5000/api/v1/Dispositivo' \
  -H 'accept: */*' \
  -H 'Content-Type: application/json' \
  -d '[
  "192.168.1.1",
  "10.0.0.1"
]'

## 📌 Endpoint: GET /GetByName

Retorna o dispositivo especificado

## Exemplo com curl:
curl -X 'GET' \
  'http://localhost:5000/api/v1/Dispositivo?Name=core-router-sp-01' \
  -H 'accept: */*'

