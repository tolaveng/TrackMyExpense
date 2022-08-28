
// Asp 5 vs 6
// https://docs.microsoft.com/en-us/aspnet/core/migration/50-to-60-samples?view=aspnetcore-6.0


using Core.Infrastructure.Configurations;
using Core.Ioc;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using OpenIddict.Validation.AspNetCore;
using System.Text;
using Web.WebApi.Mutations;
using Web.WebApi.Queries;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(option =>
{
    option.SwaggerDoc("v1", new OpenApiInfo { Title = "Client API", Version = "v1" });
    option.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Description = "Please enter a valid token",
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        BearerFormat = "JWT",
        Scheme = "Bearer"
    });
    option.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type=ReferenceType.SecurityScheme,
                    Id="Bearer"
                }
            },
            new string[]{}
        }
    });
});

// Ioc
builder.Services.ConfigureServices(builder.Configuration, builder.Environment);

// Turn off, JWT claim type mapping to allow well-known claims(sub, idp)
//JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Clear();

var oidcSetting = builder.Configuration.GetSection("OidcSetting").Get<OidcSetting>();
//builder.Services.AddSingleton(oidcSetting);
builder.Services.Configure<OidcSetting>(builder.Configuration.GetSection("OidcSetting"));

// OpenIdConnect
builder.Services.AddOpenIddict()
    .AddValidation(options =>
    {
        if (string.IsNullOrEmpty(oidcSetting.SecurityKey)) throw new InvalidOperationException("Security Key is not configued");
        var secretKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(oidcSetting.SecurityKey));
        options.Configure(conf => {
            conf.TokenValidationParameters.ValidateIssuer = true;
            conf.TokenValidationParameters.IssuerSigningKey = secretKey;
            conf.TokenValidationParameters.ValidateIssuerSigningKey = true;
        });

        // uses OpenID Connect discovery
        options.SetIssuer(oidcSetting.Issuer);
        options.AddAudiences(oidcSetting.ClientId);
        // credentials used when communicating with the remote introspection endpoint.
        options.UseIntrospection()
            .SetClientId(oidcSetting.ClientId)
            .SetClientSecret(oidcSetting.ClientSecret);

        // Register the System.Net.Http integration.
        options.UseSystemNetHttp();

        // Register the ASP.NET Core host.
        options.UseAspNetCore();
    });
builder.Services.AddAuthentication(options => {
    options.DefaultScheme = OpenIddictValidationAspNetCoreDefaults.AuthenticationScheme;
    options.DefaultAuthenticateScheme = OpenIddictValidationAspNetCoreDefaults.AuthenticationScheme;
});


builder.Services.AddCors(options => options.AddDefaultPolicy(policy =>
{
    policy.AllowAnyOrigin();
    policy.AllowAnyMethod();
    policy.AllowAnyHeader();
}));

builder.Services.AddAuthorization();

builder.Services.AddHttpContextAccessor();

// GraphQL
builder.Services
    .AddGraphQLServer()
    .AddAuthorization()
    .AddQueryType(x => x.Name("Query"))
        .AddTypeExtension<IconQuery>()
        .AddTypeExtension<ExpenseQuery>()
        .AddTypeExtension<UserQuery>()
    .AddMutationType(x => x.Name("Mutation"))
        .AddTypeExtension<ExpenseMutation>()
    ;

if (builder.Environment.IsDevelopment())
{
    builder.Services.AddLogging(options => {
        options.AddDebug();
        options.SetMinimumLevel(LogLevel.Trace);
    });
}


//----------- APP --------------
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseCors();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.MapGraphQL();//.RequireAuthorization(); // require Global authorization

app.Run();
