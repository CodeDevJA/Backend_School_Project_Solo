using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
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

    [HttpPost("CreateRootFolder")]
    public async Task<ActionResult<CreateRootFolderResponseDto>> CreateRootFolder(
        [FromBody] CreateRootFolderRequestDto request)
    {
        try
        {
            var result = await _fnFMSService.CreateRootFolderAsync(request.FolderName);
            return Ok(result);
        }
        catch (Exception ex)
        {
            return BadRequest(new { Error = ex.Message });
        }
    }

    [HttpPost("CreateFolderInFolder")]
    public async Task<ActionResult<CreateFolderInFolderResponseDto>> CreateFolderInFolder(
        [FromBody] CreateFolderInFolderRequestDto request)
    {
        try
        {
            var result = await _fnFMSService.CreateFolderInFolderAsync(request);
            return Ok(result);
        }
        catch (Exception ex)
        {
            return BadRequest(new { Error = ex.Message });
        }
    }

    [HttpPut("UpdateFolderName/{folderId}")]
    public async Task<ActionResult<UpdateFolderNameResponseDto>> UpdateFolderName(
        Guid folderId, [FromBody] UpdateFolderNameRequestDto request)
    {
        try
        {
            var result = await _fnFMSService.UpdateFolderNameAsync(folderId, request);
            return Ok(result);
        }
        catch (Exception ex)
        {
            return BadRequest(new { Error = ex.Message });
        }
    }

    [HttpDelete("DeleteFolder/{folderId}")]
    public async Task<ActionResult<DeleteFolderResponseDto>> DeleteFolder(Guid folderId)
    {
        try
        {
            var result = await _fnFMSService.DeleteFolderAsync(folderId);
            return Ok(result);
        }
        catch (Exception ex)
        {
            return BadRequest(new { Error = ex.Message });
        }
    }

    [HttpPost("UploadFileToFolder")]
    public async Task<ActionResult<UploadFileToFolderResponseDto>> UploadFileToFolder(
        [FromBody] UploadFileToFolderRequestDto request)
    {
        try
        {
            var result = await _fnFMSService.UploadFileToFolderAsync(request);
            return Ok(result);
        }
        catch (Exception ex)
        {
            return BadRequest(new { Error = ex.Message });
        }
    }

    [HttpGet("DownloadFileFromFolder/{fileId}")]
    public async Task<ActionResult<DownloadFileFromFolderResponseDto>> DownloadFileFromFolder(Guid fileId)
    {
        try
        {
            var result = await _fnFMSService.DownloadFileFromFolderAsync(fileId);
            return File(result.FileContent, "application/octet-stream", result.Filename);
        }
        catch (Exception ex)
        {
            return BadRequest(new { Error = ex.Message });
        }
    }

    [HttpPut("UpdateFileName/{fileId}")]
    public async Task<ActionResult<UpdateFileNameResponseDto>> UpdateFileName(
        Guid fileId, [FromBody] UpdateFileNameRequestDto request)
    {
        try
        {
            var result = await _fnFMSService.UpdateFileNameAsync(fileId, request);
            return Ok(result);
        }
        catch (Exception ex)
        {
            return BadRequest(new { Error = ex.Message });
        }
    }

    [HttpDelete("DeleteFile/{fileId}")]
    public async Task<ActionResult<DeleteFileResponseDto>> DeleteFile(Guid fileId)
    {
        try
        {
            var result = await _fnFMSService.DeleteFileAsync(fileId);
            return Ok(result);
        }
        catch (Exception ex)
        {
            return BadRequest(new { Error = ex.Message });
        }
    }
}
