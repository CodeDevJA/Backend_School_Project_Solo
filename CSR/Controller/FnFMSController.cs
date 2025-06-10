using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

[Authorize(AuthenticationSchemes = IdentityConstants.BearerScheme)]
[ApiController]
[Route("api/fnfms")]
public class FnFMSController : ControllerBase
{
    private readonly FnFMSService _service;

    public FnFMSController(FnFMSService service)
    {
        _service = service;
    }

    public Task<ActionResult<Guid>> CreateRootFolderAsync([FromBody] CreateRootFolderRequest request)
    {
        throw new NotImplementedException();
    }

    public Task<ActionResult<Guid>> CreateFolderInFolderAsync([FromBody] CreateFolderInFolderRequest request)
    {
        throw new NotImplementedException();
    }

    public Task<ActionResult<bool>> UpdateFolderNameAsync([FromBody] UpdateFolderNameRequest request)
    {
        throw new NotImplementedException();
    }

    public Task<ActionResult<bool>> DeleteFolderAsync([FromBody] DeleteFolderRequest request)
    {
        throw new NotImplementedException();
    }

    public Task<ActionResult<Guid>> UploadFileToFolderAsync([FromBody] UploadFileToFolderRequest request)
    {
        throw new NotImplementedException();
    }

    public Task<ActionResult<DownloadFileFromFolderResponse?>> DownloadFileFromFolderAsync(Guid fileId)
    {
        throw new NotImplementedException();
    }

    public Task<ActionResult<bool>> UpdateFileNameAsync([FromBody] UpdateFileNameRequest request)
    {
        throw new NotImplementedException();
    }

    public Task<ActionResult<bool>> DeleteFileAsync([FromBody] DeleteFileRequest request)
    {
        throw new NotImplementedException();
    }
}
