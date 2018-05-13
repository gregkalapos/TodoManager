using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using ToDoManager.Data;
using ToDoManager.MobileAppService.Converters;
using ToDoManager.Model;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ToDoManager.MobileAppService.Controllers
{
	[Route("api/[controller]")]
	public class PomodoroItemsController : Controller
	{
		private ToDoDbContext _dbContext;
		private readonly ILogger logger;

		public PomodoroItemsController(ToDoDbContext dbContext, ILogger<PomodoroItemsController> logger)
		{
			this.logger = logger;
			_dbContext = dbContext;
		}
		//// GET: api/values
		//[HttpGet]
		//public IEnumerable<string> Get()
		//{
		//    return new string[] { "value1", "value2" };
		//}

		// GET api/values/5
		[HttpGet("GetPomodoroForTodo/{id}")]
		public IActionResult GetPomodoroForTodo(Guid id)
		{
			var pomodors = _dbContext.PomodoroItems
									 .Include(n=>n.ToDoItem)
									 .Where(n => n.ToDoItem != null && n.ToDoItem.Id == id);

			List<PomodoroItemModel> retVal = new List<PomodoroItemModel>();

			foreach (var item in pomodors)
			{
				retVal.Add(PomodoroConverter.FromEntityToModel(item));
			}

			return Ok(retVal);
		}

		// POST api/values
		[HttpPost]
		public IActionResult Post([FromBody]PomodoroItemModel newItem)
		{
			logger.LogInformation("New promodo Item added");

			try
			{
				var newEntity = PomodoroConverter.FromModelToEntity(newItem);
				newEntity.ToDoItem = _dbContext.ToDoItems.Where(n => n.Id == newItem.ToDoItemGuid).FirstOrDefault();
				var dbEntity = _dbContext.PomodoroItems.Add(newEntity);
				_dbContext.SaveChanges();

				var savedItemModel = PomodoroConverter.FromEntityToModel(dbEntity.Entity);
				savedItemModel.ToDoItemGuid = newItem.ToDoItemGuid;
				logger.LogInformation($"added new {nameof(PomodoroItemModel)}");
				return Ok(savedItemModel);
			}
			catch(Exception e)
			{
				logger.LogWarning($"Failed adding {nameof(PomodoroItemModel)}, {e?.GetType()}, msg: {e?.Message}");
				return BadRequest($"Failed adding new {nameof(PomodoroItemModel)}");
			}
		}

		// PUT api/values/5
		[HttpPut("{id}")]
		public void Put(int id, [FromBody]string value)
		{
		}

		// DELETE api/values/5
		[HttpDelete("{id}")]
		public void Delete(int id)
		{
		}
	}
}
