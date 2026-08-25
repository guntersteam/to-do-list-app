using System.ComponentModel.DataAnnotations;
using TodoApp.Domain.Constants;

namespace TodoApp.Domain.Contracts.Category;

public class CreateCategoryRequest
{
   [MaxLength(ValidationConstants.Category.MaximumCategoryNameLenght,ErrorMessage = "{0} can't be longer than {1} characters")]
   public string CategoryName { get; set; }
}