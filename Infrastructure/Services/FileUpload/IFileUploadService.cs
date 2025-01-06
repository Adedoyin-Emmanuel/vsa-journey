using FluentResults;

namespace vsa_journey.Infrastructure.Services.FileUpload;


public interface IFileUploadService
{
    public Result<IUploadFileResult> UploadFile(IFormFile file);

    public Result<IUploadFilesResult> UploadFiles(IFormFileCollection files);
}