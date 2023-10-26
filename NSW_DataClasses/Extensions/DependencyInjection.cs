using Microsoft.Extensions.DependencyInjection;
using NSW.Data;
using NSW.Data.Interfaces;

namespace NSW.Data.Extensions
{
	public static class DependencyInjecction
	{
		public static void RegisterServices(IServiceCollection services)
		{
			services.AddScoped<IUser, User>();

		}
	}
}
