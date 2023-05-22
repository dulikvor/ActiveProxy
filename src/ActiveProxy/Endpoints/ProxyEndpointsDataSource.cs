using ActiveProxy.Options;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.Primitives;
using Route = ActiveProxy.Options.Route;

namespace ActiveProxy.Endpoints
{
    public class ProxyEndpointsDataSource : EndpointDataSource, IDisposable
    {
        private readonly IEndpointsFactory _endpointFactory;
        private readonly ProxyOptions _proxyOptions;
        private readonly CancellationTokenSource _cts;
        private readonly IChangeToken _consumerChangeToken;
        private bool _isDisposed = false;
        private static object _syncRoot = new object();
        private List<Endpoint>? _endpoints;

        public ProxyEndpointsDataSource(
            IEndpointsFactory endpointFactory,
            IOptions<ProxyOptions> proxyOptionsAccessor)
        {
            _endpointFactory = endpointFactory;
            _proxyOptions = proxyOptionsAccessor.Value;
            
            var cts = new CancellationTokenSource();
            _consumerChangeToken = new CancellationChangeToken(cts.Token);
        }

        public override IChangeToken GetChangeToken()
        {
            return _consumerChangeToken;
        }

        public override IReadOnlyList<Endpoint> Endpoints
        {
            get
            {
                if (_endpoints is null)
                {
                    lock (_syncRoot)
                    {
                        if (_endpoints is null)
                        {
                            CreateEndpoints();
                        }
                    }
                }
                return _endpoints!;
            }
        }

        public void Dispose()
        {
            if (_isDisposed)
            {
                return;
            }
            _cts.Dispose();
            _isDisposed = true;
        }

        private void CreateEndpoints()
        {
            _endpoints = new List<Endpoint>();

            foreach (var route in _proxyOptions.Routes ?? Array.Empty<Route>())
            {
                _endpoints.Add(_endpointFactory.CreateEndpoint(route));
            }
        }
    }
}
