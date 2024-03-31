namespace OnlineWallet.WebApi.Extensions
{
    public static class ApiDependencyInjection
    {
        public static IServiceCollection AddUILayer(this IServiceCollection services)
        {
            services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();

            return services;
        }
    }
}
