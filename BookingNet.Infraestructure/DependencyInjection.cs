using BookingNet.Domain.Interfaces.Security;
using BookingNet.Domain.Interfaces;
using BookingNet.Infraestructure.Authentication;
using BookingNet.Infraestructure.Repositories;
using BookingNet.Infraestructure.Tools.Security;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using BookingNet.Infraestructure.DataContext;
using Microsoft.EntityFrameworkCore;
using BookingNet.Infraestructure.Mappings.AutoMapper;
using System.Reflection;
using Microsoft.Extensions.Options;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using BookingNet.Application.Common.ServerCache;
using BookingNet.Infraestructure.Common;

namespace BookingNet.Infraestructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfraestructure(this IServiceCollection services, ConfigurationManager configuration)
        {
            //services.Configure<JwtSettings>(configuration.GetSection(JwtSettings.SectionName));
            services.AddSingleton<IJwtTokenGenerator, JwtTokenGenerator>();
            services.AddScoped<IHashGenerator, HashGenerator>();
            services.AddScoped<IHashVerifier, HashVerifier>();
            services.AddScoped<IPasswordGenerator, PasswordGenerator>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();

            services.AddAutoMapper(typeof(MappingProfiles).GetTypeInfo().Assembly);
            services.AddScoped(typeof(IServerCacheServiceStorage<>), typeof(ServerCacheServiceStorage<>));

            services.AddDbContext<BookingNetDbContext>(options =>
            {
                options.UseSqlServer(configuration.GetConnectionString("BookingNetDbContext"), 
                    x => x.MigrationsAssembly(typeof(BookingNetDbContext).Assembly.FullName));
            });

            var jwtSettings = new JwtSettings();
            configuration.Bind(JwtSettings.SectionName, jwtSettings);
            services.AddSingleton(Options.Create(jwtSettings));

            services.AddAuthentication(defaultScheme: JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
                options.TokenValidationParameters = new TokenValidationParameters
                {

                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = jwtSettings.Issuer,
                    ValidAudience = jwtSettings.Audience,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings.Secret))
                }
            );

            return services;
        }
    }
}