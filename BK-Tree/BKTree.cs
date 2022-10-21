// // install dependencies
// using System;
// using System.Collections.Generic;
// using System.IO;
// using System.Linq;
// using System.Collections;

class Graph
{
    protected struct Connection
    {
        public string word;
        public int weight;
    }

    protected static LevenshteinDistance leven = new LevenshteinDistance();

    protected static Dictionary<string, List<Connection>> tree = new Dictionary<string, List<Connection>>();

    protected static string[] words = File.ReadAllLines("/Users/lewisdrake/Desktop/WordLists/V2/TrueWords.txt");
    protected static int[] weights = new int[words.Length];

    protected string root;

    public Graph(string root)
    {
        // tree.Add(root);
        weights = leven.CalculateAll(root, words);

        for (int a = 0; a < words.Length; a++)
        {
            if (tree.Count == 0)
            {
                CreateRoot(root);
            }
            else
            {
                if (root != tree.Keys.ElementAt(0))
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
        string exists = CheckEdges(parent, weight);
        if (exists != null)
        {
            int newWeight = leven.Calculate(parent, word);
            CreateNode(word, exists, newWeight);
        }
        else
        {
            Connection thisConnection = new Connection();
            thisConnection.word = word;
            thisConnection.weight = weight;
            tree.Add(thisConnection.word, new List<Connection>());
            tree[parent][tree[parent].Count] = thisConnection;
        }
    }

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

    public List<string> ReturnClosest()
    {
        List<string> words = new List<string>();
        int weight = 1;
        //add case for if weight is not in inital search

        for (int a = 0; a < tree[root].Count; a++)
        {
            if (tree[root][a].weight == weight)
            {
                words = BFS(tree[root][a].word);
            }
        }

        return words;
    }

    protected List<string> BFS(string parent)
    {
        Queue queue = new Queue();
        queue.Enqueue(parent);
        List<string> words = new List<string>();
        List<string> visited = new List<string>();
        string word;

        while (queue.Count != 0)
        {
            word = queue.Dequeue().ToString();
            words.Add(word);
            visited.Add(word);

            for (int a = 0; a < tree[word].Count; a++)
            {
                if (visited.Contains(tree[word][a].word) == false)
                {
                    queue.Enqueue(tree[word][a].word);
                }
            }
        }

        return words;
    }
}