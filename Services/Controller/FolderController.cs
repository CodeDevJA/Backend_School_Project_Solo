using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System;
using System.Threading.Tasks;

[Authorize]
[Route("api/[controller]")]
[ApiController]
public class FolderController : ControllerBase
{
    private readonly IFolderService _folderService;

    public FolderController(IFolderService folderService)
    {
        _folderService = folderService;
    }

    // Endpoint - Folder - CreateRootFolder
    [HttpPost("create/root-folder")]
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

            var result = await _folderService.CreateRootFolderAsync(userId, request);
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
    [HttpPost("create/folder-in-folder")]
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

            var result = await _folderService.CreateFolderInFolderAsync(userId, request);
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
    [HttpPut("update/name")]
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

            var result = await _folderService.UpdateFolderNameAsync(userId, request);
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
    [HttpDelete("delete")]
    [Authorize]
    public async Task<IActionResult> DeleteFolder([FromBody] DeleteFolderRequestDto request)
    {
        try
        {
            var userIdClaim = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (userIdClaim is null || !Guid.TryParse(userIdClaim, out var userId))
            {
                return Unauthorized();
            }

            var result = await _folderService.DeleteFolderAsync(userId, request);
            if (!result.Success)
            {
                return NotFound(result);
            }

            return Ok(result);
        }
        catch (Exception ex)
        {
            return StatusCode(500, ex.Message);
        }
    }
}
