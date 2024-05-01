using CarAngularProject.Entity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using System;

namespace CarAngularProject.Controllers
{
	[ApiController]
	[Route("api/car")]
	public class CarController : ControllerBase
	{
		private readonly ApplicationDbContext context;

		public CarController(ApplicationDbContext context)
		{
			this.context = context;
		}

		[HttpGet]
		public async Task<ActionResult<List<Car>>> Get()
		{
			var cars = await context.Cars.ToListAsync();
			return Ok(cars);
		}

		[HttpGet("{id:int}")]
		public async Task<ActionResult<Car>> GetById(int id)
		{
			var car = await context.Cars.FindAsync(id);
			if (car == null)
			{
				return NotFound();
			}
			return Ok(car);
		}

		[HttpPost]
		public async Task<ActionResult<int>> Post(Car car)
		{
			context.Add(car);
			await context.SaveChangesAsync();
			return Ok(car.Id);
		}

		[HttpPut("{id}")]
		public async Task<ActionResult> Put(int id, Car car)
		{
			var carExists = await CarExists(id);
			if (!carExists)
			{
				return NotFound();
			}

			context.Update(car);
			await context.SaveChangesAsync();
			return Ok();
		}

		[HttpDelete("{id:int}")]
		public async Task<ActionResult> Delete(int id)
		{
			var carExists = await CarExists(id);
			if (!carExists)
			{
				return NotFound();
			}

			context.Remove(new Car() { Id = id });
			await context.SaveChangesAsync();
			return Ok();
		}

		private async Task<bool> CarExists(int id)
		{
			return await context.Cars.AnyAsync(p => p.Id == id);
		}
	}

}



/*
     [ApiController]
	[Route("api/car")]
	public class CarController : ControllerBase
	{
		private readonly ApplicationDbContext context;
		private readonly IWebHostEnvironment environment;


		public CarController(ApplicationDbContext context,IWebHostEnvironment environment)
		{
			this.context = context;
			this.environment = environment;
		}

		[HttpGet]
		public async Task<ActionResult<List<Car>>> Get()
		{
			return await context.Cars.ToListAsync();
		}

		[HttpGet("{id:int}", Name = "getbyid")]
		public async Task<ActionResult<Car>> GetById(int id)
		{
			return await context.Cars.FindAsync(id);
		}

		[HttpPost]
		public async Task<ActionResult<int>> Post(CreateCarModel car)
		{
			var newCar = new Car
			{
				Year = car.Year,
				Plaque = car.Plaque,
				InspectionDate = car.InspectionDate
			};

			if (car.PhotoPath != null)
			{
				try
				{
					newCar.PhotoPath = await UploadPhotosAsync(car.PhotoPath);
				}
				catch (Exception ex)
				{
					return BadRequest($"Photo upload failed: {ex.Message}");
				}
			}

			using (var transaction = await context.Database.BeginTransactionAsync())
			{
				try
				{
					context.Add(newCar);
					await context.SaveChangesAsync();
					await transaction.CommitAsync();
					return newCar.Id;
				}
				catch (Exception ex)
				{
					await transaction.RollbackAsync();
					return BadRequest($"Error saving car: {ex.Message}");
				}
			}
		}
		[HttpPut("{id:int}")]
		public async Task<ActionResult> Put(int id, UpdateCarModel car)
		{
			var carExists = await CarExists(id);

			if (!carExists)
			{
				return NotFound();
			}

			var oldCar = await context.Cars.FindAsync(id);
			var oldPhotoPath = oldCar.PhotoPath;

			if (car.PhotoPath != null)
			{
				try
				{
					var newCarPhoto = await UploadPhotosAsync(car.PhotoPath);
					if (newCarPhoto != oldPhotoPath)
					{
						if (!string.IsNullOrEmpty(oldPhotoPath))
						{
							try
							{
								await DeletePhotoAsync(id);
							}
							catch (Exception ex)
							{
								return BadRequest($"Photo upload failed: {ex.Message}");
							}
						}
						oldCar.PhotoPath = newCarPhoto;
					}
				}
				catch (Exception ex)
				{
					return BadRequest($"Photo upload failed: {ex.Message}");
				}
			}

			context.Update(oldCar);
			await context.SaveChangesAsync();
			return NoContent();
		}

		[HttpDelete("{id:int}")]
		public async Task<ActionResult> Delete(int id)
		{
			var carExists = await CarExists(id);

			if (!carExists)
			{
				return NotFound();
			}

			context.Remove(new Car() { Id = id });
			await context.SaveChangesAsync();
			return NoContent();
		}
		private async Task<bool> CarExists(int id)
		{
			return await context.Cars.AnyAsync(p => p.Id == id);
		}
		private async Task<string> UploadPhotosAsync(IFormFile photo)
		{
			var imagesFolderString = $@"Images\Cars";
			var imagesFolder = Path.Combine(environment.WebRootPath, imagesFolderString);
			if (!Directory.Exists(imagesFolder))
				Directory.CreateDirectory(imagesFolder);
			var fileExtension = Path.GetExtension(photo.FileName);
			var uniqueFileName = Guid.NewGuid() + fileExtension;
			var filePath = Path.Combine(imagesFolder, uniqueFileName);
			await photo.CopyToAsync(new FileStream(filePath, FileMode.Create));
			var imagePath = $"/Images/Cars/{uniqueFileName}";
			return imagePath;
		}

		private async Task DeletePhotoAsync(int id)
		{
		  var car = await context.Cars.FindAsync(id);

			var webRootPath = environment.WebRootPath;
			var filePath = webRootPath + car.PhotoPath;
			if (System.IO.File.Exists(filePath))
			{
				try
				{
					GC.Collect();
					GC.WaitForPendingFinalizers();
					System.IO.File.Delete(filePath);
				}
				catch (Exception e)
				{
					//ignored
				}
			}
			car.PhotoPath = null;
			context.Update(car);
			await context.SaveChangesAsync();
		}*/