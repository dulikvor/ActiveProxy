namespace ActiveProxy.Options
{
    public class TelemetryOptions
    {
        public const string Key = "Telemetry";

        public Guid? WorkspaceId { get; set; }
        public Guid? ClientId { get; set; }
        public string ClientSecret { get; set; }
        public Guid? TenantId { get; set; }
        public string AuthorityBaseUri { get; set; }
        public string Audience { get; set; }
        public string DceUri { get; set; }
        public string DcrImmutableId { get; set; }
        public string TableName { get; set; }
    }
}
