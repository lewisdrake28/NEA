// install dependencies
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using Newtonsoft.Json;

namespace Code
{
    class Word
    {
        public string word { get; set; }
        public string phonetic { get; set; }
        public List<Phonetics> phonetics { get; set; }
        public List<Meanings> meanings { get; set; }
        public License license { get; set; }
        public List<string> sourceUrls { get; set; }
    }

    class Phonetics
    {
        public string text { get; set; }
        public string audio { get; set; }
        public string sourceUrl { get; set; }
        public License license { get; set; }
    }

    class License
    {
        public string name { get; set; }
        public string url { get; set; }
    }

    class Meanings
    {
        public string partOfSpeech { get; set; }
        public List<Definitions> definitions { get; set; }
        public List<string> synonyms { get; set; }
        public List<string> antonyms { get; set; }
    }

    class Definitions
    {
        public string definition { get; set; }
        public List<string> synonyms { get; set; }
        public List<string> antonyms { get; set; }
        public string example { get; set; }
    }

    class Program
    {
        public static List<string> DefineWord(string toDefine)
        {
            WebRequest request = WebRequest.Create("https://api.dictionaryapi.dev/api/v2/entries/en/" + toDefine);
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            Stream dataStream = response.GetResponseStream();

            StreamReader reader = new StreamReader(dataStream);
            string responseFromServer = reader.ReadToEnd();
            IEnumerable<Word> iword = JsonConvert.DeserializeObject<IEnumerable<Word>>(responseFromServer);
            Word[] aword = new List<Word>(iword).ToArray();
            Word word = aword[0];

            Dictionary<string, string> definitions = new Dictionary<string, string>()
            {
                {"noun", "null"},
                {"pronoun", "null"},
                {"verb", "null"},
                {"adjective", "null"},
                {"adverb", "null"},
                {"preposition", "null"},
                {"conjunction", "null"},
                {"interjection", "null"},
            };


            for (int a = 0; a < word.meanings.Count; a++)
            {
                if (definitions[word.meanings[a].partOfSpeech] == "null")
                {
                    definitions[word.meanings[a].partOfSpeech] = word.meanings[a].definitions[0].definition;
                }
            }

            List<string> finals = new List<string>();
            foreach (KeyValuePair<string, string> kvPair in definitions)
            {
                if (kvPair.Value != "null")
                {
                    string key = char.ToUpper(kvPair.Key[0]) + kvPair.Key.Substring(1);
                    finals.Add(key + " - " + kvPair.Value);
                }
            }

            return finals;
        }

        static void Main(string[] args)
        {
            string word = Console.ReadLine();
            List<string> definitions = DefineWord(word);
            foreach (var definition in definitions)
            {
                Console.WriteLine(definition);
            }
        }
    }
}
