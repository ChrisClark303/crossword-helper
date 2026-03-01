using CrosswordHelper.Data.Models;
using Microsoft.Extensions.Caching.Memory;

namespace CrosswordHelper.Data.Export
{
    public class CrosswordDataFileBackedCache(IMemoryCache innerCache) : ICrosswordDataCache
    {
        private const string anagramFilePath = "anagrams.json";
        private const string containerFilePath = "containers.json";
        private const string hiddenWordFilePath = "hiddenWords.json";
        private const string homophoneFilePath = "homophones.json";
        private const string letterSelectionFilePath = "letterSelections.json";
        private const string removalFilePath = "removals.json";
        private const string reversalFilePath = "reversals.json";
        private const string substitutionFilePath = "substitutions.json";
        private const string usualSuspectFilePath = "usualSuspects.json";

        public IndicatorWord[] Anagrams => ReadFromCache<IndicatorWord>(anagramFilePath);

        public IndicatorWord[] Containers => ReadFromCache<IndicatorWord>(containerFilePath);

        public IndicatorWord[] HiddenWords => ReadFromCache<IndicatorWord>(hiddenWordFilePath);

        public IndicatorWord[] Homophones => ReadFromCache<IndicatorWord>(homophoneFilePath);

        public IndicatorWord[] LetterSelections => ReadFromCache<IndicatorWord>(letterSelectionFilePath);

        public IndicatorWord[] Removals => ReadFromCache<IndicatorWord>(removalFilePath);

        public IndicatorWord[] Reversals => ReadFromCache<IndicatorWord>(reversalFilePath);

        public IndicatorWord[] Substitutions => ReadFromCache<IndicatorWord>(substitutionFilePath);

        public UsualSuspect[] UsualSuspects => ReadFromCache<UsualSuspect>(usualSuspectFilePath);


        public void SetAnagrams(IndicatorWord[] anagrams)
        {
            WriteToFile(anagramFilePath, anagrams);
        }

        public void SetContainers(IndicatorWord[] containers)
        {
            WriteToFile(containerFilePath, containers);
        }

        public void SetHiddenWords(IndicatorWord[] hiddenWords)
        {
            WriteToFile(hiddenWordFilePath, hiddenWords);
        }

        public void SetHomophones(IndicatorWord[] homophones)
        {
            WriteToFile(homophoneFilePath, homophones);
        }

        public void SetLetterSelections(IndicatorWord[] letterSelections)
        {
            WriteToFile(letterSelectionFilePath, letterSelections);
        }

        public void SetRemovals(IndicatorWord[] removals)
        {
            WriteToFile(removalFilePath, removals);
        }

        public void SetReversals(IndicatorWord[] reversals)
        {
            WriteToFile(reversalFilePath, reversals);
        }

        public void SetSubstitutions(IndicatorWord[] substitutions)
        {
            WriteToFile(substitutionFilePath, substitutions);
        }

        public void SetUsualSuspects(UsualSuspect[] usualSuspects)
        {
            WriteToFile(usualSuspectFilePath, usualSuspects);
        }

        private void WriteToFile<T>(string filePath, T data)
        {
            var json = System.Text.Json.JsonSerializer.Serialize(data);
            File.WriteAllText(filePath, json);
        }

        private T? ReadFromFile<T>(string fileName)
        {
            var filePath = Path.Combine("data", fileName);
            if (!File.Exists(filePath)) return default;
            var json = File.ReadAllText(filePath);
            return System.Text.Json.JsonSerializer.Deserialize<T>(json);
        }

        private T[] ReadFromCache<T>(string cacheName)
        {
            return innerCache.GetOrCreate(cacheName, entry =>
            {
                var fileContents = ReadFromFile<T[]>(cacheName);

                entry.AbsoluteExpirationRelativeToNow = TimeSpan.FromSeconds(30);
                entry.SetValue(fileContents);
                return fileContents;
            }) ?? [];
        }
    }
}
