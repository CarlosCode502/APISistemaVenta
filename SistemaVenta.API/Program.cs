using SistemaVenta.IOC; //Agregar la referencia para poder habilitar el servicio

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//Para poder acceder al servico es necesario usar los métodos de extension (this IServiceCollection) min 26.44 
builder.Services.InyectarDependencias(builder.Configuration); //para poder obtener la cadena desde appsettings 27.01

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.Run();
