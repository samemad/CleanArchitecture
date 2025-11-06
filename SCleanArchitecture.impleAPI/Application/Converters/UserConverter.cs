using SCleanArchitecture.SimpleAPI.Application.DTOs;
using SCleanArchitecture.SimpleAPI.Application.Services;
using SCleanArchitecture.SimpleAPI.Domain.Entities;

namespace SCleanArchitecture.SimpleAPI.Application.Converters;

internal static class UserConverter
{
    public static User ToUserEntity(this AddUserRequestDto requestDto)
    {
        return new User
        {
            Name = requestDto.Name,
            Email = requestDto.Email,
        };
    }

    public static AddUserResponseDto ToAddUserResponse(this AddUserRequestDto requestDto, DateTime createdAt)
    {
        return new AddUserResponseDto
        {
            Name = requestDto.Name,
            Email = requestDto.Email,
            CreatedAt = createdAt,
        };
    }

    // ⚡ ADD THIS NEW METHOD HERE! ⚡
    public static AddUserResponseDto ToAddUserResponse(this User user)
    {
        return new AddUserResponseDto
        {
            Name = user.Name,
            Email = user.Email,
            CreatedAt = user.CreatedAt
        };

    }

    // this is for convert UpdateUserRequestDto to User entity
   
   
    public static User ToUserEntity(this UpdateUserRequestDto requestDto)
    {
        return new User
        {
            Id = requestDto.Id,
            Name = requestDto.Name,
            Email = requestDto.Email,
        };
    }


    //Add dependency injection helper
    public static IServiceCollection AddService(this IServiceCollection services)
    {
        services.AddScoped<IUserService, UserService>();
        services.AddScoped<IProductService, ProductService>();
        services.AddScoped<ICategoryService, CategoryService>();
        return services;
    }
}