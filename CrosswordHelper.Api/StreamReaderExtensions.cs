namespace CrosswordHelper.Api
{
    public static class StreamReaderExtensions
    {
        public static IEnumerable<string> ReadAllLines(this StreamReader reader) 
        {
            string nextLine;
            while ((nextLine = reader.ReadLine()) != null) 
            {
                yield return nextLine;
            }
        }
    }
}
