using System;


namespace NSW.Data
{
	public class Post
    {
        public int ID { get; set; }
        public int CategoryID { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public DateTime Expiry { get; set; }
        public int UserID { get; set; }
        public string Status { get; set; }
        public bool DeleteFlag { get; set; }

		public User PostUser { get; set; }

        public Post()
        { }

        
        /// <summary>
        /// checks to see if the current post should be active
        /// </summary>
        public bool IsActive
        {
            get
            {
                if (Status == "ACTIVE")
                    return true;
                else
                    return false;
            }
        }
    }
}
