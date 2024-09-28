using ExamenPrueba2.DataHub;
using ExamenPrueba2.Interfaces;
using ExamenPrueba2.Services;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();

//Servicios
builder.Services.AddSignalR();
builder.Services.AddScoped<IRegisterRandomDataService, RegisterRandomDataService>();
builder.Services.AddScoped<SignalRClient>();
var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();


app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

//Map de mi SignalR
app.MapHub<RandomDataHub>("/randomDataHub");


app.Run();
