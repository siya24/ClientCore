using ClientCore.API;

var builder = WebApplication.CreateBuilder(args);

var connString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddSqlServer<ClientCoreDBContext>(connString);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
// or more typically, using IServiceCollection
builder.Services.AddAutoMapper(cfg =>
{
    cfg.CreateMap<CreateClientDTO, Client>();
    cfg.CreateMap<Client, GetClientDTO>();

    cfg.CreateMap<CreateContactDTO, Contact>();
    cfg.CreateMap<Contact, GetContactDTO>();
});

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<IClientService, ClientService>();
builder.Services.AddScoped<IContactService, ContactService>();

var app = builder.Build();


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

await app.MigrateDbAsyncy();

app.Run();
