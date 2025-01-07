using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace BiteBox.Backend.Controllers;

[ApiController]
[Route("[controller]")]
public class UserController : ControllerBase
{

    private readonly ILogger<UserController> _logger;

    private readonly SettingsOptions _settingsOptions;

    public UserController(ILogger<UserController> logger, IOptions<SettingsOptions> settingsOptions)
    {
        _logger = logger;
        _settingsOptions = settingsOptions.Value;
    }

    [HttpGet("info")]
    public ActionResult GetAll()
    {
        return Ok(_settingsOptions.ApplicationName);
    }
}
