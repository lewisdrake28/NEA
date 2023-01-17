//////////////////
// Utilities.cs //
//////////////////

// install dependencies
using System.Text;
using System.Net;
using Newtonsoft.Json;

// suppress warnings
# pragma warning disable

namespace Utilities
{
    
    
    ////////////
    // BKTree //
    ////////////

    class BkTree
    {
        protected static LevenshteinDistance leven = new LevenshteinDistance();
        // each node has its own word, and a list of connections to store edge weights to children
        public static Dictionary<string, List<Connection>> tree = new Dictionary<string, List<Connection>>();
        protected static string[] words = File.ReadAllLines("Resources/TrueWords.txt");
        protected static int[] weights = new int[words.Length];
        protected string root;
        protected List<string> visitedWords = new List<string>();

        public BkTree(string rootIn)
        {
            tree.Clear();

            root = rootIn;

            weights = leven.CalculateAll(rootIn, words);

            // create the tree where every word is a node
            for (int a = 0; a < words.Length; a++)
            {
                if (tree.Count == 0)
                {
                    CreateRoot(rootIn);
                }
                else
                {
                    if (words[a] != root)
                    {
                        AddNode(words, weights, a);
                    }
                }
            }
        }

        protected void CreateRoot(string word)
        {
            Connection thisConnection = new Connection();
            thisConnection.word = word;
            // weight from root -> root = i (therefore 0)
            thisConnection.weight = 0;

            List<Connection> connections = new List<Connection>();
            connections.Add(thisConnection);

            tree.Add(word, connections);
        }

        public void AddNode(string[] words, int[] weights, int index)
        {
            Connection thisConnection = new Connection();
            thisConnection.word = words[index];
            thisConnection.weight = weights[index];

            CreateNode(thisConnection.word, root, thisConnection.weight);
        }

        protected void CreateNode(string word, string parent, int weight)
        {
            // checks if an edge from the root node already exists with the same weight
            // if it doesn't, then create node as nornmal from the root
            // if it does, then use that node (that has the same weight) as the new parent
            // due to the nature of BK-trees every edge from a parent must have a distinct weight
            string? exists = CheckEdges(parent, weight);
            if (exists is not null)
            {
                // but weight from the parent might be different to the weight from the node, so recalculate
                int newWeight = leven.Calculate(parent, word);
                CreateNode(word, exists, newWeight);
            }
            else
            {
                Connection thisConnection = new Connection();
                thisConnection.word = word;
                thisConnection.weight = weight;

                if (!tree.ContainsKey(thisConnection.word))
                {
                    tree.Add(thisConnection.word, new List<Connection>());
                    tree[parent].Add(thisConnection);
                }
            }
        }

        // search each edge from a specified parent to see if a particular weight already exists
        protected string CheckEdges(string word, int weight)
        {
            List<int> edges = new List<int>();

            for (int a = 0; a < tree[word].Count; a++)
            {
                edges.Add(tree[word][a].weight);
            }

            for (int a = 0; a < edges.Count; a++)
            {
                if (edges[a] == weight)
                {
                    return tree[word][a].word;
                }
            }

            return null;
        }

        public void ClearTree()
        {
            for (int a = 0; a < tree.Count; a++)
            {
                tree.Remove(words[a]);
            }

            tree.Clear();
        }

        // input 1 when ReturnClosest is called in program
        // public List<string> ReturnClosest(int maxWeight)
        public string[] ReturnClosest(int maxWeight)
        {
            // used as a dummy to store the index of last word used as parent
            Connection dummyConnection = new Connection();
            dummyConnection.word = "";
            dummyConnection.weight = 0;

            string parent = FindParent(maxWeight);
            List<Connection> closeWords = tree[parent];

            while (closeWords.Count < 15)
            {
                if (!(dummyConnection.weight < closeWords.Count))
                {
                    break;
                }

                parent = closeWords[dummyConnection.weight].word;
                closeWords.AddRange(tree[parent]);
                dummyConnection.weight++;
            }

            string[] wordsOut = new string[closeWords.Count];

            for (int a = 0; a < closeWords.Count; a++)
            {
                wordsOut[a] = closeWords[a].word;
            }

            return wordsOut;
        }

