using APSEstate.CONCRETE.Interface;
using APSEstate.CONCRETE.Repository;

namespace APSEstate.API.Extension
{
    public static class ServiceExtension
    {
        public static void ConfigureDIServices(this IServiceCollection services)
        {
            services.AddScoped<IAuthRepository, AuthRepository>();
            services.AddScoped<IBuilderRepository, BuilderRepository>();
        }
    }
}
