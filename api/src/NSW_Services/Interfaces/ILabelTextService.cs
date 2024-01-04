using NSW.Data;
using NSW.Data.DTO.Response;

namespace NSW.Services.Interfaces
{
	public interface ILabelTextService : IService<LabelText>
	{
		IDictionary<string, string> GetListOfGroupedLabels(string groupIdentifier);
        IDictionary<string, LabelTextDictionaryItemResponse> GetListOfGroupedLabelsAllLanguages(string groupIdentifier);
        string GetTextWithPreferenceByIdentifier(string identifier);
		string GetTextByIdentifier(string identifier);
	}
}
