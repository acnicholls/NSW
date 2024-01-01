using Microsoft.AspNetCore.Http;

namespace NSW.Data.Validation.Interfaces
{
	public interface IPostalCodeTask
	{
		void StartBackgroundPostalCodeWorker(ApiAccessType accessType);
		void StartBackgroundPostalCodeWorker(HttpContext context, ApiAccessType accessType);
	}
}
