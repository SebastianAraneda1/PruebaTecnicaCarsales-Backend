# Prueba Técnica Carsales - Backend

Backend desarrollado en .NET 8 para actuar como una API intermedia entre el frontend y la API pública de Rick and Morty.

El objetivo principal del backend es aplicar una arquitectura Backend for Frontend (BFF), exponiendo endpoints propios para consultar episodios y personajes de forma ordenada, paginada y con filtros.

---

## Descripción general

Este backend consume información desde la API pública de Rick and Morty y adapta la respuesta para ser utilizada por una aplicación cliente.

La aplicación permite:

- Consultar episodios.
- Consultar personajes.
- Aplicar filtros de búsqueda.
- Obtener resultados paginados.
- Manejar errores de forma centralizada.
- Configurar URLs externas mediante archivos de configuración.

El backend no utiliza base de datos. Su función principal es consumir una API externa, transformar la información y devolver una respuesta más limpia.

---

## Tecnologías utilizadas

- .NET 8
- ASP.NET Core Web API
- C#
- HttpClient
- Swagger / OpenAPI
- Inyección de dependencias
- Middleware personalizado
- Arquitectura por capas
- Archivos de configuración appsettings.json

---

## Estructura del proyecto

backend/
|
├── PruebaTecnicaCarsales.sln
|
└── src/
    ├── PruebaTecnicaCarsales.Api/
    ├── PruebaTecnicaCarsales.Application/
    ├── PruebaTecnicaCarsales.Domain/
    └── PruebaTecnicaCarsales.Infrastructure/

---

## Arquitectura por capas

El backend está separado en cuatro proyectos principales.

### PruebaTecnicaCarsales.Api

Es la capa de entrada de la aplicación.

Contiene:

- Controladores.
- Middleware de errores.
- Configuración de servicios.
- Swagger.
- CORS.
- Archivo Program.cs.

Responsabilidades:

- Recibir peticiones HTTP.
- Validar parámetros básicos.
- Llamar a los servicios correspondientes.
- Devolver respuestas HTTP.

---

### PruebaTecnicaCarsales.Application

Contiene contratos, DTOs y modelos comunes.

Contiene:

- Interfaces.
- DTOs.
- Modelos de respuesta paginada.

Responsabilidades:

- Definir qué operaciones necesita la aplicación.
- Definir qué datos se exponen como respuesta.
- Evitar que la API dependa directamente de la infraestructura.

---

### PruebaTecnicaCarsales.Domain

Contiene las entidades internas del dominio.

Contiene:

- Episode.
- Character.

Responsabilidades:

- Representar los modelos internos de la aplicación.
- Separar el modelo interno del formato exacto de la API externa.

---

### PruebaTecnicaCarsales.Infrastructure

Contiene la lógica técnica de consumo externo.

Contiene:

- Servicios HTTP.
- Modelos de respuesta externa.
- Configuración de HttpClient.
- Registro de dependencias.

Responsabilidades:

- Consumir la API pública de Rick and Morty.
- Mapear respuestas externas.
- Devolver DTOs adaptados a la aplicación.

---

## Flujo de una petición

Ejemplo al consultar episodios:

Cliente
  ↓
EpisodesController
  ↓
IRickAndMortyEpisodeService
  ↓
RickAndMortyEpisodeService
  ↓
Rick and Morty API
  ↓
Mapeo a DTO
  ↓
Respuesta paginada

---

## Configuración

La URL base de la API externa se configura en:

src/PruebaTecnicaCarsales.Api/appsettings.json

Ejemplo:

{
  "RickAndMortyApi": {
    "BaseUrl": "https://rickandmortyapi.com/api/"
  },
  "AllowedHosts": "*"
}

En desarrollo también se utiliza:

src/PruebaTecnicaCarsales.Api/appsettings.Development.json

Ejemplo:

{
  "RickAndMortyApi": {
    "BaseUrl": "https://rickandmortyapi.com/api/"
  },
  "Cors": {
    "AllowedOrigins": [
      "http://localhost:4200"
    ]
  },
  "Logging": {
    "LogLevel": {
      "Default": "Information",
      "Microsoft.AspNetCore": "Warning"
    }
  }
}

Esto permite evitar URLs hardcodeadas dentro del código.

---

## Endpoints disponibles

### Episodios

Obtiene episodios paginados.

GET /api/episodes?page=1

Filtros disponibles:

GET /api/episodes?page=1&name=pilot
GET /api/episodes?page=1&episode=S01E01

Parámetros:

| Parámetro | Tipo | Requerido | Descripción |
|---|---|---:|---|
| page | int | No | Número de página. Por defecto es 1. |
| name | string | No | Filtro por nombre del episodio. |
| episode | string | No | Filtro por código del episodio. Ejemplo: S01E01. |

---

### Personajes

Obtiene personajes paginados.

GET /api/characters?page=1

Filtros disponibles:

GET /api/characters?page=1&name=rick
GET /api/characters?page=1&status=alive
GET /api/characters?page=1&species=human
GET /api/characters?page=1&gender=male

Parámetros:

| Parámetro | Tipo | Requerido | Descripción |
|---|---|---:|---|
| page | int | No | Número de página. Por defecto es 1. |
| name | string | No | Filtro por nombre del personaje. |
| status | string | No | Filtro por estado. Ejemplo: alive, dead, unknown. |
| species | string | No | Filtro por especie. |
| gender | string | No | Filtro por género. Ejemplo: male, female, genderless, unknown. |

---

## Formato de respuesta paginada

