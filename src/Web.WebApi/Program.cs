
// Asp 5 vs 6
// https://docs.microsoft.com/en-us/aspnet/core/migration/50-to-60-samples?view=aspnetcore-6.0


using Core.Infrastructure.Configurations;
using Core.Infrastructure.Database;
using Core.Ioc;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using OpenIddict.Abstractions;
using OpenIddict.Validation;
using OpenIddict.Validation.AspNetCore;
using System.Diagnostics;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Web.WebApi.Mutations;
using Web.WebApi.Queries;
using static OpenIddict.Validation.AspNetCore.OpenIddictValidationAspNetCoreHandlers;

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

// OpenIdConnect
builder.Services.AddOpenIddict()
    .AddValidation(options =>
    {
        // Test if the authorization handler can be extracted from the HttpRequest
        options.AddEventHandler<OpenIddictValidationEvents.ProcessAuthenticationContext>(builder =>
        {
            builder.UseInlineHandler(context =>
            {
                var request = context.Transaction.GetHttpRequest();
                Debug.WriteLine("Authorization header: " + request.Headers.Authorization.ToString());

                return default;
            });

            builder.SetOrder(ExtractAccessTokenFromAuthorizationHeader.Descriptor.Order + 1);
        });
        // End Test

        //options.SetConfiguration(new OpenIdConnectConfiguration
        //{
        //    Issuer = "https://localhost:44365/",
        //    SigningKeys = { new X509SecurityKey(AuthenticationExtensionMethods.TokenSigningCertificate()) }
        //});

        //options.AddEncryptionCertificate(AuthenticationExtensionMethods.TokenEncryptionCertificate());

        // uses OpenID Connect discovery
        options.SetIssuer("https://localhost:8081/");
        options.AddAudiences("api-client");
        // credentials used when communicating with the remote introspection endpoint.
        options.UseIntrospection()
            .SetClientId("api-client")
            .SetClientSecret("499D56FA-B47B-5199-BA61-B298D431C318");

        // Register the System.Net.Http integration.
        options.UseSystemNetHttp();

        // Register the ASP.NET Core host.
        options.UseAspNetCore();

        // Register the Owin host.
        //options.UseOwin().UseActiveAuthentication();
    });
builder.Services.AddAuthentication(options => {
    options.DefaultScheme = OpenIddictValidationAspNetCoreDefaults.AuthenticationScheme;
    options.DefaultAuthenticateScheme = OpenIddictValidationAspNetCoreDefaults.AuthenticationScheme;
});



// Authentication
// - JWT
//var jwtConfig = builder.Configuration.GetSection("JwtSetting").Get<JwtSetting>();
// builder.Services.AddSingleton(jwtConfig);

// JWT
//builder.Services.AddAuthentication(opt =>
//{
//    opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
//    opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
//})
//.AddJwtBearer(opt =>
//    {
//        opt.Authority = "https://localhost:8081/";
//        opt.Audience = "api-client";
//        opt.TokenValidationParameters.ValidTypes = new[] { "at+jwt" };
//        //opt.RequireHttpsMetadata = false;
//        //opt.SaveToken = true; // save to HttpContext
//        opt.TokenValidationParameters = new TokenValidationParameters
//        {
//            ValidateIssuerSigningKey = true,

//            NameClaimType = ClaimTypes.NameIdentifier,
//            ValidateIssuer = false,
//            //ValidIssuer = jwtTokenConfig.Issuer,
//            ValidateAudience = false,
//            ValidAudience = "api-client", //jwtTokenConfig.Audience,
//            ValidateLifetime = true,
//            ClockSkew = TimeSpan.Zero // remove 5 minute window after the token expired,
//        };
//    });


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


//-----------APP--------------
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
