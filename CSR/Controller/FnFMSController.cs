using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Security.Claims;
using System.Threading.Tasks;

[Authorize]
[Route("api/[controller]")]
[ApiController]
public class FnFMSController : ControllerBase
{
    private readonly IFnFMSService _fnFMSService;

    public FnFMSController(IFnFMSService fnFMSService)
    {
        _fnFMSService = fnFMSService;
    }

    // Endpoints
    // Folder
    // CreateRootFolder
    [HttpPost("folder/create/root-folder")]
    [Authorize]
    public async Task<IActionResult> CreateRootFolder([FromBody] CreateRootFolderRequestDto request)
    {
        try
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (userId == null)
            {
                return Unauthorized();
            }

            var folderEntity = await _fnFMSService.CreateRootFolderAsync(userId, request);

            return Ok(folderEntity);
        }
        catch (Exception exception)
        {
            return StatusCode(500, exception.Message);
        }
    }

    // CreateFolderInFolder
    // UpdateFolderName
    // DeleteFolder

    // File
    // UploadFileToFolder
    // DownloadFileFromFolder
    // UpdateFileName
    // DeleteFile
}
