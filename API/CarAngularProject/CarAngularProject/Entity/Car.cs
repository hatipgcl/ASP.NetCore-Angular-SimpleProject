namespace CarAngularProject.Entity
{
	public class Car
	{
		public int Id { get; set; }
		public string? Plaque { get; set; }
		public int Year { get; set; }
		public DateTime InspectionDate { get; set; }
		public string? PhotoPath { get; set; }
	}
}
