using BookingNet.Api.Errors;
using BookingNet.Application.DataFilters;
using BookingNet.Domain.Aggregates.UserAggregate;
using BookingNet.Domain.Interfaces.Filtering;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;

using System.Reflection;

namespace BookingNet.Api
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApi(this IServiceCollection services, ConfigurationManager configuration)
        {
            services.AddSingleton<ProblemDetailsFactory, CustomProblemFactory>();
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            services.AddSwaggerGen(options =>
            {
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                options.IncludeXmlComments(xmlPath);
            });

            services.AddApiVersioning(options => {
                options.AssumeDefaultVersionWhenUnspecified = true;
                options.DefaultApiVersion = new ApiVersion(1, 0);
                options.ReportApiVersions = true;
            });

            //services.AddCors(options =>
            //{
            //    options.AddPolicy(name: "Front",
            //        builder =>
            //        {
            //            builder.WithOrigins("https://localhost:4200").AllowAnyHeader().AllowAnyMethod().WithExposedHeaders("x-custom-header");
            //        });
            //});


            return services;
        }
    }
}
