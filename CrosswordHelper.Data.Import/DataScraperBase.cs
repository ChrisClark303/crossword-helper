using HtmlAgilityPack;
using System.Diagnostics;

namespace CrosswordHelper.Data.Import;

public abstract class DataScraperBase(ICrosswordHelperManagerRepository managerRepository, IHtmlDocumentProvider documentProvider)
{
    protected readonly ICrosswordHelperManagerRepository managerRepository = managerRepository;
    protected readonly IHtmlDocumentProvider documentProvider = documentProvider;
    protected abstract ScrapeType scrapeType { get; }

    public async Task Scrape()
    {
        var words = new List<WordData>();
        var docs = await documentProvider.GetDocuments(scrapeType);
        foreach (var doc in docs)
        {
            ScrapeWords(doc, words);
        }

        AddIndicatorsByType(words, WordType.Anagram, managerRepository.AddAnagramIndictor);
        AddIndicatorsByType(words, WordType.Reversal, managerRepository.AddReversalIndicator);
        AddIndicatorsByType(words, WordType.Removal, managerRepository.AddRemovalIndicator);
        AddIndicatorsByType(words, WordType.Container, managerRepository.AddContainerIndicator);
        AddIndicatorsByType(words, WordType.Hidden, managerRepository.AddHiddenWordIndicator);
        AddIndicatorsByType(words, WordType.UsualSuspect, managerRepository.AddAUsualSuspect);
        AddIndicatorsByType(words, WordType.Homophone, managerRepository.AddHomophoneIndicator);
    }

    protected abstract void ScrapeWords(HtmlDocument doc, List<WordData> words);

    private void AddIndicatorsByType(List<WordData> words, WordType wordType, Action<string, string> dataHandler)
    {
        var indicators = words.Where(w => w.WordType == wordType);
        foreach (var indicator in indicators)
        {
            Debug.WriteLine($"Adding word {indicator.Word} {indicator.Description}");
            dataHandler(indicator.Word!, indicator.Description!);
        }
    }

    private void AddIndicatorsByType(List<WordData> words, WordType wordType, Action<string, string[]> dataHandler)
    {
        var indicators = words.Where(w => w.WordType == wordType);
        foreach (var indicator in indicators)
        {
            if (indicator.Substitutions != null)
            {
                dataHandler(indicator.Word!, indicator.Substitutions);
            }
        }
    }
}
