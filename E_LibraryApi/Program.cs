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
    .WriteTo.File("./Logging/log.txt", rollingInterval: RollingInterval.Day).CreateLogger();
builder.Services.AddControllers();
builder.Services.AddLogging();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<IUserRepository,UserRepository>();
builder.Services.AddAutoMapper(typeof(MapperConfig));

builder.Services.AddScoped<IBookRepository,BookRepository>();
builder.Services.AddScoped<ISignUpRepository,SignUpRepository>();
builder.Services.AddScoped<ISignInRepository,SignInRepository>();
builder.Services.AddScoped<IBorrowBook,BorrowBook>();
/*builder.Services.AddScoped<IPasswordHasher, PasswordHasher>();
*/builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddDbContext<E_LibDb>(option=>option.UseSqlServer(builder.Configuration.GetConnectionString("Default")));
/*builder.Services.AddAuthentication(x =>
{
    x.DefaultScheme=JwtBearerDefaults.AuthenticationScheme;
    x.DefaultAuthenticateScheme=JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(x =>
{
    x.RequireHttpsMetadata=false;
    x.SaveToken=true;
    x.TokenValidationParameters=new TokenValidationParameters
    {
        ValidateIssuerSigningKey=true,
        IssuerSigningKey=new SymmetricSecurityKey(Encoding.ASCII.GetBytes(builder.Configuration.GetSection("ApiSettings:Secret").Value)),
        ValidateIssuer=false,
        ValidateAudience=false
    };
});*/

/*builder.Services.AddSwaggerGen(options =>
{
    options.AddSecurityDefinition("Bearer",new OpenApiSecurityScheme
    {
        Description = "JWT Authorization header using the Bearer scheme.\r\r\n\n"
        + "Enter 'Bearer' [space] and then your token in the text input below.\r\n\n"
        + "Example: \"Bearer 12345abcdef\"\r\n\n",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Scheme= "Bearer"
    });
    options.AddSecurityRequirement(new OpenApiSecurityRequirement()
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                },
                Scheme = "oauth2",
                Name = "Bearer",
                In = ParameterLocation.Header,
            },
            new List<string>()
        }
    });
});*/
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
//app.UseAuthentication();
app.UseAuthorization();


app.MapControllers();

app.Run();
