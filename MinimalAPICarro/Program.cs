using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MinimalAPICarro.Config;
using MinimalAPICarro.Modelo;

var builder = WebApplication.CreateBuilder(args);


///////Swagger e Metados/////////////
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var stringConexao = "Data Source=(localdb)\\MSSQLLocalDB; Initial Catalog = Minimal_APICarro; Integrated Security = True; Connect Timeout = 30; Encrypt = False; TrustServerCertificate = False; ApplicationIntent = ReadWrite; MultiSubnetFailover = False";

builder.Services.AddDbContext<Contexto>
    (options => options.UseSqlServer(stringConexao));

var app = builder.Build();
app.UseSwagger();


app.MapPost("AdicionaCarro", async (Carros carro, Contexto contexto) =>
{
    contexto.Carro.Add(carro);
    await contexto.SaveChangesAsync();
});


app.MapPost("ExcluirCarro/{id}", async (int id, Contexto contexto) =>
{
    var carro = await contexto.Carro.FirstOrDefaultAsync(p => p.Id == id);
    if (carro != null)
    {
        contexto.Carro.Remove(carro);
        await contexto.SaveChangesAsync();
    }
});

app.MapGet("ListarCarros", async (Contexto contexto) =>
{
    return await contexto.Carro.ToListAsync();
});

app.MapGet("ObterCarro/{id}", async (int id, Contexto contexto) =>
{
    return await contexto.Carro.FirstOrDefaultAsync(p => p.Id == id);
});

app.UseSwaggerUI();
///////Swagger e Metados/////////////

app.Run();

