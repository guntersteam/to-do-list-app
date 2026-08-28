using AutoMapper;
using TodoApp.Domain.Contracts.Category;
using TodoApp.Domain.Contracts.Exception;
using TodoApp.Domain.Interfaces.Repositories;
using TodoApp.Domain.Interfaces.Services;
using TodoApp.Domain.Models;
using Task = System.Threading.Tasks.Task;

namespace TodoApp.Application.Services;

public class CategoryService : ICategoryService
{
   private readonly ICategoryRepository _categoryRepository;
   private readonly IUnitOfWork _unitOfWork;
   private readonly IMapper _mapper;

   public CategoryService(ICategoryRepository categoryRepository, IUnitOfWork unitOfWork, IMapper mapper)
   {
      _categoryRepository = categoryRepository;
      _unitOfWork = unitOfWork;
      _mapper = mapper;
   }

   public async Task CreateCategory(Guid userId, CreateCategoryRequest request, CancellationToken cancellationToken)
   {
      var isExist = await _categoryRepository.IsExist(userId,request.CategoryName);
      
      if (isExist)
      {
         throw new ApiException("Category already exist", 400);
      }
      
      var category = new Category
      {
         UserId = userId,
         Name = request.CategoryName
      };
      
      await _categoryRepository.Add(category);
      await _unitOfWork.SaveChangesAsync(cancellationToken);
   }

   public async Task<List<CategoryDto>> GetUserCategories(Guid userId,CancellationToken cancellationToken)
   {
      var categories = (await _categoryRepository.GetByPredicate(c => c.UserId == userId)).ToList();
      return _mapper.Map<List<CategoryDto>>(categories);
   }

   public async Task DeleteCategory(Guid userId, Guid categoryId,CancellationToken cancellationToken)
   {
      var category = await _categoryRepository.FindById(categoryId);

      if (category == null)
      {
         throw new ApiException("Category not found",404);
      }

      if (category.UserId != userId)
      {
         throw new ApiException("You don't have permission for deleting this category", 403);
      }
      
      await _categoryRepository.DeleteAsync(category.Id);
      await _unitOfWork.SaveChangesAsync(cancellationToken);
   }

   public async Task UpdateCategory(Guid userId, UpdateCategoryRequest request,CancellationToken cancellationToken)
   {
      var category = await _categoryRepository.FindById(request.CategoryId);
      if (category == null)
      {
         throw new ApiException("Category not found", 404);
      }

      if (category.UserId != userId)
      {
         throw new ApiException("You don't have permission for updating this category", 403);
      }
      category.Name = request.CategoryName;
      _categoryRepository.Update(category);
      await _unitOfWork.SaveChangesAsync(cancellationToken);
   }
}