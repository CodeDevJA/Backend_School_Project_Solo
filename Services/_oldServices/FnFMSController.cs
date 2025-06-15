// using Microsoft.AspNetCore.Authorization;
// using Microsoft.AspNetCore.Mvc;
// using System.Security.Claims;
// using System;
// using System.Threading.Tasks;

// [Authorize]
// [Route("api/[controller]")]
// [ApiController]
// public class FnFMSController : ControllerBase
// {
//     private readonly IFnFMSService _fnFMSService;

//     public FnFMSController(IFnFMSService fnFMSService)
//     {
//         _fnFMSService = fnFMSService;
//     }

//     /// <summary>
//     /// Creates a root folder for the authenticated user.
//     /// </summary>
//     /// <param name="request">The details of the root folder to create.</param>
//     /// <returns>An action result indicating success or failure.</returns>
//     [HttpPost("folder/create/root-folder")]
//     [Authorize]
//     public async Task<IActionResult> CreateRootFolder([FromBody] CreateRootFolderRequestDto request)
//     {
//         try
//         {
//             var userIdClaim = User.FindFirstValue(ClaimTypes.NameIdentifier);
//             if (userIdClaim is null || !Guid.TryParse(userIdClaim, out var userId))
//             {
//                 return Unauthorized();
//             }

//             var result = await _fnFMSService.CreateRootFolderAsync(userId, request);
//             if (result == null)
//             {
//                 return BadRequest("Could not create folder.");
//             }

//             return Ok(result);
//         }
//         catch (Exception ex)
//         {
//             return StatusCode(500, ex.Message);
//         }
//     }

//     /// <summary>
//     /// Creates a folder within an existing folder for the authenticated user.
//     /// </summary>
//     /// <param name="request">The details of the folder to create.</param>
//     /// <returns>An action result indicating success or failure.</returns>
//     [HttpPost("folder/create/folder-in-folder")]
//     [Authorize]
//     public async Task<IActionResult> CreateFolderInFolder([FromBody] CreateFolderInFolderRequestDto request)
//     {
//         try
//         {
//             var userIdClaim = User.FindFirstValue(ClaimTypes.NameIdentifier);
//             if (userIdClaim is null || !Guid.TryParse(userIdClaim, out var userId))
//             {
//                 return Unauthorized();
//             }

//             var result = await _fnFMSService.CreateFolderInFolderAsync(userId, request);
//             if (result is null)
//             {
//                 return BadRequest("Could not create folder.");
//             }

//             return Ok(result);
//         }
//         catch (Exception ex)
//         {
//             return StatusCode(500, ex.Message);
//         }
//     }

//     /// <summary>
//     /// Updates the name of a folder for the authenticated user.
//     /// </summary>
//     /// <param name="request">The folder ID and new name.</param>
//     /// <returns>An action result indicating success or failure.</returns>
//     [HttpPut("folder/update/name")]
//     [Authorize]
//     public async Task<IActionResult> UpdateFolderName([FromBody] UpdateFolderNameRequestDto request)
//     {
//         try
//         {
//             var userIdClaim = User.FindFirstValue(ClaimTypes.NameIdentifier);
//             if (userIdClaim is null || !Guid.TryParse(userIdClaim, out var userId))
//             {
//                 return Unauthorized();
//             }

//             var result = await _fnFMSService.UpdateFolderNameAsync(userId, request);
//             if (result is null)
//             {
//                 return NotFound("Folder not found or update failed.");
//             }

//             return Ok(result);
//         }
//         catch (Exception ex)
//         {
//             return StatusCode(500, ex.Message);
//         }
//     }

//     /// <summary>
//     /// Deletes a folder for the authenticated user.
//     /// </summary>
//     /// <param name="request">The folder to be deleted.</param>
//     /// <returns>An action result indicating success or failure.</returns>
//     [HttpDelete("folder/delete")]
//     [Authorize]
//     public async Task<IActionResult> DeleteFolder([FromBody] DeleteFolderRequestDto request)
//     {
//         try
//         {
//             var userIdClaim = User.FindFirstValue(ClaimTypes.NameIdentifier);
//             if (userIdClaim is null || !Guid.TryParse(userIdClaim, out var userId))
//             {
//                 return Unauthorized();
//             }

