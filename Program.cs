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

app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();

app.UseCors(options =>
{
    options.AllowAnyOrigin();
    options.AllowAnyMethod();
    options.AllowAnyHeader();
});

app.UseAuthorization();

app.MapControllers();

app.Run();
