using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System;
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

    // Endpoint - Folder - CreateRootFolder
    [HttpPost("folder/create/root-folder")]
    [Authorize]
    public async Task<IActionResult> CreateRootFolder([FromBody] CreateRootFolderRequestDto request)
    {
        try
        {
            var userIdClaim = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (userIdClaim is null || !Guid.TryParse(userIdClaim, out var userId))
            {
                return Unauthorized();
            }

            var result = await _fnFMSService.CreateRootFolderAsync(userId, request);
            if (result == null)
            {
                return BadRequest("Could not create folder.");
            }

            return Ok(result);
        }
        catch (Exception ex)
        {
            return StatusCode(500, ex.Message);
        }
    }

    // Endpoint - Folder - CreateFolderInFolder
    [HttpPost("folder/create/folder-in-folder")]
    [Authorize]
    public async Task<IActionResult> CreateFolderInFolder([FromBody] CreateFolderInFolderRequestDto request)
    {
        try
        {
            var userIdClaim = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (userIdClaim is null || !Guid.TryParse(userIdClaim, out var userId))
            {
                return Unauthorized();
            }

            var result = await _fnFMSService.CreateFolderInFolderAsync(userId, request);
            if (result is null)
            {
                return BadRequest("Could not create folder.");
            }

            return Ok(result);
        }
        catch (Exception ex)
        {
            return StatusCode(500, ex.Message);
        }
    }

    // Endpoint - Folder - UpdateFolderName
    [HttpPut("folder/update/name")]
    [Authorize]
    public async Task<IActionResult> UpdateFolderName([FromBody] UpdateFolderNameRequestDto request)
    {
        try
        {
            var userIdClaim = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (userIdClaim is null || !Guid.TryParse(userIdClaim, out var userId))
            {
                return Unauthorized();
            }

            var result = await _fnFMSService.UpdateFolderNameAsync(userId, request);
            if (result is null)
            {
                return NotFound("Folder not found or update failed.");
            }

            return Ok(result);
        }
        catch (Exception ex)
        {
            return StatusCode(500, ex.Message);
        }
    }

    // Endpoint - Folder - DeleteFolder

    // Endpoint - File - UploadFileToFolder
    // Endpoint - File - DownloadFileFromFolder
    // Endpoint - File - UpdateFileName
    // Endpoint - File - DeleteFile
}
