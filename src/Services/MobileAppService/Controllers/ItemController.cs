using System;
using Microsoft.AspNetCore.Mvc;
using ToDoManager.Data;
using ToDoManager.MobileAppService.Converters;
using ToDoManager.Model;
using System.Collections.Generic;
using Microsoft.Extensions.Logging;
using System.Linq;
using ToDoManager.MobileAppService;

namespace ToDoManager.Controllers
{
	[Route("api/[controller]")]
	public class ItemController : Controller
	{
		private readonly ToDoDbContext dbContext;
		private readonly ILogger logger;

		public ItemController(ToDoDbContext dbContext, ILogger<ItemController> logger)
		{
			this.logger = logger;
			this.dbContext = dbContext;

			if(dbContext.ToDoItems.Count() == 0)
			{
				Startup.FillInMemoryDbContext(dbContext);
			}
		}

		/// <summary>
		/// Returns the "default" list. 
		/// "default": not done todo items 
		/// </summary>
		/// <returns>The list.</returns>
		[HttpGet]
		public IActionResult List()
		{
			List<ToDoItemModel> todoItems = new List<ToDoItemModel>();

			logger.LogInformation($"# of PomodoreItems: {dbContext.PomodoroItems.ToList().Count}");
			logger.LogInformation($"# of todoItems: {dbContext.ToDoItems.ToList().Count}");

			foreach (var item in dbContext.ToDoItems.Where(n => !n.IsDone))
			{
				todoItems.Add(ToDoItemConverter.FromEntityToModel(item));
			} 

			return Ok(todoItems);
		}

		/// <summary>
		/// Gets all todo items (including items that are done)
		/// </summary>
		/// <returns>The all todo item.</returns>
		[HttpGet("GetAllTodoItems")]
		public IActionResult GetAllTodoItems()
		{
			List<ToDoItemModel> todoItems = new List<ToDoItemModel>();

			foreach (var item in dbContext.ToDoItems)
			{
				todoItems.Add(ToDoItemConverter.FromEntityToModel(item));
			}

			return Ok(todoItems);
		}

		[HttpGet("GetDoneTodoItems")]
		public IActionResult GetDoneTodoItems()
		{
			List<ToDoItemModel> todoItems = new List<ToDoItemModel>();

			foreach (var item in dbContext.ToDoItems.Where(n => n.IsDone))
			{
				todoItems.Add(ToDoItemConverter.FromEntityToModel(item));
			}

			return Ok(todoItems);
		}

		[HttpPost("AddNewTodoItem")]
		public IActionResult AddNewTodoItem([FromBody]ToDoItemModel item)
		{
			try
			{
				if (item == null || !ModelState.IsValid)
				{
					return BadRequest("Invalid State");
				}

				var savedItem = dbContext.ToDoItems.Add(ToDoItemConverter.FromModelToEntity(item));
				dbContext.SaveChanges();
				var retItem = ToDoItemConverter.FromEntityToModel(savedItem.Entity);

				return Ok(retItem);
			}
			catch (Exception e)
			{
				logger.LogWarning($"{nameof(AddNewTodoItem)} failed, {e?.GetType()}, msg: {e?.Message}");
				return BadRequest("Error while creating");
			}
		}

		[HttpPost("TodoItemDone")]
		public IActionResult TodoItemDone([FromBody]Guid doneTodoGuid)
		{
			var todoItemToUpdate = dbContext.ToDoItems.Where(n => n.Id == doneTodoGuid).FirstOrDefault();

			if(todoItemToUpdate != null)
			{
				todoItemToUpdate.IsDone = true;
				todoItemToUpdate.FinishedDate = DateTime.UtcNow;

				dbContext.SaveChanges();
				return Ok(ToDoItemConverter.FromEntityToModel(todoItemToUpdate));
			}

			return BadRequest($"In {nameof(TodoItemDone)}, error setting IsDone for {doneTodoGuid}");
		}
	}
}