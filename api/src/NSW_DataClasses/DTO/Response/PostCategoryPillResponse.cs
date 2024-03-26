using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NSW.Data.DTO.Response
{
    public class PostCategoryPillResponse
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int CountOfPosts { get; set; }
    }
}
