using ActiveProxy.Endpoints;
using ActiveProxy.Options;

namespace ActiveProxy
{
    public class Startup
    {
        private readonly IConfiguration _configurationService;
        public Startup(IConfiguration configurationService)
        {
            _configurationService = configurationService;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddOptions<TelemetryOptions>()
                .Bind(_configurationService.GetSection(TelemetryOptions.Key))
                .ValidateDataAnnotations();
            services.AddOptions<ProxyOptions>()
                .Bind(_configurationService)
                .ValidateDataAnnotations();

            services.AddSingleton<IEndpointsFactory, EndpointsFactory>();
            services.AddSingleton<ProxyEndpointsDataSource>();
        }

        public void Configure(IApplicationBuilder app)
        {
            app.UseRouting();
            app.WithTelemetry();
            app.UseEndpoints(endpoints =>
            {
                endpoints.AddProxy();
            });
        }
    }
}
