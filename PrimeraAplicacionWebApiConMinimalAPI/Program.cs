var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
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

//Crear una lista para almacenar objetos de tipo Cliet (clientes)
var clients = new List<Client>();

//Configurar una ruta GET para obtener todos los clientes
app.MapGet("/clients/{id}", (int id) =>
{
    //Busca un cliente en la lista que tenga el ID especificado
    var client = clients.FirstOrDefault(c => c.Id == id);
    return client; //devuelva el cliente encontrado (o null si no encuentra)
});

//Configurar una ruta POST para agregar un nuevo cliente a la lista
app.MapPost("/clients", (Client client) =>
{
    clients.Add(client); //agrega el nuevo cliente a la lista
    return Results.Ok(); //devuelve una respuesta HTTP 200 Ok
});

app.MapPut("/clients/{id}", (int id, Client client) =>
{
    var existingClient = clients.FirstOrDefault(c => c.Id == id);
    if (existingClient != null)
    {
        existingClient.Name = client.Name;
        existingClient.LastName = client.LastName;
        return Results.Ok();
    }
    else
    {
        return Results.NotFound();
    }
});

app.MapDelete("/clients", ( int id, Client client) =>
{
    var existingClient = clients.FirstOrDefault(c => c.Id == id);
    if (existingClient != null)
    {
        clients.Remove(existingClient);
        return Results.Ok();
    }
    else
    {
        return Results.NotFound();
    }
});


app.Run();

//definicion de un cliente que represena la estructura de un cliente
internal class Client
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string LastName { get; set; }
}
