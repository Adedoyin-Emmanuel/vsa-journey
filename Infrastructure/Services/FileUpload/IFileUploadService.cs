namespace vsa_journey.Infrastructure.Services.FileUpload;

public interface IFileUploadService
{
    public (bool, string) UploadFile(IFormFile file);

    public (bool, string[]) UploadFiles(IFormFileCollection files);
}