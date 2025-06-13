// [HttpPost("file/upload")]
// [Authorize]
// public async Task<IActionResult> UploadFileToFolder([FromForm] Guid parentFolderId, [FromForm] IFormFile file)
// {
//     try
//     {
//         var userIdClaim = User.FindFirstValue(ClaimTypes.NameIdentifier);
//         if (userIdClaim is null || !Guid.TryParse(userIdClaim, out var userId))
//         {
//             return Unauthorized();
//         }

//         var result = await _fnFMSService.UploadFileToFolderAsync(userId, parentFolderId, file);
//         if (result is null)
//         {
//             return BadRequest("File upload failed.");
//         }

//         return Ok(result);
//     }
//     catch (Exception ex)
//     {
//         return StatusCode(500, ex.Message);
//     }
// }
