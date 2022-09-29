var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddRepositories()
                .AddBusiness();

new EnvLoader()
              .AddEnvFile("config.env")
              .Load();

var settings = new EnvBinder().Bind<AppSettings>();
builder.Services.AddSingleton(settings);

builder.Services.AddDbContext<NotesContext>(options =>
{
    options.UseNpgsql(settings.ConnectionString);
});

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

app.UseAuthorization();

app.MapControllers();

app.Run();
