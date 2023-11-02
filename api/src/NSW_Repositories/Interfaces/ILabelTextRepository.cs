using NSW.Data;

namespace NSW.Repositories.Interfaces
{
	public interface ILabelTextRepository : IRepository<LabelText>
	{
		IDictionary<string, string> GetListOfGroupedLabels(string groupIdentifier);
		string GetTextWithPreferenceByIdentifier(string identifier);
		string GetTextByIdentifier(string identifier);
	}
}
