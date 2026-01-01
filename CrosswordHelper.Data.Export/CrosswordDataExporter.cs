using CrosswordHelper.Data.Models;

namespace CrosswordHelper.Data.Export
{
    public class CrosswordDataExporter(ICrosswordHelperRepository repository)
    {
        public async Task<IndicatorWord[]> ExportAnagramIndicators()
        {
            return repository.GetAnagramIndicators()
                .ToArray();
        }

        public async Task<IndicatorWord[]> ExportContainerIndicators()
        {
            return repository.GetContainerIndicators()
                .ToArray();
        }

        public async Task<IndicatorWord[]> ExportHiddenWordIndicators()
        {
            return repository.GetHiddenWordIndicators()
                .ToArray();
        }

        public async Task<IndicatorWord[]> ExportHomophoneIndicators()
        {
            return repository.GetHomophoneIndicators()
                .ToArray();
        }

        public async Task<IndicatorWord[]> ExportLetterSelectionIndicators()
        {
            return repository.GetLetterSelectionIndicators()
                .ToArray();
        }

        public async Task<IndicatorWord[]> ExportRemovalIndicators()
        {
            return repository.GetRemovalIndicators()
                .ToArray();
        }

        public async Task<IndicatorWord[]> ExportReversalIndicators()
        {
            return repository.GetReversalIndicators()
                .ToArray();
        }

        public async Task<IndicatorWord[]> ExportSubstitutionIndicators()
        {
            return repository.GetSubstitutionIndicators()
                .ToArray();
        }

        public async Task<UsualSuspect[]> ExportUsualSuspects()
        {
            return repository.GetUsualSuspects()
                .ToArray();
        }
    }
}
