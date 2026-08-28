namespace TodoApp.Domain.Constants;

public static class ValidationConstants
{
   public static class User
   {
      public const int MinimumPasswordLength = 6;
      public const int MaximumPasswordLength = 50;
      
      public const int MinimumUsernameLength = 3;
      public const int MaximumUsernameLength = 30;
   }

   public static class Category
   {
      public const int MaximumCategoryNameLenght = 100;
   }

   public static class Task
   {
      public const int MaximumTaskTitleLenght = 200;
      public const int MaximumTaskNoteLenght = 3000;
   }
}