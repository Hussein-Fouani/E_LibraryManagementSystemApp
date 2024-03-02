using E_LibraryApi.Mapper;
using E_LibraryApi.Repository;
using E_LibraryApi.Repository.IRepository;
using E_LibraryManagementSystem.Db;
using Microsoft.EntityFrameworkCore;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var logPathFile = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Logging","log.txt"); 
Log.Logger = new LoggerConfiguration()
    .WriteTo.File(logPathFile, rollingInterval: RollingInterval.Day).CreateLogger();
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddAutoMapper(typeof(MapperConfig));
/*builder.Services.AddScoped<IBookRepository,BookRepository>();
builder.Services.AddScoped<IStudentRepository,StudentRepository>();
builder.Services.AddScoped<ISignInRepository,SignInRepository>();*/
//builder.Services.AddScoped<ISignUpRepository,SignUpRepository>();
builder.Services.AddDbContext<E_LibDb>(option=>option.UseSqlServer(builder.Configuration.GetConnectionString("Default")));
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
