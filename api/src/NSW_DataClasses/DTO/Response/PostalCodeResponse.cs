using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NSW.Data.DTO.Response
{
	public class PostalCodeResponse
	{
		public string Code { get; set; }
		public decimal Longitude { get; set; }
		public decimal Latitude { get; set; }
	}
}
