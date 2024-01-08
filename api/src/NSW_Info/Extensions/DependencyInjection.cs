using NSW.Info.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace NSW.Info.Extensions
{
	public static class DependencyInjection
	{
		public static void RegisterServices(IServiceCollection services)
		{
			services.AddSingleton<IProjectInfo, ProjectInfo>();
			services.AddSingleton<IAppSettings, AppSettings>();
			services.AddSingleton<ILog, Log>();
			services.AddSingleton<IConnectionInfo, ConnectionInfo>();
			services.AddSingleton<IRandomFunctions, RandomFunctions>();
		}
	}
}
