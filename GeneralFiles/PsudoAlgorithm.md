# Psudo Algorithm

## Services
- FnFMS 
    - Controller
    - Service
    - Repository

## Endpoints and Methods
- Endpoints and Methods
    - Create folder
    - Update folderName
    - Delete folder

    - Upload file
    - Download fileName
    - Update fileName
    - Delete file

---

## Psuedo Code
### Psuedo Code - Endpoint: Create folder
#### FnFMSController
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

    // Example endpoint syntax
    [HttpPost("folder/create/root-folder")]
    [Authorize]
    public async Task<IActionResult> CreateRootFolder([FromBody] CreateRootFolderRequestDto request)
    {
        try
        {
            var folderEntity = await _fnFMSService.CreateRootFolderAsync(userId, request);

            return Ok(folderEntity);
        }
        catch (Exception exception)
        {
            return StatusCode(500, exception.Message);
        }
    }
}

##### Create Folder

- 