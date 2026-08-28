using TodoApp.Domain.Contracts.User;

namespace TodoApp.Domain.Interfaces.Services;

public interface IAuthService
{
   Task Register(RegisterUserRequest request, CancellationToken cancellationToken);
   Task<(LoginUserResponse response, string refreshToken)> Login(LoginUserRequest request, CancellationToken cancellationToken);
   Task<(LoginUserResponse loginUserResponse, string refreshToken)> RefreshTokens(string refreshToken,  CancellationToken cancellationToken);
   Task<UserDto> GetUserInformation(Guid userId,CancellationToken cancellationToken);
}