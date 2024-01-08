using Microsoft.Extensions.DependencyInjection;
using NSW.Data;
using NSW.Repositories.Interfaces;

namespace NSW.Repositories.Extensions
{
	public static class DependencyInjection
	{
		public static void RegisterServices(IServiceCollection services)
		{
			// generic interfaces (can be useed for graphql...???
			services.AddScoped<IRepository<LabelText>, LabelTextRepository>();
			services.AddScoped<IRepository<Post>, PostRepository>();
			services.AddScoped<IRepository<PostalCode>, PostalCodeRepository>();
			services.AddScoped<IRepository<PostCategory>, PostCategoryRepository>();

			// custom interfaces
			services.AddScoped<ILabelTextRepository, LabelTextRepository>();
			services.AddScoped<IUserRepository, UserRepository>();
			services.AddScoped<IPostRepository, PostRepository>();
			services.AddScoped<IPostCategoryRepository, PostCategoryRepository>();
		}
	}
}
