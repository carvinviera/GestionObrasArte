using GestionObrasArte.BlazorApp.Components;
using GestionObrasArte.BlazorApp.Services;

var builder = WebApplication.CreateBuilder(args);

// IMPORTANTE: Ajusta esta URL a la que usa tu API
builder.Services.AddScoped(sp => new HttpClient
{
    BaseAddress = new Uri("http://localhost:5103/api/")
});

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents(); // Asegura interactividad Server

builder.Services.AddScoped<ArtistasService>();

var app = builder.Build();

app.UseStaticFiles();
app.UseAntiforgery();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();