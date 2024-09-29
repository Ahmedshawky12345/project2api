using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using project2api.Data;
using project2api.Repositary.interfaces;
using project2api.Repositary;
using project2api.Model;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

builder.Services.AddControllersWithViews()
    .AddNewtonsoftJson(options =>
    options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
);


// add repostory
builder.Services.AddScoped<IProduct, ProductRepsitory>();
builder.Services.AddScoped<ICategory, CategoryRepsiory>();
builder.Services.AddScoped<IOrder, OrderRepsitory>();
builder.Services.AddScoped<IOrderItem, OrderitemRepsitory>();
builder.Services.AddScoped<ICartitem, CartitemRepsitory>();

builder.Services.AddIdentity<AppUser, IdentityRole>().AddEntityFrameworkStores<AppDbContext>();


// allow use my api
builder.Services.AddCors(options => options.AddPolicy("allow", policy =>
{
    policy.AllowAnyOrigin().
    AllowAnyMethod().AllowAnyHeader();
}));

builder.Services.AddDbContext<AppDbContext>(x => x.UseSqlServer(
    builder.Configuration.GetConnectionString("conn")));

// change the authrzation to suppurt jwt not cookis

builder.Services.AddAuthentication(option =>
{
    option.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;


    // to dont navgation into the controller (auth/login)
    option.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    option.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;

}
    // i went cheach the token is valid (take key and toked to do valdation)


    ).AddJwtBearer(
    options =>
    {
        options.SaveToken=true;
        // ensure that user https or not
        options.RequireHttpsMetadata = true;
        options.TokenValidationParameters = new TokenValidationParameters()
        {
            ValidateIssuer = true,
            ValidIssuer = builder.Configuration["JWT:valid_issur"],
            ValidateAudience = true,
            ValidAudience = builder.Configuration["JWT:valdid_audiance"],
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8
        .GetBytes(builder.Configuration["JWT:secret"].PadRight(48)))

        };

    }
    );





// to turn on the suagger ------------- important---------
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo { Title = "My API", Version = "v1" });

    // Define the security scheme for JWT Bearer tokens
    options.AddSecurityDefinition("Bearer", new Microsoft.OpenApi.Models.OpenApiSecurityScheme
    {
        Description = "JWT Authorization header using the Bearer scheme. Example: \"Authorization: Bearer {token}\"",
        Name = "Authorization",
        In = Microsoft.OpenApi.Models.ParameterLocation.Header,
        Type = Microsoft.OpenApi.Models.SecuritySchemeType.Http,
        Scheme = "bearer"
    });

    // Add a security requirement to include the Bearer token in requests
    options.AddSecurityRequirement(new Microsoft.OpenApi.Models.OpenApiSecurityRequirement
    {
        {
            new Microsoft.OpenApi.Models.OpenApiSecurityScheme
            {
                Reference = new Microsoft.OpenApi.Models.OpenApiReference
                {
                    Type = Microsoft.OpenApi.Models.ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            Array.Empty<string>()
        }
    });
});

//              -------------------------- end turn on the sugger





// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

var app = builder.Build();






// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseStaticFiles();
app.UseHttpsRedirection();
app.UseCors("allow"); // usercorse this middleware allow to ather cosumaser to use my api ( must to do to use any api)

app.UseAuthorization();

app.MapControllers();

app.Run();
