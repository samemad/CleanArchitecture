using SCleanArchitecture.SimpleAPI.Application.Converters;
using SCleanArchitecture.SimpleAPI.Application.DTOs;
using SCleanArchitecture.SimpleAPI.Domain.Repositories;

namespace SCleanArchitecture.SimpleAPI.Application.Services;

public interface IUserService
{
    Task<AddUserResponseDto> AddUser(AddUserRequestDto requestDto);
    Task<List<AddUserResponseDto>> GetAllUsers();
    Task<AddUserResponseDto> GetUserById(int id);
    Task<AddUserResponseDto> UpdateUser(UpdateUserRequestDto requestDto);
}

internal sealed class UserService : IUserService
{
    private readonly IUserRepository _userRepository;
    public UserService(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<AddUserResponseDto> AddUser(AddUserRequestDto requestDto)
    {
        if (!requestDto.IsValid())
        {
            return UserErrors.InvalidRequest();
        }

        var userEntity = requestDto.ToUserEntity();
        await _userRepository.AddUserAsync(userEntity);
        var response = requestDto.ToAddUserResponse(userEntity.CreatedAt);

        return response;

        // ✅ Removed try-catch completely
    }

    public async Task<List<AddUserResponseDto>> GetAllUsers()
    {
        // this will get all users from repository!!
        var users = await _userRepository.GetAllUsersAsync();

        // this will convert each User entity to AddUserResponseDto using Converter!!
        var response = users.Select(u => u.ToAddUserResponse()).ToList();

        return response;
    }

    public async Task<AddUserResponseDto> GetUserById(int id)
    {
        // this will get all users from repository!!
        var user = await _userRepository.GetUserByIdAsync(id);

        // If user not found, return null as a result
        if (user == null)
            return null;

        // this will convert each User entity to AddUserResponseDto using Converter!!
        var response = user.ToAddUserResponse();

        return response;
    }

    public async Task<AddUserResponseDto> UpdateUser(UpdateUserRequestDto requestDto)
    {
        // check if user exists
        var existingUser = await _userRepository.GetUserByIdAsync(requestDto.Id);

        if (existingUser == null)
            return null;  // if User not found

        // then Convert DTO to Entity
        var userEntity = requestDto.ToUserEntity();

        // then Update user in repository
        await _userRepository.UpdateUserAsync(userEntity);

        // then Get the updated user to return
        var updatedUser = await _userRepository.GetUserByIdAsync(requestDto.Id);

        // and lastly Convert to response DTO
        var response = updatedUser.ToAddUserResponse();

        return response;
    }


}



public static class UserErrors
{
    public static AddUserResponseDto InvalidRequest()
    {
        return new AddUserResponseDto();
    }
}
