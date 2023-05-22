using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Features;

namespace ActiveProxy
{
    public class ActiveProxyApplicationBuilder : IApplicationBuilder
    {
        private readonly IApplicationBuilder _applicationBuilder;
        public ActiveProxyApplicationBuilder(IApplicationBuilder applicationBuilder)
        {
            _applicationBuilder = applicationBuilder;
        }

        public IServiceProvider ApplicationServices
        {
            get => _applicationBuilder.ApplicationServices;
            set => _applicationBuilder.ApplicationServices = value;
        }
        public IFeatureCollection ServerFeatures => _applicationBuilder.ServerFeatures;
        public IDictionary<string, object?> Properties => _applicationBuilder.Properties;
        public RequestDelegate Build() => _applicationBuilder.Build();
        public IApplicationBuilder New() => _applicationBuilder.New();
        public IApplicationBuilder Use(Func<RequestDelegate, RequestDelegate> middleware) => _applicationBuilder.Use(middleware);
    }
}
