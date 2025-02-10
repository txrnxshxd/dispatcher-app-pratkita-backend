namespace DispatcherApp.Data
{
    public sealed class MongoSettings
    {
        public string ConnectionString { get; init; } = null!;
        public string Database { get; init; } = null!;
    }
}
