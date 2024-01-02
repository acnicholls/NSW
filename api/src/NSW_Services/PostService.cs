using NSW.Data;
using NSW.Data.Interfaces;
using NSW.Info.Interfaces;
using NSW.Repositories;
using NSW.Repositories.Interfaces;
using NSW.Services.Interfaces;
using System.Net.Mail;

namespace NSW.Services
{
	public class PostService : IPostService
	{
		private readonly IPostRepository _repository;
		private readonly ILabelTextRepository _labelTextRepository;
		private readonly IEmailService _emailService;
		private readonly IProjectInfo _projectInfo;
		private readonly IUserService _userService;
		private readonly ILog _log;

		public PostService(
			ILog log,
			IPostRepository repository,
			ILabelTextRepository labelTextRepository,
			IEmailService emailService,
			IProjectInfo projectInfo,
			IUserService userService)
		{
			_log = log;
			_repository = repository;
			_labelTextRepository = labelTextRepository;
			_emailService = emailService;
			_projectInfo = projectInfo;
			_userService = userService;
		}

		public void Delete(Post entity) => _repository.Delete(entity);

		public IList<Post> GetAll() => _repository.GetAll();

		public Post? GetById(int id) => _repository.GetById(id);

		public Post? GetByIdentifier(string identifier) => _repository.GetByIdentifier(identifier);

		public Post Insert(Post entity) => _repository.Insert(entity);

		public Post Modify(Post entity) => _repository.Modify(entity);

		/// <summary>
		/// sends an email to the current posts user indicating post expiry
		/// </summary>
		public void SendExpiryEmail(Post post)
		{
			try
			{
				var emailDetails = _labelTextRepository.GetListOfGroupedLabels("ExpiryEmail");
				var email = new MailMessage();
				var thisUser = _userService.GetById(post.UserID);
				if (thisUser != null)
				{
					email.To.Add(thisUser?.Email);
					email.Subject = emailDetails[".Subject"];
					string strBody = emailDetails[".Line1"] + " " + post.Title + "\r\n\r\n";
					strBody += "\r\n";
					strBody += emailDetails[".Line2"];
					strBody += "\r\n\r\n";
					string strLink = _projectInfo.protocol + _projectInfo.webServer + "/Posts/RenewPost.aspx?postID=" + post.ID.ToString();
					strBody += strLink;
					strBody += "\r\n\r\n";
					strBody += emailDetails[".Line3"] + "\r\n";
					strBody += emailDetails[".Line4"];
					email.Body = strBody;
					_emailService.Send(email);
					_repository.SetEmailSent(post);
				}
			}
			catch (Exception x)
			{
				_log.WriteToLog(_projectInfo.ProjectLogType, "PostRepository.SendExpiryEmail", x, LogEnum.Critical);
			}
		}

		public IList<Post> GetByCategoryId(int categoryId) => this._repository.GetByCategoryId(categoryId);
		public IList<Post> GetByUserId(int userId) => this._repository.GetByUserId(userId);
	}
}
