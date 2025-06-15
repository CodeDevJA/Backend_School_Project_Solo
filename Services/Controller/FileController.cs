using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System;
using System.Threading.Tasks;

[Authorize]
[Route("api/[controller]")]
[ApiController]
public class FileController : ControllerBase
{
    private readonly IFileService _fileService;

    public FileController(IFileService fileService)
    {
        _fileService = fileService;
    }

    // Endpoint - File - UploadFileToFolder
    /// <summary>
    /// Uploads a file to a specific folder for the authenticated user.
    /// </summary>
    /// <param name="request">The file and target folder information.</param>
    /// <returns>An action result with upload result or error.</returns>
    [HttpPost("upload")]
    [Authorize]
    public async Task<IActionResult> UploadFileToFolder([FromForm] UploadFileRequestDto request)
    {
        try
        {
            var userIdClaim = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (userIdClaim is null || !Guid.TryParse(userIdClaim, out var userId))
            {
                return Unauthorized();
            }

            var result = await _fileService.UploadFileToFolderAsync(userId, request);
            if (result is null)
            {
                return BadRequest("File upload failed.");
            }

            return Ok(result);
        }
        catch (Exception ex)
        {
            return StatusCode(500, ex.Message);
        }
    }

    // Endpoint - File - DownloadFileFromFolder
    /// <summary>
    /// Downloads a file from a folder for the authenticated user.
    /// </summary>
    /// <param name="fileId">The ID of the file to download.</param>
    /// <returns>The file stream or an error if not found or unauthorized.</returns>
    [HttpGet("download/{fileId}")]
    [Authorize]
    public async Task<IActionResult> DownloadFileFromFolder(Guid fileId)
    {
        try
        {
            var userIdClaim = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (userIdClaim is null || !Guid.TryParse(userIdClaim, out var userId))
            {
                return Unauthorized();
            }

            var result = await _fileService.DownloadFileFromFolderAsync(userId, fileId);
            if (result is null)
            {
                return NotFound("File not found or unauthorized.");
            }

            return File(result.Content, "application/octet-stream", result.Filename);
        }
        catch (Exception ex)
        {
            return StatusCode(500, ex.Message);
        }
    }

    // Endpoint - File - UpdateFileName
    /// <summary>
    /// Updates the filename of an existing file for the authenticated user.
    /// </summary>
    /// <param name="request">The file ID and the new filename.</param>
    /// <returns>An action result indicating success or failure.</returns>
    [HttpPut("update-name")]
    [Authorize]
    public async Task<IActionResult> UpdateFileName([FromBody] UpdateFileNameRequestDto request)
    {
        try
        {
            var userIdClaim = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (userIdClaim is null || !Guid.TryParse(userIdClaim, out var userId))
            {
                return Unauthorized();
            }

            var success = await _fileService.UpdateFileNameAsync(userId, request.FileId, request.NewFilename);
            if (!success)
            {
                return NotFound("File not found or unauthorized.");
            }

            return Ok("Filename updated successfully.");
        }
        catch (Exception ex)
        {
            return StatusCode(500, ex.Message);
        }
    }

    // Endpoint - File - DeleteFile
    /// <summary>
    /// Deletes a file for the authenticated user.
    /// </summary>
    /// <param name="request">The file to delete.</param>
    /// <returns>An action result indicating success or failure.</returns>
    [HttpDelete("delete")]
    [Authorize]
    public async Task<IActionResult> DeleteFile([FromBody] DeleteFileRequestDto request)
    {
        try
        {
            var userIdClaim = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (userIdClaim is null || !Guid.TryParse(userIdClaim, out var userId))
            {
                return Unauthorized();
            }

            var success = await _fileService.DeleteFileAsync(userId, request.FileId);
            if (!success)
            {
                return NotFound("File not found or unauthorized.");
            }

            return Ok("File deleted successfully.");
        }
        catch (Exception ex)
        {
            return StatusCode(500, ex.Message);
        }
    }
}
