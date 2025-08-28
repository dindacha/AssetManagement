
using Microsoft.EntityFrameworkCore;
using AssetManagement.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddRazorPages(options =>
{
    options.Conventions.AddAreaPageRoute("Assets", "/Assets/Index",   "/assets");
    options.Conventions.AddAreaPageRoute("Assets", "/Assets/Create",  "/assets/create");
    options.Conventions.AddAreaPageRoute("Assets", "/Assets/Edit",    "/assets/edit/{id:int}");
    options.Conventions.AddAreaPageRoute("Assets", "/Assets/Details", "/assets/details/{id:int}");
    options.Conventions.AddAreaPageRoute("Assets", "/Assets/Delete",  "/assets/delete/{id:int}");
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseRouting();

app.UseAuthorization();

app.MapStaticAssets();
app.MapRazorPages()
   .WithStaticAssets();

app.Run();
