using Application.PipelineBehaviours;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Application
{
    public static class ServiceCollectionExtensions
    {
        public static void AddApplicationServices(this IServiceCollection services)
        {
            services
                .AddSingleton<Instrumentation>()
                .AddAutoMapper(Assembly.GetExecutingAssembly())
                .AddMediatR(cfg =>
                {
                    cfg.RegisterServicesFromAssemblies(Assembly.GetExecutingAssembly());
                })
                .AddValidatorsFromAssembly(Assembly.GetExecutingAssembly())
                .AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationPipelineBehaviour<,>))
                .AddTransient(typeof(IPipelineBehavior<,>), typeof(CachePipelineBehaviour<,>));
        }
    }
}
