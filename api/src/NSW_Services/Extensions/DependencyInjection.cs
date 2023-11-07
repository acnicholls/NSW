using Microsoft.Extensions.DependencyInjection;
using NSW.Data;
using NSW.Services.Interfaces;

namespace NSW.Services.Extensions
{
	public static class DependencyInjection
	{
		public static void RegisterServices(IServiceCollection services)
		{
			// TODO: https://stackoverflow.com/questions/26733/getting-all-types-that-implement-an-interface

			// generic interfaces (can be useed for graphql...???
			services.AddScoped<IService<LabelText>, LabelTextService>();
			services.AddScoped<IService<Post>, PostService>();
			services.AddScoped<IService<PostalCode>, PostalCodeService>();
			services.AddScoped<IService<User>, UserService>();
			services.AddScoped<IService<PostCategory>, PostCategoryService>();

			// custom interfaces
			services.AddScoped<ILabelTextService, LabelTextService>();
			services.AddScoped<IUserService, UserService>();
			services.AddScoped<IPostService, PostService>();
			services.AddTransient<IEmailService, EmailService>();
		}
	}
}
