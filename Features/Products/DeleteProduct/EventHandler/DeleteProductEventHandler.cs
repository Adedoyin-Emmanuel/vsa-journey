using MediatR;
using vsa_journey.Features.Products.DeleteProduct.Event;
using vsa_journey.Infrastructure.Services.FileUpload;

namespace vsa_journey.Features.Products.DeleteProduct.EventHandler;

public class DeleteProductEventHandler : INotificationHandler<DeleteProductEvent>
{
    private readonly IFileUploadService _fileUploadService;
    private readonly ILogger<DeleteProductEventHandler> _logger;

    public DeleteProductEventHandler(IFileUploadService fileUploadService, ILogger<DeleteProductEventHandler> logger)
    {
        _fileUploadService = fileUploadService;
        _logger = logger;
    }
    
    
    public Task Handle(DeleteProductEvent notification, CancellationToken cancellationToken)
    {
         _logger.LogInformation($"Triggered Delete product event: {notification.FilePaths}");
         var fileDeletionResult  = _fileUploadService.DeleteFiles(notification.FilePaths);

         if (fileDeletionResult.IsFailed)
         {
             var errors = fileDeletionResult.Errors.Select(error => error.Message);
             
             _logger.LogError($"Failed to delete files: {errors}", errors);
             
         }
         else
         {
             _logger.LogInformation($"Deleted files: {notification.FilePaths}");
         }

         return Task.CompletedTask;
    }
}