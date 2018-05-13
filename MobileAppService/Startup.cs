using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;

using Swashbuckle.AspNetCore.Swagger;
using ToDoManager.Data;
using ToDoManager.Data.Entities;

namespace ToDoManager.MobileAppService
{
	public class Startup
	{
		public Startup(IHostingEnvironment env)
		{
			var builder = new ConfigurationBuilder()
				.SetBasePath(env.ContentRootPath)
				.AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
				.AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
				.AddEnvironmentVariables();

			Configuration = builder.Build();
		}

		public IConfigurationRoot Configuration { get; }

		// This method gets called by the runtime. Use this method to add services to the container.
		public void ConfigureServices(IServiceCollection services)
		{
			services.AddMvc();

			services.AddDbContext<ToDoDbContext>(
					n => n.UseInMemoryDatabase("InMemoryTestDb")
				);


			services.AddSwaggerGen(c =>
			{
				c.SwaggerDoc("v1", new Info { Title = "My API", Version = "v1" });
			});

			//var dbContext = services.BuildServiceProvider().GetService<ToDoDbContext>();
			//Startup.FillInMemoryDbContext(dbContext);
		}

		// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
		public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
		{
			loggerFactory.AddConsole(Configuration.GetSection("Logging"));
			loggerFactory.AddDebug();

			app.UseMvc();

			app.UseSwagger();
			app.UseSwaggerUI(c =>
			{
				c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
			});

			app.Run(async (context) => context.Response.Redirect("/swagger"));
		}

		/// <summary>
		/// Just for testing! Fills in memory db context
		/// </summary>
		public  static void FillInMemoryDbContext(ToDoDbContext toDoDbContext)
		{
			var newTodo1 = new ToDoItem()
			{
				Created = DateTime.Now.AddMinutes(-353),
				Description = "Do what you have to do",
				Title = "Code new feature - in memory test",
				Id = Guid.NewGuid()
			};

			var todo1Entity = toDoDbContext.ToDoItems.Add(newTodo1);

			var pomodoroItem = new PomodoroItem
			{
				DateTimeInUtc = DateTime.Now.AddMinutes(-12),
				ToDoItem = todo1Entity.Entity,
				LengthInSec = 15 * 60,
			};

			toDoDbContext.PomodoroItems.Add(pomodoroItem);
			toDoDbContext.SaveChanges();

			int aa = toDoDbContext.ToDoItems.ToList().Count;
		}
	}
}
