using Route = ActiveProxy.Options.Route;

namespace ActiveProxy.Endpoints
{
    public interface IEndpointsFactory
    {
        public void SetProxyPipeline(RequestDelegate requestPipeline);
        public Endpoint CreateEndpoint(Route route);
    }
}
