///////////////////////
// Classes/BKTree.cs //
///////////////////////

// install dependencies
using System.Collections;
using Levenshtein;

// suppress warnings
#pragma warning disable

namespace BKTree
{
    public struct Connection
    {
        // node
        public string word;
        // edge to parent
        public int weight;
    }

    class BkTree
    {
        protected static LevenshteinDistance leven = new LevenshteinDistance();
        // each node has its own word, and a list of connections to store edge weights to children
        public static Dictionary<string, List<Connection>> tree = new Dictionary<string, List<Connection>>();
        protected static string[] words = File.ReadAllLines("/Users/lewisdrake/Desktop/WordLists/V2/TrueWords.txt");
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
            if (exists != null)
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
}