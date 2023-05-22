using ActiveProxy.Endpoints;
using ActiveProxy.Options;
using Microsoft.Extensions.Options;
using OpenTelemetry;
using OpenTelemetry.Exporter.AzureMonitorLogs;
using OpenTelemetry.Exporter.AzureMonitorLogs.Monitor;
using OpenTelemetry.Trace;

namespace ActiveProxy
{
    public static class ActiveProxyExtensions
    {
        public static IEndpointRouteBuilder AddProxy(this IEndpointRouteBuilder endpointRouteBuilder)
        {
            var activeProxyApplicationBuilder = new ActiveProxyApplicationBuilder(endpointRouteBuilder.CreateApplicationBuilder());
            var application = activeProxyApplicationBuilder.Build();

            var endpointFactory = endpointRouteBuilder.ServiceProvider.GetRequiredService<IEndpointsFactory>();
            endpointFactory.SetProxyPipeline(application);

            var dataSource = endpointRouteBuilder.ServiceProvider.GetRequiredService<ProxyEndpointsDataSource>();
            endpointRouteBuilder.DataSources.Add(dataSource);

            return endpointRouteBuilder;
        }

        public static IApplicationBuilder WithTelemetry(this IApplicationBuilder endpointRouteBuilder)
        {
            var telemetryOptions = endpointRouteBuilder.ApplicationServices.GetRequiredService<IOptions<TelemetryOptions>>().Value;

            using var tracerProvider = Sdk.CreateTracerProviderBuilder()
            .AddAzureMonitorLogsExporter(o =>
            {
                o.WorkspaceId = telemetryOptions.WorkspaceId.Value;
                o.ClientId = telemetryOptions.ClientId.Value;
                o.ClientSecret = telemetryOptions.ClientSecret;
                o.TenantId = telemetryOptions.TenantId.Value;
                o.AuthorityBaseUri = telemetryOptions.AuthorityBaseUri;
                o.DceUri = telemetryOptions.DceUri;
                o.DcrImmutableId = telemetryOptions.DcrImmutableId;
                o.TableName = telemetryOptions.TableName;
            })
            .AddSource(ActivityScope.Source)
            .Build();
            
            return endpointRouteBuilder;
        }
    }
}