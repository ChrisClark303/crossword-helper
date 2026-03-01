using CrosswordHelper.Data.Models;

public interface ICrosswordDataCache
{
    IndicatorWord[] Anagrams { get; }
    IndicatorWord[] Containers { get; }
    IndicatorWord[] HiddenWords { get; }
    IndicatorWord[] Homophones { get; }
    IndicatorWord[] LetterSelections { get; }
    IndicatorWord[] Removals { get; }
    IndicatorWord[] Reversals { get; }
    IndicatorWord[] Substitutions { get; }
    UsualSuspect[] UsualSuspects { get; }

    void SetAnagrams(IndicatorWord[] anagrams);
    void SetContainers(IndicatorWord[] containers);
    void SetHiddenWords(IndicatorWord[] hiddenWords);
    void SetHomophones(IndicatorWord[] homophones);
    void SetLetterSelections(IndicatorWord[] letterSelections);
    void SetRemovals(IndicatorWord[] removals);
    void SetReversals(IndicatorWord[] reversals);
    void SetSubstitutions(IndicatorWord[] substitutions);
    void SetUsualSuspects(UsualSuspect[] usualSuspects);
}