        protected string FindParent(int maxWeight)
        {
            bool exists = false;

            while (!exists)
            {
                for (int a = 0; a < tree[root].Count; a++)
                {
                    if (tree[root][a].weight < maxWeight && tree[root][a].word != root)
                    {
                        return tree[root][a].word;
                    }
                }

                if (!exists)
                {
                    maxWeight++;
                }
            }

            return null;
        }
    }


    /////////////////
    // BloomFilter //
    /////////////////

    class BloomFilter
    {
        protected int[] filter = new int[2682975];

        // FIRST TIME USE
        // 
        // constructor used to reset all indexes in the filter to 0
        // then setup the filter to the text file
        public BloomFilter(int num)
        {
            for (int a = 0; a < filter.Length; a++)
            {
                filter[a] = 0;
            }

            SetUp();
        }

        // reads the list of words from text file
        // inserts each word into filter
        // write the filter to a text file
        public void SetUp()
        {
            string[] words = File.ReadAllLines("Resources/TrueWords.txt");

            foreach (var a in words)
            {
                Insert(a);
            }

            WriteFilter();
        }

        // write the filter to a text file
        protected void WriteFilter()
        {
            string filename = "Resources/Filter.txt";
            string[] stringArray = filter.Select(x => x.ToString()).ToArray();
            string result = String.Concat(stringArray);

            File.WriteAllText(filename, result);
        }

        // GENERAL USE
        // 
        // read the filter from a text file and store as an int array
        public BloomFilter()
        {
            string filename = "Resources/Filter.txt";
            string text = File.ReadAllText(filename);

            for (uint a = 0; a < filter.Length; a++)
            {
                filter[a] = text[(int)a] - 48;
            }
        }

        public bool Lookup(string word)
        {
            // there are only two one letter words that are in the set - "a" and "i"
            // however all one letter words return true, so check all one letter words
            if (word.Length == 1)
            {
                return (word == "a" || word == "i");
            }
            else
            {
                // if all the indexes are 1, then the item may be in the set
                // if any of the indexes are 0, then the item is definitely not in the set
                if (filter[Hash1(word)] == 1 && filter[Hash2(word)] == 1 && filter[Hash3(word)] == 1 && filter[Hash4(word)] == 1 && filter[Hash5(word)] == 1 && filter[Hash6(word)] == 1 && filter[Hash7(word)] == 1 && filter[Hash8(word)] == 1 && filter[Hash9(word)] == 1 && filter[Hash10(word)] == 1)
                {
                    return true;
                }
                // if any of the indexes are 0, then do a binary search to check the list of user-added words
                else
                {
                    return BinarySearch(word);
                }
            }
        }

        public bool BinarySearch(string target)
        {
            target = target.ToLower();
            string[] words = File.ReadAllLines("Resources/AddedWords.txt");
            int min = 0;
            int max = words.Length;
            int mid;

            while (min < max)
            {
                mid = (min + max) / 2;

                if (words[mid] == target)
                {
                    return true;
                }
                else if (words[mid].CompareTo(target) < 0)
                {
                    min = mid + 1;
                }
                else 
                {
                    max = mid - 1;
                }
            }

            return false;
        }

        protected void Insert(string word)
        {
            filter[Hash1(word)] = 1;
            filter[Hash2(word)] = 1;
            filter[Hash3(word)] = 1;
            filter[Hash4(word)] = 1;
            filter[Hash5(word)] = 1;
            filter[Hash6(word)] = 1;
            filter[Hash7(word)] = 1;
            filter[Hash8(word)] = 1;
            filter[Hash9(word)] = 1;
            filter[Hash10(word)] = 1;

            WriteFilter();
        }

