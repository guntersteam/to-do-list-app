using System.ComponentModel.DataAnnotations;
using TodoApp.Domain.Constants;

namespace TodoApp.Domain.Contracts.Category;

public class UpdateCategoryRequest
{
   public Guid CategoryId { get; set; }
   
   [MaxLength(ValidationConstants.Category.MaximumCategoryNameLenght, ErrorMessage = "{0} can't be longer than {1} characters")]
   public string CategoryName { get; set; }
}