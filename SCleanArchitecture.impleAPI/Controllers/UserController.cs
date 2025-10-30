using Microsoft.AspNetCore.Mvc;
using SCleanArchitecture.SimpleAPI.Application.DTOs;
using SCleanArchitecture.SimpleAPI.Application.Services;

namespace SCleanArchitecture.SimpleAPI.Controllers
{
    [ApiController]
    [Route("users")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        public UserController(IUserService userService)
        {
          _userService = userService;   
        }
 
        [HttpPost]
        public async Task<IActionResult> AddUser(AddUserRequestDto userRequestDto)
        {
            var result = await _userService.AddUser(userRequestDto);

            return Ok(result);
        }
    }
}
