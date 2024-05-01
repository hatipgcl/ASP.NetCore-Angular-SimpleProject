namespace CarAngularProject.Models
{
	public class UpdateCarModel
	{
		public int Id { get; set; }
		public string? Plaque { get; set; }
		public int Year { get; set; }
		public DateTime InspectionDate { get; set; }
		public IFormFile? PhotoPath { get; set; }
	}
}
