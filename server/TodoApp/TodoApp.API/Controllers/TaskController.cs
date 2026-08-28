using System.IdentityModel.Tokens.Jwt;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using TodoApp.API.Helpers.Jwt;
using TodoApp.Domain.Contracts.Response;
using TodoApp.Domain.Contracts.Task;
using TodoApp.Domain.Interfaces.Services;

namespace TodoApp.API.Controllers;

[ApiController]
[Route("api/tasks")]
public class TaskController : ControllerBase
{
   private readonly ITaskService _taskService;
   
   public TaskController(ITaskService taskService)
   {
      _taskService = taskService;
   }

   [HttpGet("me")]
   [SwaggerOperation("Get user tasks with search and filtering")]
   [Authorize]
   public async Task<ActionResult<ApiResponse>> GetUserTasks([FromQuery] SearchTaskOptions searchTaskOptions, CancellationToken cancellationToken)
   {
      var userId = JwtHelper.ExtractUserId(HttpContext)!;
      var searchResult = await _taskService.GetUserTasks(userId.Value, searchTaskOptions, cancellationToken);
      return Ok(ApiResponse.Ok(searchResult));
   }
   
   [HttpPost]
   [SwaggerOperation("Create a new task")]
   [Authorize]
   public async Task<ActionResult<ApiResponse>> CreateTask([FromBody] CreateTaskRequest request, CancellationToken cancellationToken)
   {
      var userId = JwtHelper.ExtractUserId(HttpContext)!;
      await _taskService.CreateTask(userId.Value, request, cancellationToken);
      return Ok(ApiResponse.Ok("Task created successfully"));
   }

   [HttpPut]
   [SwaggerOperation("Update an existing task")]
   [Authorize]
   public async Task<ActionResult<ApiResponse>> UpdateTask([FromBody] UpdateTaskRequest request, CancellationToken cancellationToken)
   {
      var userId = JwtHelper.ExtractUserId(HttpContext)!;
      await _taskService.UpdateTask(userId.Value, request, cancellationToken);
      return Ok(ApiResponse.Ok("Task updated successfully"));
   }

   [HttpDelete("{taskId:guid}")]
   [SwaggerOperation("Delete a user task")]
   [Authorize]
   public async Task<ActionResult<ApiResponse>> DeleteTask(Guid taskId, CancellationToken cancellationToken)
   {
      var userId = JwtHelper.ExtractUserId(HttpContext)!;
      await _taskService.DeleteTask(userId.Value, taskId, cancellationToken);
      return Ok(ApiResponse.Ok("Task deleted successfully"));
   }
}