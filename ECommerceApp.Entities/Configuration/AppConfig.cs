namespace ECommerceApp.Entities.Configuration;

public class AppConfig
{
    public Storageconfiguration StorageConfiguration { get; set; } = null!;

    public Jwt Jwt { get; set; } = null!;
    
}

public class Storageconfiguration
{
    public string PublicUrl { get; set; } = null!;
    public string Path { get; set; } = null!;
}


public class Jwt
{
    public string SecretKey { get; set; } = default!;

    public string Audiencia { get; set; } = default!;

    public string Emisor { get; set; } = default!;
}