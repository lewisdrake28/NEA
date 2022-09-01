using System;
using System.Threading.Tasks;
using System.Net.Http;

namespace Test_file
{
    public class Program
    {
        static async Task Main(string[] args)
        {
            string word = "test";
            using var client = new HttpClient();
            var content = await client.GetStringAsync("https://api.dictionaryapi.dev/api/v2/entries/en/" + word);
            Console.WriteLine(content);
        }
    }
}


// File.WriteAllText(fileName, content);



// string jsonString = JsonSerializer.Serialize(content);



//public class Word
// {
//     public string? word { get; set; }
//     public string? phonetic { get; set; }
//     public List<Phonetic>? phonetics { get; set; }
//     public List<Meaning>? meanings { get; set; }
//     public License? license { get; set; }
//     public List<SourceUrl>? sourceUrls { get; set; }
// }

// public class Phonetic
// {
//     public string? text { get; set; }
//     public string? audio { get; set; }
//     public string? sourceUrl { get; set; }
//     public License? licenses { get; set; }
// }

// public class Meaning
// {
//     public string? partOfSpeech { get; set; }
//     public List<Definition>? definitions { get; set; }
//     public List<Synonym>? synonyms { get; set; }
//     public List<Antonym>? antonyms { get; set; }
// }

// public class License
// {
//     public string? name { get; set; }
//     public string? url { get; set; }
// }

// public class SourceUrl
// {
//     public string? url { get; set; }
// }

// public class Definition
// {
//     public string? definition { get; set; }
//     public List<Synonym>? synonyms { get; set; }
//     public List<Antonym>? antonyms { get; set; }
//     public string? example { get; set; }
// }

// public class Synonym
// {
//     public string? name { get; set; }
// }

// public class Antonym
// {
//     public string? name { get; set; }
// }