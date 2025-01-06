namespace vsa_journey.Infrastructure.Services.FileUpload;

public class UploadFileResult : IUploadFileResult
{
    public bool Success { get; set; }
    public string UploadUrl { get; set; }
}

public class UploadFilesResult : IUploadFilesResult
{
    public List<IUploadFileResult> UploadedFiles { get; set; }
}