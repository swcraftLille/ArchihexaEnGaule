using Dotnetoramix.Domain.Druide;
using Dotnetoramix.Domain.Recette;
using Dotnetoramix.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapPost("/potion-magique", () =>
{
    var stock = new GardeManger();
    var cuiseur = new Thermomix();
    var druide = new Dotnetoramix.Domain.Druide.Dotnetoramix(stock, cuiseur);

    return Results.Ok(druide.Preparer(Recette.PotionMagique));
})
.WithName("CreerPotionMagique")
.WithOpenApi();

app.Run();
