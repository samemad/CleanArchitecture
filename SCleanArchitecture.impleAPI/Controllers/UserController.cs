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

        //  Get All Users endpoint !!
        [HttpGet]
        public async Task<IActionResult> GetAllUsers()
        {
            var result = await _userService.GetAllUsers();
            return Ok(result);
        }

        [HttpGet("{id}")]  // ← {id} means it expects a parameter in the URL
        public async Task<IActionResult> GetUserById(int id)
        {
            var result = await _userService.GetUserById(id);

            // If user not found, return 404 Not Found {error handelr !}
            if (result == null)
                return NotFound($"User with ID {id} not found");

            // If found, return 200 OK with the user
            return Ok(result);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateUser(UpdateUserRequestDto requestDto)
        {
            var result = await _userService.UpdateUser(requestDto);

            // check If user not found, return 404
            if (result == null)
                return NotFound($"User with ID {requestDto.Id} not found");

            // check If updated successfully, return 200 OK with updated user
            return Ok(result);
        }



    }
}