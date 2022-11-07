// install dependencies
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Collections;

class Graph
{
    protected struct Connection
    {
        // node
        public string word;
        // edge to parent
        public int weight;
    }

    protected static LevenshteinDistance leven = new LevenshteinDistance();
    // each node has its own word, and a list of connections to store edge weights to children
    protected static Dictionary<string, List<Connection>> tree = new Dictionary<string, List<Connection>>();
    protected static string[] words = File.ReadAllLines("/Users/lewisdrake/Desktop/WordLists/V2/TrueWords.txt");
    protected static int[] weights = new int[words.Length];
    protected string root;

    public Graph(string root)
    {
        weights = leven.CalculateAll(root, words);

        // create the tree where every word is a node
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
        string exists = CheckEdges(parent, weight);
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

            tree.Add(thisConnection.word, new List<Connection>());
            tree[parent][tree[parent].Count] = thisConnection;
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

    // input 1 when ReturnClosest is called in program
    public List<string> ReturnClosest(int maxWeight)
    {
        List<string> words = new List<string>();
        bool exists = false;

        for (int a = 0; a < tree[root].Count; a++)
        {
            if (tree[root][a].weight == maxWeight)
            {
                exists = true;
                // creates a new "sub-tree" and traverse it to return all children and sub-children
                words = BFS(tree[root][a].word);
            }

            // if the maxWeight doesn't exist from root
            // then increase maxWeight and try again
            // this will increase the allowed distance from root -> other words
            if (!exists)
            {
                maxWeight++;
                ReturnClosest(maxWeight);
            }
        }

        return words;
    }

    // used as a traversal method rather than searching
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
                // if a node is unvisited, then add it to the queue to be vistsed soon
                if (visited.Contains(tree[word][a].word) == false)
                {
                    queue.Enqueue(tree[word][a].word);
                }
            }
        }

        return words;
    }
}