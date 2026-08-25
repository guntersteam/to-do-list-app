using System.IdentityModel.Tokens.Jwt;
using System.Net;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using TodoApp.API.Helpers.Jwt;
using TodoApp.Domain.Contracts.Category;
using TodoApp.Domain.Contracts.Response;
using TodoApp.Domain.Interfaces.Services;

namespace TodoApp.API.Controllers;

[ApiController]
[Route("api/categories")]
public class CategoryController : ControllerBase
{
   private readonly ICategoryService _categoryService;
   public CategoryController(ICategoryService categoryService)
    {
      _categoryService = categoryService;
    }

   [HttpGet("me")]
   [SwaggerOperation("Get user categories")]
   [Authorize]
   public async Task<ActionResult<ApiResponse>> GetCategory(CancellationToken cancellationToken)
   {
      var userId = JwtHelper.ExtractUserId(HttpContext)!;
      var userCategories = await _categoryService.GetUserCategories(userId.Value,cancellationToken);
      return Ok(ApiResponse.Ok(userCategories));
   }

   [HttpPost]
   [SwaggerOperation("Create user category")]
   [Authorize]
   public async Task<ActionResult<ApiResponse>> CreateCategory([FromBody] CreateCategoryRequest request,
      CancellationToken cancellationToken)
   {
      var userId = JwtHelper.ExtractUserId(HttpContext)!;
      await _categoryService.CreateCategory(userId.Value, request, cancellationToken);
      return Ok(ApiResponse.Ok());
   }

   [HttpPatch]
   [SwaggerOperation("Update user category")]
   [Authorize]
   public async Task<ActionResult<ApiResponse>> UpdateCategory([FromBody] UpdateCategoryRequest request,
      CancellationToken cancellationToken)
   {
      var userId = JwtHelper.ExtractUserId(HttpContext)!;
      await _categoryService.UpdateCategory(userId.Value,request, cancellationToken);
      return Ok(ApiResponse.Ok());
   }

   [HttpDelete("{categoryId:guid}")]
   [SwaggerOperation("Delete user category")]
   [Authorize]
   public async Task<ActionResult<ApiResponse>> DeleteCategory(Guid categoryId,
      CancellationToken cancellationToken)
   {
      var userId = JwtHelper.ExtractUserId(HttpContext)!;
      await _categoryService.DeleteCategory(userId.Value, categoryId, cancellationToken);
      return Ok(ApiResponse.Ok());
   }
   
}