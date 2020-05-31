namespace Ajupov.Infrastructure.All.Orm.Settings
{
    public class OrmSettings
    {
        public bool IsTestMode { get; set; }

        public string MainConnectionString { get; set; }

        public string ReadonlyConnectionString { get; set; }
    }
}
