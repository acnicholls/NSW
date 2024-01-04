using NSW.Data;
using NSW.Data.DTO.Response;

namespace NSW.Repositories.Interfaces
{
	public interface ILabelTextRepository : IRepository<LabelText>
	{
		IDictionary<string, string> GetListOfGroupedLabels(string groupIdentifier);
        IDictionary<string, LabelTextDictionaryItemResponse> GetListOfGroupedLabelsAllLanguages(string groupIdentifier);
        //GetListOfGroupedLabelsAllLanguages
        string GetTextWithPreferenceByIdentifier(string identifier);
		string GetTextByIdentifier(string identifier);
	}
}
