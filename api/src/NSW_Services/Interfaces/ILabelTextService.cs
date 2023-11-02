using NSW.Data;

namespace NSW.Services.Interfaces
{
	public interface ILabelTextService : IService<LabelText>
	{
		IDictionary<string, string> GetListOfGroupedLabels(string groupIdentifier);
		string GetTextWithPreferenceByIdentifier(string identifier);
		string GetTextByIdentifier(string identifier);
	}
}
