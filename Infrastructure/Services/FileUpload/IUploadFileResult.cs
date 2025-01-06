namespace vsa_journey.Infrastructure.Services.FileUpload;


public interface IUploadFileResult
{
    bool Success { get; set; }
    string UploadUrl { get; set; }
}

public interface IUploadFilesResult
{
   List<IUploadFileResult> UploadedFiles { get; }
}