        // Pearson Hashing
        protected uint Hash1(string word)
        {
            uint hash = 0;
            byte[] bytes = Encoding.UTF8.GetBytes(word);
            byte[] nums = { 114, 177, 249, 4, 222, 117, 190, 121, 130, 78, 53, 196, 255, 208, 5, 116, 221, 27, 144, 41, 252, 33, 170, 231, 62, 89, 235, 111, 174, 57, 105, 132, 204, 205, 151, 135, 90, 211, 37, 36, 66, 164, 40, 253, 108, 153, 98, 156, 67, 214, 35, 6, 38, 42, 162, 148, 28, 18, 254, 79, 61, 155, 3, 25, 184, 189, 152, 143, 84, 216, 87, 44, 75, 138, 191, 158, 243, 230, 1, 242, 91, 113, 26, 171, 245, 197, 22, 68, 187, 161, 218, 246, 97, 16, 234, 193, 73, 125, 101, 80, 226, 195, 139, 49, 9, 212, 224, 63, 72, 13, 100, 233, 104, 163, 207, 247, 137, 199, 136, 160, 203, 141, 250, 71, 200, 167, 129, 32, 19, 145, 238, 43, 142, 237, 198, 64, 76, 103, 182, 149, 2, 74, 107, 124, 88, 54, 157, 159, 51, 52, 102, 201, 7, 77, 180, 110, 109, 228, 85, 99, 11, 239, 169, 12, 8, 209, 165, 168, 248, 34, 82, 112, 140, 56, 120, 185, 55, 58, 31, 179, 47, 213, 86, 206, 194, 69, 127, 147, 123, 20, 219, 166, 29, 223, 220, 83, 70, 225, 188, 60, 21, 251, 240, 10, 119, 122, 23, 131, 96, 178, 227, 126, 173, 14, 17, 176, 192, 15, 46, 65, 215, 134, 232, 115, 106, 181, 175, 48, 202, 154, 150, 81, 50, 183, 39, 229, 92, 24, 217, 45, 172, 95, 128, 93, 133, 244, 210, 186, 118, 59, 30, 241, 146, 236, 94 };

            foreach (var a in bytes)
            {
                hash = nums[((hash ^ a) % (uint)nums.Length)];
            }

            hash %= (uint)filter.Length;
            return hash;
        }

        // Hashing by cyclic polynomial (Buzhash)
        protected uint Hash2(string word)
        {
            uint hash = 1;

            foreach (char a in word)
            {
                hash = CircularShift(hash) ^ CircularShift((byte)a) ^ CircularShift((uint)(byte)a + 1);
            }

            hash %= (uint)filter.Length;
            return hash;
        }

        // Fowler-Noll-Vo (FNV-0) hash 
        protected uint Hash3(string word)
        {
            uint hash = 0;
            byte[] bytes = Encoding.UTF8.GetBytes(word);

            foreach (var a in bytes)
            {
                hash = (hash * 16777619) ^ a;
            }

            hash %= (uint)filter.Length;
            return hash;
        }

        // dijb2
        protected uint Hash4(string word)
        {
            // randomly generated number
            uint hash = 5381;
            byte[] bytes = Encoding.UTF8.GetBytes(word);

            foreach (var b in bytes)
            {
                hash = ((hash << 5) + hash) + 33;
            }

            hash %= (uint)filter.Length;
            return hash;
        }

        // sdbm
        protected uint Hash5(string word)
        {
            uint hash = 0;
            byte[] bytes = Encoding.UTF8.GetBytes(word);

            foreach (var a in bytes)
            {
                hash = 65599 + (hash << 6) + (hash << 16) - hash;
            }

            hash %= (uint)filter.Length;
            return hash;
        }

        // PJW hash function
        protected uint Hash6(string word)
        {
            uint hash = 0;
            uint bits = (sizeof(uint) * 8);
            uint max = (uint)(0xFFFFFFFF) << (int)(bits - (bits / 8));

            for (int a = 0; a < word.Length; a++)
            {
                hash = hash << (int)(bits / 8) + word[a];

                if (max != 0)
                {
                    hash = hash ^ (max >> (int)bits * 3 / 4) & (~max);
                }
            }

            hash %= (uint)filter.Length;
            return hash;
        }

