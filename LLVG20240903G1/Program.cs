using System.Text.RegularExpressions;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

//lista de marcas
var marcas = new List<Marcas>();

//obtener todas las marcas
app.MapGet("/marcas", () =>
{
    return marcas; //dv la lista de las marcas
});

//obtener una marca en especifico por su ID
app.MapGet("/marcas/{id}", (int id) =>
{
    var marca = marcas.FirstOrDefault(m => m.Id == id);
    return marca;
});
//ruta post para la creación de una nueva marca
app.MapPost("/marcas", (Marcas marca) =>
{
    marcas.Add(marca);//agrega la nueva marca 
    return Results.Ok();//y dv una respuesta HTTP 200 ok
});

// Configurar una ruta PUT para actualizar una marca existente
app.MapPut("/marcas/{id}", (int id, Marcas marca) =>
{
    var existingMarca = marcas.FirstOrDefault(m => m.Id == id);
    if (existingMarca != null)
    {
        existingMarca.Name = marca.Name;
        return Results.Ok();
    }
    else
    {
        return Results.NotFound();
    }
});

// Configurar una ruta DELETE para eliminar una marca por su ID
app.MapDelete("/marcas/{id}", (int id) =>
{
    var existingMarca = marcas.FirstOrDefault(m => m.Id == id);
    if (existingMarca != null)
    {
        marcas.Remove(existingMarca);
        return Results.Ok();
    }
    else
    {
        return Results.NotFound();
    }
});


//ejecuta la aplicación 
app.Run();

internal class Marcas
{
    public int Id { get; set; }
    public string Name { get; set; }
}
