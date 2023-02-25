using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace MITT.Services.Helpers.JwtHelper
{
    public static class JwtExtensions
    {
        public static IServiceCollection AddJwt(this IServiceCollection services)
        {
            var jwtOptions = configureJWTOptions(services);
            if (jwtOptions is not null)
            {
                using var serviceProvider = services.BuildServiceProvider();
                var config = serviceProvider.GetService<IConfiguration>();

                services.AddAuthentication(options =>
                {
                    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
                })
                    .AddJwtBearer(options =>
                    {
                        options.SaveToken = true;
                        options.TokenValidationParameters = new TokenValidationParameters
                        {
                            ClockSkew = TimeSpan.Zero,
                            ValidateIssuer = true,
                            ValidateAudience = true,
                            ValidateLifetime = true,
                            ValidateIssuerSigningKey = true,
                            ValidIssuer = jwtOptions.Issuer,
                            ValidAudience = jwtOptions.Audience,
                            IssuerSigningKey =
                            new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtOptions.SecretKey))
                        };
                        options.Events = new JwtBearerEvents
                        {
                            OnAuthenticationFailed = context =>
                            {
                                if (context.Exception.GetType() == typeof(SecurityTokenExpiredException))
                                    context.Response.Headers.Add("Token-Expired", "true");
                                return Task.CompletedTask;
                            }
                        };
                    });

                services.AddTransient<JwtTokenBuilder>();
            }
            return services;
        }

        private static JWTOptions configureJWTOptions(IServiceCollection services)
        {
            JWTOptions jwtOptions;
            using (var serviceProvider = services.BuildServiceProvider())
            {
                var configuration = serviceProvider.GetService<IConfiguration>();
                var jwt = configuration.GetSection("Jwt");
                if (jwt.Exists())
                {
                    services.Configure<JWTOptions>(configuration.GetSection("Jwt"));
                    jwtOptions = configuration.GetOptions<JWTOptions>("Jwt");
                    return jwtOptions;
                }
                else return null;
            }
        }
    }

    public static class Extensions
    {
        public static TModel GetOptions<TModel>(this IConfiguration configuration, string section) where TModel : new()
        {
            var model = new TModel();
            configuration.GetSection(section).Bind(model);
            return model;
        }
    }

    public class JWTOptions
    {
        public string SecretKey { get; set; }
        public string Issuer { get; set; }
        public string Audience { get; set; }
        public int RefreshTokenTimeout { get; set; }
        public int TokenTimeout { get; set; }
    }

    public class JwtOnlinePayment
    {
        public string SecretKey { get; set; }
        public string Issuer { get; set; }
        public string Audience { get; set; }
        public int RefreshTokenTimeout { get; set; }
        public int TokenTimeout { get; set; }
    }
}