using BakeryApi.Application.Services.Users;
using Microsoft.EntityFrameworkCore;
using BakeryApi.Infrastructure.Data;

var builder = WebApplication.CreateBuilder(args);



//add dbcontext
// builder.Services.AddDbContext<BakeryDbContext>(options =>
//     options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

    // services.AddDbContext<BakeryDbContext>(options =>
    // options.UseNpgsql(Configuration.GetConnectionString("DefaultConnection")));
    builder.Services.AddDbContext<BakeryDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));



// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<IUserService, UserService>();
// Enable console logging
builder.Logging.ClearProviders();
builder.Logging.AddConsole();






var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<BakeryDbContext>();
    Console.WriteLine($"[DEBUG] Connected to DB: {db.Database.GetConnectionString()}");
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers(); // This maps your [ApiController]s
app.Run();
app.UseMiddleware<ExceptionMiddleware>();


