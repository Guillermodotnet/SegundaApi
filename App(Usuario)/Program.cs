using App_Usuario_.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// --------------------
// Configuracion de servicios
// --------------------

// Obtiene la cadena de conexion desde appsettings.json
var connectionString = builder.Configuration.GetConnectionString("connetion");

// Registra el contexto de base de datos en el contenedor de dependencias
// Esto permite usar Entity Framework Core con SQL Server
builder.Services.AddDbContext<DbUsuarioContext>(options =>
    options.UseSqlServer(connectionString));

// Agrega soporte para controladores de API
builder.Services.AddControllers();

// Agrega herramientas de documentacion y prueba de API (Swagger)
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// --------------------
// Construccion de la aplicacion
// --------------------
var app = builder.Build();

// --------------------
// Configuracion del pipeline HTTP
// --------------------
if (app.Environment.IsDevelopment())
{
    // Swagger solo se habilita en entorno de desarrollo
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Redirige automaticamente HTTP a HTTPS
app.UseHttpsRedirection();

// Habilita el sistema de autorizacion (por si se agrega autenticacion mas adelante)
app.UseAuthorization();

// Mapea los controladores (con sus rutas definidas)
app.MapControllers();

// Inicia la aplicacion
app.Run();

