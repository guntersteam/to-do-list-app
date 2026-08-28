using AutoMapper;
using TodoApp.Domain.Contracts.Exception;
using TodoApp.Domain.Contracts.User;
using TodoApp.Domain.Interfaces.Helpers;
using TodoApp.Domain.Interfaces.Repositories;
using TodoApp.Domain.Interfaces.Services;
using TodoApp.Domain.Models;

using Task = System.Threading.Tasks.Task;

namespace TodoApp.Application.Services;

public class AuthService : IAuthService
{
   private readonly IUserRepository _userRepository;
   private readonly IUnitOfWork _unitOfWork;
   private readonly IPasswordHasher _passwordHasher;
   private readonly ITokenService _tokenService;
   private readonly IMapper _mapper;

   public AuthService(IUserRepository userRepository, IUnitOfWork unitOfWork, IPasswordHasher passwordHasher, ITokenService tokenService, IMapper mapper)
   {
      _userRepository = userRepository;
      _unitOfWork = unitOfWork;
      _passwordHasher = passwordHasher;
      _tokenService = tokenService;
      _mapper = mapper;
   }
   
   public async Task Register(RegisterUserRequest request, CancellationToken cancellationToken = default)
   {
      var isUserExist = await _userRepository.IsUserExist(request.Email,request.Username);

      if (isUserExist)
      {
         throw new ApiException($"User with email: {request.Email} already exists.",400);
      }
      
      var passwordHash = _passwordHasher.Generate(request.Password);
      
      var user = new User
      {
         Email = request.Email,
         Username = request.Username,
         Password = passwordHash,
      };
      
      await _userRepository.Add(user);
      await _unitOfWork.SaveChangesAsync(cancellationToken);
   }

   public async Task<(LoginUserResponse response, string refreshToken)> Login(LoginUserRequest request, CancellationToken cancellationToken = default)
   {
      var loginUserResult = new LoginUserResponse();

      var candidate = (await _userRepository.GetByPredicate(u => u.Email == request.Email)).FirstOrDefault();

      if (candidate == null)
      {
         throw new ApiException($"User with email {request.Email} wasn't found", 404);
      }
      
      var passwordVerifyResult = _passwordHasher.Verify(request.Password, candidate.Password);

      if (!passwordVerifyResult)
      {
         throw new ApiException("Incorrect password", 400);
      }
      
      var (accessToken, refreshToken) = await _tokenService.GenerateTokens(candidate);

      loginUserResult.AccessToken = accessToken;
      loginUserResult.User = _mapper.Map<UserDto>(candidate);

      return (loginUserResult, refreshToken);
   }

   public async Task<(LoginUserResponse loginUserResponse, string refreshToken)> RefreshTokens(string refreshToken, CancellationToken cancellationToken)
   {
      var loginResponse = new LoginUserResponse();

      if (string.IsNullOrEmpty(refreshToken))
      {
         throw new ApiException("Token  is empty", 400);
      }

      var (isValid, userId) = _tokenService.IsTokenValid(refreshToken);

      if (!isValid)
      {
         throw new ApiException("Invalid refresh token",400);
      }
      
      var user = await _userRepository.FindById(Guid.Parse(userId));

      if (user == null)
      {
         throw new ApiException("User wasn't found", 404);
      }

      var (newAccessToken, newRefreshToken) = await _tokenService.GenerateTokens(user);

      loginResponse.AccessToken = newAccessToken;
      loginResponse.User = _mapper.Map<UserDto>(user);
      
      return (loginResponse,newRefreshToken);
   }

   public async Task<UserDto> GetUserInformation(Guid userId, CancellationToken cancellationToken)
   {
      var user = await _userRepository.FindById(userId);

      return user == null ? throw new ApiException("User wasn't found", 404) : _mapper.Map<UserDto>(user);
   }
}