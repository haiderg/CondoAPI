using CondoAPI.Core.Interfaces;
using CondoAPI.Core.Models;
using CondoAPI.Core.Validators.Models;

using CondoAPI.Infrastructure.Data;
using CondoAPI.Infrastructure.Repositories;
using CondoAPI.Infrastructure.Services;

using FluentValidation;
using Microsoft.EntityFrameworkCore;


namespace CondoAPI.API.Extensions
{
    public static class ServiceExtensions
    {
        public static IServiceCollection AddDbContext(this IServiceCollection services, IConfiguration configuration)
        {
            var databaseSettings = configuration.GetSection("DatabaseSettings").Get<DatabaseSettings>();
            
            services.AddDbContext<CondoDbContext>(options =>
            {
                var connectionString = databaseSettings?.ConnectionString ?? configuration.GetConnectionString("DefaultConnection");
                options.UseSqlServer(connectionString);
            });
            
            services.AddScoped<IAgentRepository, AgentRepository>();
            return services;
        }

        public static IServiceCollection AddServices(this IServiceCollection services)
        {
            services.AddScoped<IAgentService, AgentService>();
            return services;
        }

        public static IServiceCollection AddValidators(this IServiceCollection services)
        {
            services.AddScoped<IValidator<Agent>, AgentValidator>();
            return services;
        }

        public static IServiceCollection AddDatabaseConfiguration(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<DatabaseSettings>(configuration.GetSection("DatabaseSettings"));
            
            return services;
        }
    }
}