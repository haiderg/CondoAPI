using CondoAPI.Core.Interfaces;
using CondoAPI.Core.Models;
using CondoAPI.Core.Validators.Models;
using CondoAPI.Core.DTOs.Requests;
using CondoAPI.Infrastructure.Data;
using CondoAPI.Infrastructure.Repositories;
using CondoAPI.Infrastructure.Services;
using CondoAPI.Core.Validators.DTOs;
using FluentValidation;

namespace CondoAPI.API.Extensions
{
    public static class ServiceExtensions
    {
        public static IServiceCollection AddRepositories(this IServiceCollection services)
        {
            services.AddScoped<IDbConnectionFactory, SqlConnectionFactory>();
            services.AddScoped<IResidentRepository, ResidentRepository>();
            
            return services;
        }

        public static IServiceCollection AddServices(this IServiceCollection services)
        {
            services.AddScoped<IAvailableResortsService, AvailableResortsService>();
            
            return services;
        }

        public static IServiceCollection AddValidators(this IServiceCollection services)
        {
            services.AddScoped<IValidator<Resident>, ResidentValidator>();
            services.AddScoped<IValidator<AvailableResortsCountriesRequest>, AvailableResortsCountriesRequestValidator>();
            
            return services;
        }

        public static IServiceCollection AddDatabaseConfiguration(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<DatabaseSettings>(configuration.GetSection("DatabaseSettings"));
            
            return services;
        }
    }
}