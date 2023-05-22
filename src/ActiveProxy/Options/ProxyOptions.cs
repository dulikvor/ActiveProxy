namespace ActiveProxy.Options
{
    public class RouteDestination
    {
        public string Name { get; set; }
        public string Destination { get; set; }
    }

    public class RouteMatch
    {
        public string Path { get; set; }
        public string[] Methods { get; set; }
    }

    public class Route
    {
        public string Destinition { get; set; }
        public RouteMatch Match { get; set; }
    }

    public class ProxyOptions
    {
        public Route[] Routes { get; set; }

        public RouteDestination[] Destinations { get; set; }
    }
}
