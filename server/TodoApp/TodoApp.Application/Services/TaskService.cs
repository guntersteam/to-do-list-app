using AutoMapper;
using TodoApp.Domain.Contracts.Exception;
using TodoApp.Domain.Contracts.Task;
using TodoApp.Domain.Interfaces.Repositories;
using TodoApp.Domain.Interfaces.Services;
using DomainTask = TodoApp.Domain.Models.Task;

namespace TodoApp.Application.Services;

public class TaskService : ITaskService
{
   private readonly ITaskRepository _taskRepository;
   private readonly IMapper _mapper;
   private readonly IUnitOfWork _unitOfWork;

   public TaskService(ITaskRepository taskRepository, IMapper mapper, IUnitOfWork unitOfWork)
   {
      _taskRepository = taskRepository;
      _mapper = mapper;
      _unitOfWork = unitOfWork;
   }

   public async Task CreateTask(Guid userId, CreateTaskRequest request, CancellationToken cancellationToken)
   {
      var task = new DomainTask
      {
         Id = Guid.NewGuid(),
         Title = request.Title,
         Note = request.Note,
         IsCompleted = false,
         DueTime = request.DueTime,
         UserId = userId,
      };
      
      await _taskRepository.Add(task);
      
      if (request.CategoryIds != null && request.CategoryIds.Count > 0)
      {
         await _taskRepository.CreateCategoryTasks(request.CategoryIds,task.Id,cancellationToken);
      }

      await _unitOfWork.SaveChangesAsync(cancellationToken);
   }

   public async Task UpdateTask(Guid userId, UpdateTaskRequest request, CancellationToken cancellationToken)
   {
      var task = await _taskRepository.FindById(request.TaskId);
      
      if (task == null)
      {
         throw new ApiException("Task wasn't found",404);
      }

      if (task.UserId != userId)
      {
         throw new ApiException("You can't update this task", 403);
      }
      
      await _taskRepository.UpdateCategoryTasks(request.CategoryIds,task.Id,cancellationToken);
      
      task.Title = request.Title;
      task.Note = request.Note;
      task.DueTime = request.DueTime;
      task.IsCompleted = request.IsCompleted ?? task.IsCompleted;
      
      _taskRepository.Update(task);
      await _unitOfWork.SaveChangesAsync(cancellationToken);
   }

   public async Task DeleteTask(Guid userId, Guid taskId, CancellationToken cancellationToken)
   {
      var task = await _taskRepository.FindById(taskId);

      if (task == null)
         return;

      if (task.UserId != userId)
      {
         throw new ApiException("You can't delete this task", 403);
      }
      
      await _taskRepository.DeleteAsync(taskId);
      await _unitOfWork.SaveChangesAsync(cancellationToken);
   }

   public async Task<SearchTaskResponse> GetUserTasks(Guid userId, SearchTaskOptions searchTaskOptions, CancellationToken cancellationToken)
   {
      var (items, totalCount) = await _taskRepository.GetPagedAndFilteredAsync(userId, searchTaskOptions);
      
      var taskDtos = _mapper.Map<List<TaskDto>>(items);
      
      return new SearchTaskResponse
      {
         Items = taskDtos,
         TotalCount = totalCount,
         Page = searchTaskOptions.Page,
         PageSize = searchTaskOptions.PageSize
      };
   }
}