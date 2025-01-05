namespace vsa_journey.Infrastructure.Services.FileUpload;

public class FileUploadService : IFileUploadService
{
    private readonly ILogger<FileUploadService> _logger;
    private readonly List<string> _allowedExtensions = [".png", ".jpeg", ".gif", ".jpg", ".webp"];

    public FileUploadService(ILogger<FileUploadService> logger)
    {
        _logger = logger;
    }
    public (bool, string) UploadFile(IFormFile file)
    {
        throw new NotImplementedException();
    }

    public (bool, string[]) UploadFiles(IFormFileCollection files)
    {
        throw new NotImplementedException();
    }
}