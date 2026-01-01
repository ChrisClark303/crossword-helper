namespace CrosswordHelper.Data.Import
{
    public class AnagramDataImporter
    {
        private readonly ICrosswordHelperManagerRepository _repository;

        public AnagramDataImporter(ICrosswordHelperManagerRepository repository)
        {
            _repository = repository;
        }

        public void Import(string[] anagramIndicators)
        {
            foreach (var anagramIndicator in anagramIndicators)
            {
                _repository.AddAnagramIndictor(anagramIndicator.ToLower());
            }
        }
    }
}