        // Fast-Hash
        protected uint Hash7(string word)
        {
            // randomly generated number
            uint a = unchecked((uint)0x880355f21e6d1965);
            uint hash = 144 ^ ((uint)word.Length * a);
            uint b = 0;

            for (uint c = 0; c < word.Length; c++)
            {
                hash = (hash ^ Mix(c)) * a;
            }

            int d = word.Length & 7;
            d %= word.Length;
            int e = (d - 1) * 8;
            if (e < 0)
            {
                e = 0;
            }
            b ^= (uint)word[d] << d;

            hash ^= Mix(b) * a;
            hash = Mix(hash);

            hash %= (uint)filter.Length;
            return (uint)hash;
        }

        // Rabin Fingerprint
        protected uint Hash8(string word)
        {
            uint hash = word[0];
            uint length = (uint)word.Length;

            for (int a = 1; a < length; a++)
            {
                hash += (uint)Math.Pow(word[a] * length, a);
            }

            hash %= (uint)filter.Length;
            return hash;
        }

        // Fletcher-32
        protected uint Hash9(string word)
        {
            uint a = 0;
            uint b = 0;

            for (int c = 0; c < word.Length; c++)
            {
                a = (a + word[c] % (uint)0xffff);
                b = (a + b) % (uint)0xffff;
            }

            uint hash = (b << 16) | a;

            hash %= (uint)filter.Length;
            return hash;
        }

        // CRC32
        protected uint Hash10(string word)
        {
            // randomly generated number
            uint hash = 0xffffffff;
            uint index;
            byte[] nums = { 114, 177, 249, 4, 222, 117, 190, 121, 130, 78, 53, 196, 255, 208, 5, 116, 221, 27, 144, 41, 252, 33, 170, 231, 62, 89, 235, 111, 174, 57, 105, 132, 204, 205, 151, 135, 90, 211, 37, 36, 66, 164, 40, 253, 108, 153, 98, 156, 67, 214, 35, 6, 38, 42, 162, 148, 28, 18, 254, 79, 61, 155, 3, 25, 184, 189, 152, 143, 84, 216, 87, 44, 75, 138, 191, 158, 243, 230, 1, 242, 91, 113, 26, 171, 245, 197, 22, 68, 187, 161, 218, 246, 97, 16, 234, 193, 73, 125, 101, 80, 226, 195, 139, 49, 9, 212, 224, 63, 72, 13, 100, 233, 104, 163, 207, 247, 137, 199, 136, 160, 203, 141, 250, 71, 200, 167, 129, 32, 19, 145, 238, 43, 142, 237, 198, 64, 76, 103, 182, 149, 2, 74, 107, 124, 88, 54, 157, 159, 51, 52, 102, 201, 7, 77, 180, 110, 109, 228, 85, 99, 11, 239, 169, 12, 8, 209, 165, 168, 248, 34, 82, 112, 140, 56, 120, 185, 55, 58, 31, 179, 47, 213, 86, 206, 194, 69, 127, 147, 123, 20, 219, 166, 29, 223, 220, 83, 70, 225, 188, 60, 21, 251, 240, 10, 119, 122, 23, 131, 96, 178, 227, 126, 173, 14, 17, 176, 192, 15, 46, 65, 215, 134, 232, 115, 106, 181, 175, 48, 202, 154, 150, 81, 50, 183, 39, 229, 92, 24, 217, 45, 172, 95, 128, 93, 133, 244, 210, 186, 118, 59, 30, 241, 146, 236, 94, 0 };

            byte[] bytes = Encoding.UTF8.GetBytes(word);

            foreach (var b in bytes)
            {
                index = ((hash ^ b) & 0xff) % (uint)nums.Length;
                hash = (hash >> 8) ^ nums[index];
            }

            hash ^= 0xffffffff;

            hash %= (uint)filter.Length;
            return hash;
        }

        // helpers
        // for hash2
        protected uint CircularShift(uint a)
        {
            uint b = a << 1 | a >> 31;
            return b;
        }

