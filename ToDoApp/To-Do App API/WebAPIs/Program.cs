
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.OpenApi.Models;
using System.Text;
using Repository;
using Repository.Interfaces;
using Services.Interfaces;
using Services;

//Creating and Building the Web Application
var builder = WebApplication.CreateBuilder(args);//creating a WebApplicationBuilder instance
builder.Configuration.Sources.Clear();
builder.Configuration.AddJsonFile("appsettings.json");
// Add services to the DI container.
builder.Services.AddCors(option =>
{
    option.AddPolicy("AllowAllOrigins",
        builder =>
        {
            builder.AllowAnyOrigin()
            .AllowAnyHeader()
            .AllowAnyMethod();
        });
});

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
// Add JWT authentication
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(Options =>
    {
        Options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidIssuer = builder.Configuration["Jwt:Issuer"],
            ValidAudience = builder.Configuration["Jwt:Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]!))
        };
    });
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


builder.Services.AddDbContext<TodoContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("MyDatabaseConnection")));
builder.Services.AddTransient<IUserService,UserService>();
builder.Services.AddTransient<IUserRepository, UserRepository>();
builder.Services.AddTransient<ITodoService, TodoTaskService>();
builder.Services.AddTransient<ITodoTaskRepository, TodoTaskRepository>();
builder.Services.AddAutoMapper(typeof(MappingProfiles).Assembly);

builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "EMS API", Version = "v1" });
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Description = "JWT Authorization header using the Bearer scheme. Example: \"Bearer {token}\"",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.Http,
        Scheme = "bearer"
    });
    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            new string[] { }
        }
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
//if (app.Environment.IsDevelopment()) //Checks if the application is running in the development environment
//{
    app.UseSwagger();//Adds the Swagger JSON endpoint to the middleware pipeline. This endpoint exposes the API documentation in JSON format.
    app.UseSwaggerUI();//Adds Swagger UI 
//}



app.UseCors("AllowAllOrigins");

app.UseHttpsRedirection();//Redirects HTTP requests to HTTPS

app.UseAuthentication();

app.UseAuthorization();//It's used to restrict access to certain endpoints based on user rules or permissions.

app.MapControllers();//This method maps controller routes to the incoming HTTP requests.

app.Run();
