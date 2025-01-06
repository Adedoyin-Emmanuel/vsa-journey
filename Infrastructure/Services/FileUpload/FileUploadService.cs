using FluentResults;

namespace vsa_journey.Infrastructure.Services.FileUpload;

public class FileUploadService : IFileUploadService
{
    private readonly ILogger<FileUploadService> _logger;
    private readonly List<string> _allowedExtensions = [".png", ".jpeg", ".gif", ".jpg", ".webp"];
    private readonly int _maxFileSize = 5 * 1024 * 1024;
    private readonly string _uploadRootPath = "/Application/Data/Uploads";
    

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

        long fileSize = file.Length;

        if (fileSize > _maxFileSize)
        {
            return Result.Fail($"File size cannot be more than {_maxFileSize} bytes");
        }

        try
        {
            string fileName = $"{Guid.NewGuid().ToString()}_{fileExtension}";
            string fileDestination = Path.Combine(_uploadRootPath, fileName);

            using (FileStream fileStream = new FileStream(fileDestination, FileMode.Create))
            {
                file.CopyTo(fileStream);
            }
            
            _logger.LogInformation($"File {fileName} uploaded to {fileDestination}");

            var uploadedFileResult = new UploadFileResult
            {
                Success = true,
                UploadUrl = fileDestination
            };

            return Result.Ok<IUploadFileResult>(uploadedFileResult);
        }
        catch (Exception e)
        {
           _logger.LogError(e, "An error occured while upload file");
            return Result.Fail<IUploadFileResult>("An error occured while uploading file");
        }
    }

    public Result<IUploadFilesResult> UploadFiles(IFormFileCollection files)
    {
        if (files == null || files.Count == 0)
        {
            return Result.Fail<IUploadFilesResult>("File is null or empty");
        }

        var uploadResults = new List<IUploadFileResult>();

        foreach (var file in files)
        {
            var result = UploadFile(file);
            if (result.IsSuccess)
            {
                uploadResults.Add(result.ValueOrDefault);
            }
            else
            {
                return Result.Fail(result.Errors);
            }
        }

        return Result.Ok<IUploadFilesResult>(new UploadFilesResult { UploadedFiles =  uploadResults});
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
