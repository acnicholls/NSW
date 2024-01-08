namespace NSW.Data.DTO.Response
{
	public class PostResponse
	{
		public int Id { get; set; }
		public int CategoryId { get; set; }
		public string Title { get; set; }
		public string Description { get; set; }
		public decimal Price { get; set; }
		public DateTime Expiry { get; set; }
		public string Status { get; set; }

		public PostUserResponse PostUser { get; set; }

		public bool IsActive { get; set; }
	}
}
