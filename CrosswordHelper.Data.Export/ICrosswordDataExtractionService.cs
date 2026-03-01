namespace CrosswordHelper.Data.Export
{
    public interface ICrosswordDataExtractionService
    {
        Task ExportCrosswordDataToCache();
    }
}