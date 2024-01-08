using NSW.Data;
using NSW.Data.DTO.Response;
using NSW.Repositories.Interfaces;
using NSW.Services.Interfaces;

namespace NSW.Services
{
    public class LabelTextService : BaseService, ILabelTextService
    {
        private readonly ILabelTextRepository _labelTextRespository;

        public LabelTextService(ILabelTextRepository labelTextRespository)
        {
            _labelTextRespository = labelTextRespository;
        }

        public IList<LabelText> GetAll() => _labelTextRespository.GetAll();

        public LabelText? GetById(int id) => _labelTextRespository.GetById(id);
        public LabelText? GetByIdentifier(string identifier) => _labelTextRespository.GetByIdentifier(identifier);


        public LabelText Insert(LabelText entity) => _labelTextRespository.Insert(entity);

        public LabelText Modify(LabelText entity) => _labelTextRespository.Modify(entity);

        public void Delete(LabelText entity) => _labelTextRespository.Delete(entity);

        public IDictionary<string, string> GetListOfGroupedLabels(string groupIdentifier) => _labelTextRespository.GetListOfGroupedLabels(groupIdentifier);
        public IDictionary<string, LabelTextDictionaryItemResponse> GetListOfGroupedLabelsAllLanguages(string groupIdentifier) => _labelTextRespository.GetListOfGroupedLabelsAllLanguages(groupIdentifier);

        public string GetTextWithPreferenceByIdentifier(string identifier) => _labelTextRespository.GetTextWithPreferenceByIdentifier(identifier);

        public string GetTextByIdentifier(string identifier) => _labelTextRespository.GetTextByIdentifier(identifier);
    }
}
