using ECommerceApp.Entities.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace ECommerceApp.Services;

public class FileUploader : IFileUploader
{
    private readonly AppConfig _appConfig;
    private readonly ILogger<FileUploader> _logger;

    public FileUploader(IOptions<AppConfig> options, ILogger<FileUploader> logger)
    {
        _appConfig = options.Value;
        _logger = logger;
    }

    public async Task<string> UploadFileAsync(string? base64Image, string? fileName)
    {
        if (string.IsNullOrWhiteSpace(base64Image) || string.IsNullOrWhiteSpace(fileName))
            return string.Empty;

        try
        {
            var bytes = Convert.FromBase64String(base64Image);

            // C:\CursoBlazor\ImagenesTienda\xiaomi.jpg (Windows) Backslash (\)
            // ~/CursoBlazor/ImagenesTienda/xiaomi.jpg (Linux) Forward slash (/)

            var path = Path.Combine(_appConfig.StorageConfiguration.Path, fileName);

            await using var fileStream = new FileStream(path, FileMode.Create);
            await fileStream.WriteAsync(bytes, 0, bytes.Length);

            // http://localhost:5500/imagenestienda/xiaomi.jpg

            return $"{_appConfig.StorageConfiguration.PublicUrl}{fileName}";

        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error al subir el archivo {fileName} {Message}", fileName, ex.Message);

            return string.Empty;
        }
    }
}