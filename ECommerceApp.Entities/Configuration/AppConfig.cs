namespace ECommerceApp.Entities.Configuration;

public class AppConfig
{
    public Storageconfiguration StorageConfiguration { get; set; } = null!;

    public class Storageconfiguration
    {
        public string PublicUrl { get; set; } = null!;
        public string Path { get; set; } = null!;
    }

}