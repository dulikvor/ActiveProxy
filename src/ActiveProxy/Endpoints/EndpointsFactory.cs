using Microsoft.AspNetCore.Routing.Patterns;
using Route = ActiveProxy.Options.Route;

namespace ActiveProxy.Endpoints
{
    public class EndpointsFactory : IEndpointsFactory
    {
        private RequestDelegate? _requestPipeline;

        public void SetProxyPipeline(RequestDelegate requestPipeline)
        {
            _requestPipeline = requestPipeline;
        }

        public Endpoint CreateEndpoint(Route route)
        {
            var pathPattern = string.IsNullOrEmpty(route.Match.Path) ? "/{**catchall}" : route.Match.Path;

            var endpointRouteBuilder = new RouteEndpointBuilder(
                _requestPipeline,
                RoutePatternFactory.Parse(pathPattern),
                0);

            return endpointRouteBuilder.Build();
        }
    }
}
