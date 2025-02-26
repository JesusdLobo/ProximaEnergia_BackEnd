Acuerdos Microservice (Backend)
Este proyecto implementa un microservicio en .NET 8 para la gestión de acuerdos comerciales y sus tarifas asociadas, utilizando Entity Framework Core y SQL Server como base de datos.

1. Arquitectura y Estructura de Proyectos
La solución está dividida en cuatro proyectos principales, siguiendo un enfoque por capas:

Acuerdos.Domain

Contiene las entidades de dominio (p. ej., AcuerdoComercial, TarifaAcuerdo, TarifaConsumo).
No conoce nada de EF Core; solo define los objetos de negocio.
Acuerdos.Infrastructure

Contiene la implementación de acceso a datos con EF Core (clase AppDbContext), migraciones y repositorios opcionales.
Hace referencia a Acuerdos.Domain para usar las entidades.
Acuerdos.Application

Contiene la lógica de negocio o servicios (por ejemplo, AcuerdosService), junto con las interfaces que describen la funcionalidad (p. ej., IAcuerdosService).
Hace referencia a Acuerdos.Infrastructure si el servicio necesita utilizar directamente AppDbContext, o bien recibe repositorios inyectados.
Acuerdos.Api

Es el proyecto Web API en .NET 8 que expone los endpoints REST.
Controladores (p. ej., AcuerdosController) que orquestan las llamadas a los servicios de la capa Application.
Contiene la configuración de Program.cs, el registro de dependencias (DI), appsettings.json con la cadena de conexión, etc.

2. Tecnologías
.NET 8 (C#)
Entity Framework Core (SQL Server)
SQL Server (base de datos relacional)
Swagger (opcional) para documentación de los endpoints


4. Configuración de la Base de Datos
En Acuerdos.Api/appsettings.json se define la cadena de conexión:

json
Copiar
Editar
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=localhost;Database=AcuerdosDb;User Id=sa;Password=123456;TrustServerCertificate=True;"
  }
}
Asegúrate de ajustar el servidor, usuario y contraseña según tu entorno de SQL Server.

5. Ejecución
Compila la solución en Visual Studio.
Establece Acuerdos.Api como proyecto de inicio (Startup Project).
Presiona F5 o Ctrl+F5 para iniciar la aplicación.
El microservicio se levantará en https://localhost:xxxx.
Si tienes Swagger habilitado, podrás acceder a la ruta /swagger para probar los endpoints.