        // for hash7
        protected uint Mix(uint hash)
        {
            long a = (long)hash;
            a ^= a >> 23;
            // randomly generated number
            a *= 0x2127599bf4325c37;
            a ^= a >> 47;

            hash = (uint)a;
            return (uint)hash;
        }
    }


    ////////////////
    // Connection //
    ////////////////

    public struct Connection
    {
        // node
        public string word;
        // edge to parent
        public int weight;
    }


    /////////////////
    // Levenshtein //
    /////////////////

    class LevenshteinDistance
    {
        protected int distance;

        public LevenshteinDistance()
        {
            distance = 0;
        }

        public int Calculate(string a, string b)
        {
            int cost;

            // if a or b has no length, return the other one
            if (a.Length == 0)
            {
                return b.Length;
            }
            else if (b.Length == 0)
            {
                return a.Length;
            }

            // row and column sizes are (word.Length + 1) because row/column 1 store index values
            int[,] matrix = new int[a.Length + 1, b.Length + 1];

            // store index values in row/coilumn 1, example
            // 0 1 ...
            // 1
            // 2
            // ...
            for (int c = 0; c < matrix.GetLength(0); c++)
            {
                matrix[c, 0] = c;
                for (int d = 0; d < matrix.GetLength(1); d++)
                {
                    matrix[0, d] = d;
                }
            }

            for (int c = 1; c < matrix.GetLength(0); c++)
            {
                for (int d = 1; d < matrix.GetLength(1); d++)
                {
                    // if the values at the index are the same then no change is needed
                    if (a[c - 1] == b[d - 1])
                    {
                        cost = 0;
                    }
                    // if not then a change (substitution, deletion or addition) is needed
                    else
                    {
                        cost = 1;
                    }

                    int value1 = matrix[c - 1, d] + 1;
                    int value2 = matrix[c, d - 1] + 1;
                    int value3 = matrix[c - 1, d - 1] + cost;
                    int[] values = { value1, value2, value3 };
                    matrix[c, d] = values.Min();
                }
            }

            // final cost is stored in the bottom right of the matrix
            distance = matrix[a.Length, b.Length];
            return distance;
        }

        public int[] CalculateAll(string a, string[] words)
        {
            int[] distances = new int[words.Length];

            for (int b = 0; b < words.Length; b++)
            {
                distances[b] = Calculate(a, words[b]);
            }

            return distances;
        }
    }
}


/////////////////
// DefineWords //
/////////////////

namespace DefineWords
{
    // create template to store JSON data retruned by API call
    class Word
    {
        public string? word { get; set; }
        public string? phonetic { get; set; }
        public List<Phonetics>? phonetics { get; set; }
        public List<Meanings>? meanings { get; set; }
        public License? license { get; set; }
        public List<string>? sourceUrls { get; set; }
    }

    class Phonetics
    {
        public string? text { get; set; }
        public string? audio { get; set; }
        public string? sourceUrl { get; set; }
        public License? license { get; set; }
    }

    class License
    {
        public string? name { get; set; }
        public string? url { get; set; }
    }

    class Meanings
    {
        public string? partOfSpeech { get; set; }
        public List<Definitions>? definitions { get; set; }
        public List<string>? synonyms { get; set; }
        public List<string>? antonyms { get; set; }
    }

    class Definitions
    {
        public string? definition { get; set; }
        public List<string>? synonyms { get; set; }
        public List<string>? antonyms { get; set; }
        public string? example { get; set; }
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
            IEnumerable<Word>? iword = JsonConvert.DeserializeObject<IEnumerable<Word>>(responseFromServer);
            Word[] aword = new List<Word>(iword).ToArray();
            Word word = aword[0];

            // makes sure there is only one definition for each word type
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

            // if the definition stored for the word type is null, then store the definiton
            for (int a = 0; a < word.meanings.Count; a++)
            {
                if (definitions[word.meanings[a].partOfSpeech] == "null")
                {
                    definitions[word.meanings[a].partOfSpeech] = word.meanings[a].definitions[0].definition;
                }
            }

            // convert values in dictionary to list
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
