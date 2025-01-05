using FluentResults;

namespace vsa_journey.Infrastructure.Services.FileUpload;

public class FileUploadService : IFileUploadService
{
    private readonly ILogger<FileUploadService> _logger;
    private readonly List<string> _allowedExtensions = [".png", ".jpeg", ".gif", ".jpg", ".webp"];

    public FileUploadService(ILogger<FileUploadService> logger)
    {
        _logger = logger;
    }
    public Result<IUploadFileResult> UploadFile(IFormFile file)
    {
        if (file == null || file.Length == 0)
        {
            return Result.Fail($"File is null or empty");
        }

        var fileExtension = Path.GetExtension(file.FileName);
        var fileExtensionCheck = CheckExtension(fileExtension);

        if (fileExtensionCheck.IsFailed)
        {
            var failedMessage = fileExtensionCheck?.Reasons?.FirstOrDefault()?.Message ?? "Invalid file extension";
            return Result.Fail(failedMessage);
        }


        try
        {
            var uploadPath = Path.Combine
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
        

    }

    public Result<IUploadFilesResult> UploadFiles(IFormFileCollection files)
    {
        throw new NotImplementedException();
    }

    private Result CheckExtension(string extension)
    {
        if (!_allowedExtensions.Contains(extension))
        {
            return Result.Fail($"Invalid file extension, {_allowedExtensions} are the valid file extensions");
        }
        
        return Result.Ok();
    }
}