//             var result = await _fnFMSService.DeleteFolderAsync(userId, request);
//             if (!result.Success)
//             {
//                 return NotFound(result);
//             }

//             return Ok(result);
//         }
//         catch (Exception ex)
//         {
//             return StatusCode(500, ex.Message);
//         }
//     }

//     /// <summary>
//     /// Uploads a file to a specific folder for the authenticated user.
//     /// </summary>
//     /// <param name="request">The file and target folder information.</param>
//     /// <returns>An action result with upload result or error.</returns>
//     [HttpPost("file/upload")]
//     [Authorize]
//     public async Task<IActionResult> UploadFileToFolder([FromForm] UploadFileRequestDto request)
//     {
//         try
//         {
//             var userIdClaim = User.FindFirstValue(ClaimTypes.NameIdentifier);
//             if (userIdClaim is null || !Guid.TryParse(userIdClaim, out var userId))
//             {
//                 return Unauthorized();
//             }

//             var result = await _fnFMSService.UploadFileToFolderAsync(userId, request);
//             if (result is null)
//             {
//                 return BadRequest("File upload failed.");
//             }

//             return Ok(result);
//         }
//         catch (Exception ex)
//         {
//             return StatusCode(500, ex.Message);
//         }
//     }

//     /// <summary>
//     /// Downloads a file from a folder for the authenticated user.
//     /// </summary>
//     /// <param name="fileId">The ID of the file to download.</param>
//     /// <returns>The file stream or an error if not found or unauthorized.</returns>
//     [HttpGet("file/download/{fileId}")]
//     [Authorize]
//     public async Task<IActionResult> DownloadFileFromFolder(Guid fileId)
//     {
//         try
//         {
//             var userIdClaim = User.FindFirstValue(ClaimTypes.NameIdentifier);
//             if (userIdClaim is null || !Guid.TryParse(userIdClaim, out var userId))
//             {
//                 return Unauthorized();
//             }

//             var result = await _fnFMSService.DownloadFileFromFolderAsync(userId, fileId);
//             if (result is null)
//             {
//                 return NotFound("File not found or unauthorized.");
//             }

//             return File(result.Content, "application/octet-stream", result.Filename);
//         }
//         catch (Exception ex)
//         {
//             return StatusCode(500, ex.Message);
//         }
//     }

//     /// <summary>
//     /// Updates the filename of an existing file for the authenticated user.
//     /// </summary>
//     /// <param name="request">The file ID and the new filename.</param>
//     /// <returns>An action result indicating success or failure.</returns>
//     [HttpPut("file/update-name")]
//     [Authorize]
//     public async Task<IActionResult> UpdateFileName([FromBody] UpdateFileNameRequestDto request)
//     {
//         try
//         {
//             var userIdClaim = User.FindFirstValue(ClaimTypes.NameIdentifier);
//             if (userIdClaim is null || !Guid.TryParse(userIdClaim, out var userId))
//             {
//                 return Unauthorized();
//             }

//             var success = await _fnFMSService.UpdateFileNameAsync(userId, request.FileId, request.NewFilename);
//             if (!success)
//             {
//                 return NotFound("File not found or unauthorized.");
//             }

//             return Ok("Filename updated successfully.");
//         }
//         catch (Exception ex)
//         {
//             return StatusCode(500, ex.Message);
//         }
//     }

//     /// <summary>
//     /// Deletes a file for the authenticated user.
//     /// </summary>
//     /// <param name="request">The file to delete.</param>
//     /// <returns>An action result indicating success or failure.</returns>
//     [HttpDelete("file/delete")]
//     [Authorize]
//     public async Task<IActionResult> DeleteFile([FromBody] DeleteFileRequestDto request)
//     {
//         try
//         {
//             var userIdClaim = User.FindFirstValue(ClaimTypes.NameIdentifier);
//             if (userIdClaim is null || !Guid.TryParse(userIdClaim, out var userId))
//             {
//                 return Unauthorized();
//             }

//             var success = await _fnFMSService.DeleteFileAsync(userId, request.FileId);
//             if (!success)
//             {
//                 return NotFound("File not found or unauthorized.");
//             }

//             return Ok("File deleted successfully.");
//         }
//         catch (Exception ex)
//         {
//             return StatusCode(500, ex.Message);
//         }
//     }
// }
