using Microsoft.Extensions.DependencyInjection;
using NSW.GeoCoder.Interfaces;

namespace NSW.GeoCoder.Extensions
{
	public static class DependencyInjection
	{
		public static void RegisterServices(IServiceCollection services)
		{
			services.AddScoped<IDatabase, Database>();
			services.AddScoped<IApiCall, ApiCall>();
		}
	}
}
