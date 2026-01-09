# BackEnd Intuit – API de Clientes

API REST desarrollada en **.NET 8** para la gestión de Clientes (ABM), como parte del **Challenge Técnico Intuit / Yappa**.

La aplicación implementa una arquitectura en capas, validaciones de dominio, manejo global de errores, logging y documentación mediante Swagger.

## Tecnologías utilizadas

- **.NET 8**
- **ASP.NET Core Web API**
- **Entity Framework Core**
- **MySQL**
- **Serilog** (logging en consola y archivo)
- **Swagger / OpenAPI**
- **xUnit + Moq** (tests unitarios)
- **SonarQube** (calidad de código y coverage)

## Funcionalidades

- Alta, baja y modificación de clientes
- Búsqueda de clientes por nombre o apellido
- Validaciones de dominio (CUIT, email, fechas, campos obligatorios)
- Manejo centralizado de errores mediante middleware
- Registro de errores en archivo `.log`
- Documentación automática con Swagger
- Cobertura de tests superior al 80%


## Endpoints principales

| Método | Endpoint                  | Descripción                         |
|------|---------------------------|-------------------------------------|
| GET  | `/api/clientes`           | Obtiene todos los clientes           |
| GET  | `/api/clientes/{id}`      | Obtiene un cliente por ID            |
| GET  | `/api/clientes/search`    | Busca clientes por nombre/apellido   |
| POST | `/api/clientes`           | Crea un nuevo cliente                |
| PUT  | `/api/clientes/{id}`      | Actualiza un cliente                 |
| DELETE | `/api/clientes/{id}`    | Elimina un cliente                   |


## Tests

- Tests unitarios implementados sobre la capa **Application**
- Uso de **Moq** para simular repositorios
- Coverage medido con **coverlet**
- Integración con **SonarQube**


## Calidad de código

- **SonarQube**
  - Security: A
  - Reliability: A
  - Maintainability: A
  - Coverage: +80%


## Manejo de errores y logging

- Middleware global de excepciones (`ExceptionHandlingMiddleware`)
- Respuestas HTTP consistentes ante errores
- Logs generados con **Serilog**:
  - Consola
  - Archivo rotativo diario: `Logs/app-YYYYMMDD.log`


## Documentación Swagger

Swagger se encuentra disponible al ejecutar la aplicación.
Incluye:
- Descripción de endpoints
- Modelos de request/response
- Comentarios XML de controladores

## Antes de compilar
- Configurar la cadena de conexión en `appsettings.json`
