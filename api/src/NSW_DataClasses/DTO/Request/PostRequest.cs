namespace NSW.Data.DTO.Request
{
	public class PostRequest
	{
		public int CategoryID { get; set; }
		public string Title { get; set; }
		public string Description { get; set; }
		public decimal Price { get; set; }
		// TODO: add in image(s) placeholder.  maybe list IFormFile?
	}
}
