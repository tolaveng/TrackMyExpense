
// Asp 5 vs 6
// https://docs.microsoft.com/en-us/aspnet/core/migration/50-to-60-samples?view=aspnetcore-6.0


using Core.Infrastructure.Configurations;
using Core.Ioc;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using System.Text;
using Web.WebApi.Mutations;
using Web.WebApi.Queries;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
// builder.Services.AddSwaggerGen();

// Ioc
builder.Services.ConfigureServices(builder.Configuration, builder.Environment);


// Authentication
// - JWT
var jwtConfig = builder.Configuration.GetSection("JwtSetting").Get<JwtSetting>();
builder.Services.AddSingleton(jwtConfig);

builder.Services.AddAuthentication(opt =>
{
    opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(opt =>
    {
        opt.RequireHttpsMetadata = false;
        opt.SaveToken = true; // save to HttpContext
        opt.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtConfig.Secret)),

            NameClaimType = ClaimTypes.NameIdentifier,
            ValidateIssuer = false,
            //ValidIssuer = jwtTokenConfig.Issuer,
            ValidateAudience = false,
            //ValidAudience = jwtTokenConfig.Audience,
            ValidateLifetime = true,
            ClockSkew = TimeSpan.Zero // remove 5 minute window after the token expired,
        };
    });

builder.Services.AddAuthorization();

builder.Services.AddHttpContextAccessor();

// GraphQL
builder.Services
    .AddGraphQLServer()
    .AddAuthorization()
    .AddQueryType(x => x.Name("Query"))
        .AddTypeExtension<IconQuery>()
        .AddTypeExtension<ExpenseQuery>()
    .AddMutationType(x => x.Name("Mutation"))
        .AddTypeExtension<ExpenseMutation>()
    ;

var app = builder.Build();

// Configure the HTTP request pipeline.
//if (app.Environment.IsDevelopment())
//{
//    app.UseSwagger();
//    app.UseSwaggerUI();
//}

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();

// app.MapControllers();

app.MapGraphQL().RequireAuthorization(); // require Global authorization

app.Run();
