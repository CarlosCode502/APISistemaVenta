using SistemaVenta.IOC; //Agregar la referencia para poder habilitar el servicio

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//Para poder acceder al servico es necesario usar los métodos de extension (this IServiceCollection) min 26.44 
builder.Services.InyectarDependencias(builder.Configuration); //para poder obtener la cadena desde appsettings 27.01

//Agregando los Cors (tener problemas al momento de enviar y recibir información) min 29.10 parte 6
//Ya que la url para la app angular va a ser distinta a la del api es (http://localhost:5137/api/Usuario/Lista)
builder.Services.AddCors(options =>
{
    //Opciones para agregar una nueva politica min 31.06 parte 6
    options.AddPolicy("NuevaPolitica", app =>
    {
        //Configurar los permisos
        app.AllowAnyOrigin() //Cualquier origen
        .AllowAnyHeader() //Cualquier cabecera
        .AllowAnyMethod(); //Permitir cualquier método (Get, Post, Put y Delete) min 31.40 parte 6
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

//Activar toda la configuracion de Cors (del min 29.10 parte 6) min 31.53 parte 6
//Con esto se activan los cors (agregar el nombre de la politica) para cualquier url
app.UseCors("NuevaPolitica");

app.UseAuthorization();

app.MapControllers();

app.Run();
