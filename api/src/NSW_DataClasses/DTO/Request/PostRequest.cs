using System.ComponentModel.DataAnnotations;

namespace NSW.Data.DTO.Request
{
	public class PostRequest
	{
		public int CategoryID { get; set; }
        [MaxLength(50)]
		public string Title { get; set; }
        [MaxLength (4000)]
		public string Description { get; set; }
		public decimal Price { get; set; }
		// TODO: add in image(s) placeholder.  maybe list IFormFile?
	}
}
