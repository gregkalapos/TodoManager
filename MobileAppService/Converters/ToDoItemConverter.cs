using System;

using ToDoManager.Data.Entities;
using ToDoManager.Model;

namespace ToDoManager.MobileAppService.Converters
{
    public static class ToDoItemConverter
    {
        public static ToDoItem FromModelToEntity(ToDoItemModel model)
        {
            return new ToDoItem
            {
                Created = model.Created,
                Id = model.Id,
                Title = model.Title,
                User = model.User,
				Description = model.Description,
				IsDone = model.IsDone
            };
        }

		public static ToDoItemModel FromEntityToModel(ToDoItem entity)
		{
			return new ToDoItemModel
			{
				Created = entity.Created,
				Id = entity.Id,
				Title = entity.Title,
				User = entity.User,
				Description = entity.Description,
				IsDone = entity.IsDone
			};
		}
    }
}