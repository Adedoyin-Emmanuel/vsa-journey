using FluentResults;

namespace vsa_journey.Infrastructure.Services.FileUpload;

public interface IUploadFileResult
{
    bool Success { get; set; }
    string UploadUrl { get; set; }
}

public interface IUploadFilesResult
{
    bool Success { get; set; }
    string UploadUrl { get; set; }
}

public interface IFileUploadService
{
    public Result<IUploadFileResult> UploadFile(IFormFile file);

    public Result<IUploadFilesResult> UploadFiles(IFormFileCollection files);
}