Los endpoints devuelven una respuesta con este formato:

{
  "count": 51,
  "pages": 3,
  "currentPage": 1,
  "hasNextPage": true,
  "hasPreviousPage": false,
  "results": []
}

Descripción:

| Campo | Descripción |
|---|---|
| count | Cantidad total de registros. |
| pages | Cantidad total de páginas. |
| currentPage | Página actual consultada. |
| hasNextPage | Indica si existe una página siguiente. |
| hasPreviousPage | Indica si existe una página anterior. |
| results | Resultados de la página actual. |

---

## Ejemplo de respuesta de episodios

{
  "count": 51,
  "pages": 3,
  "currentPage": 1,
  "hasNextPage": true,
  "hasPreviousPage": false,
  "results": [
    {
      "id": 1,
      "name": "Pilot",
      "airDate": "December 2, 2013",
      "episode": "S01E01"
    }
  ]
}

---

## Ejemplo de respuesta de personajes

{
  "count": 826,
  "pages": 42,
  "currentPage": 1,
  "hasNextPage": true,
  "hasPreviousPage": false,
  "results": [
    {
      "id": 1,
      "name": "Rick Sanchez",
      "status": "Alive",
      "species": "Human",
      "type": "",
      "gender": "Male",
      "image": "https://rickandmortyapi.com/api/character/avatar/1.jpeg"
    }
  ]
}

---

## Manejo de errores

El backend implementa un middleware global:

Middleware/ExceptionHandlingMiddleware.cs

Este middleware permite capturar errores de forma centralizada.

Ejemplo de error al consultar un servicio externo:

{
  "message": "No fue posible obtener información desde el servicio externo."
}

Ejemplo de error inesperado:

{
  "message": "Ocurrió un error inesperado."
}

---

## Ejecución local

Desde la carpeta backend, restaurar dependencias:

dotnet restore

Compilar la solución:

dotnet build

Ejecutar la API:

dotnet run --project src/PruebaTecnicaCarsales.Api

---

## Swagger

En ambiente de desarrollo, Swagger queda disponible para probar los endpoints.

La URL suele ser similar a:

https://localhost:7000/swagger

El puerto puede variar según la configuración local.

---

## Pruebas manuales recomendadas

Probar episodios:

GET /api/episodes?page=1
GET /api/episodes?page=1&name=pilot
GET /api/episodes?page=1&episode=S01E01

Probar personajes:

GET /api/characters?page=1
GET /api/characters?page=1&name=rick
GET /api/characters?page=1&status=alive
GET /api/characters?page=1&species=human
GET /api/characters?page=1&gender=male

Probar validación:

GET /api/episodes?page=0
GET /api/characters?page=0

Respuesta esperada:

{
  "message": "La página debe ser mayor o igual a 1."
}

---

## Buenas prácticas aplicadas

### Separación de responsabilidades

Cada capa tiene una responsabilidad clara:

Api             → recibe peticiones HTTP
Application     → define contratos y DTOs
Domain          → define entidades internas
Infrastructure  → consume servicios externos

---

### Uso de DTOs

El backend no devuelve directamente todo el modelo externo de Rick and Morty.

Se utilizan DTOs para exponer solo la información necesaria:

- EpisodeDto.
- CharacterDto.

---

### Uso de interfaces

Los controladores dependen de interfaces, no de implementaciones concretas.

Ejemplo:

private readonly IRickAndMortyEpisodeService _episodeService;

Esto reduce el acoplamiento entre capas.

---

### Inyección de dependencias

Los servicios se registran mediante el contenedor de dependencias de .NET.

Esto evita crear instancias manualmente y permite mantener el código más limpio.

---

### Configuración externa

La URL de la API externa se define en appsettings.json.

Esto permite cambiar valores de configuración sin modificar el código fuente.

---

### Manejo centralizado de errores

El middleware de errores evita repetir bloques try/catch en todos los controladores.

---

## Relación con SOLID

### Single Responsibility Principle

Cada clase tiene una responsabilidad específica.

Ejemplos:

- EpisodesController: expone endpoints de episodios.
- CharactersController: expone endpoints de personajes.
- RickAndMortyEpisodeService: consume episodios desde la API externa.
- RickAndMortyCharacterService: consume personajes desde la API externa.
- ExceptionHandlingMiddleware: maneja errores globales.

---

### Open/Closed Principle

La estructura permite agregar nuevas funcionalidades sin modificar gran parte del código existente.

Por ejemplo, se podría agregar una feature de ubicaciones creando nuevas clases y servicios, sin romper episodios ni personajes.

---

### Dependency Inversion Principle

Los controladores dependen de abstracciones definidas en Application.

Ejemplo:

IRickAndMortyEpisodeService

No dependen directamente de RickAndMortyEpisodeService.

---

### Interface Segregation Principle

Las interfaces son específicas para cada funcionalidad.

Ejemplos:

- IRickAndMortyEpisodeService.
- IRickAndMortyCharacterService.

---

## Comandos útiles

Restaurar dependencias:

dotnet restore

Compilar:

dotnet build

Ejecutar:

dotnet run --project src/PruebaTecnicaCarsales.Api

Limpiar compilados:

dotnet clean

---

## Resumen

Este backend implementa una API en .NET 8 con arquitectura por capas, consumo de servicios externos, paginación, filtros, DTOs, configuración externa, inyección de dependencias y manejo global de errores.

El proyecto está preparado para ser probado localmente mediante Swagger y para ser consumido por una aplicación cliente.
