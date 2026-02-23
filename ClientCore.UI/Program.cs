using ClientCore.Business.Services;
using ClientCore.Models;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

var connString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddSqlServer<ClientCoreDBContext>(connString);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddAutoMapper(cfg =>
{
    cfg.CreateMap<CreateClientDTO, Client>();
    cfg.CreateMap<Client, GetClientDTO>();

    cfg.CreateMap<CreateContactDTO, Contact>();
    cfg.CreateMap<Contact, GetContactDTO>();
});

builder.Services.AddScoped<IClientService, ClientService>();
builder.Services.AddScoped<IContactService, ContactService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseRouting();

app.UseAuthorization();

app.MapStaticAssets();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Clients}/{action=Index}/{id?}")
    .WithStaticAssets();


app.Run();
