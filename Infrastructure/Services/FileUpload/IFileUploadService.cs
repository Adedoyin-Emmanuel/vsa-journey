using FluentResults;

namespace vsa_journey.Infrastructure.Services.FileUpload;


public interface IFileUploadService
{
    public Task<Result<IUploadFileResult>> UploadFileAsync(IFormFile file);

    public Task<Result<IUploadFilesResult>> UploadFilesAsync(IFormFileCollection files);
    
    public Result DeleteFiles(IEnumerable<string> filePaths);
} 