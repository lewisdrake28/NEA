// install dependencies
using System;
using System.Linq;

// suppress warnings
#pragma warning disable 

namespace Levenshtein
{

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