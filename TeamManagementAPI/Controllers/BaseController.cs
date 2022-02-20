using Microsoft.AspNetCore.Mvc;

namespace TeamManagementAPI.Controllers;
/// <summary>
/// Base controller for the controllers
/// </summary>
[ApiController]
[Route("api/v{version:apiVersion}/[controller]")]
public abstract class BaseController : ControllerBase
{
    
}