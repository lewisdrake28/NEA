// // install dependencies
using System;
using System.Linq;
using System.ComponentModel.DataAnnotations;

class LevenshteinDistance
{
    // int distance;
    // [Required]
    // public string? a { get; set; }
    // [Required]
    // public string? b { get; set; }

    public LevenshteinDistance()
    {
        // distance = 0;

        // b = a.Substring(0, 2);
    }

    public async Task<int> Calculate(string a, string b)
    {
        if(a == null || b == null)
        {
            return 0;
        }

        int cost;

        if (a.Length == 0)
        {
            return b.Length;
        }
        else if (b.Length == 0)
        {
            return a.Length;
        }

        int[,] matrix = new int[a.Length + 1, b.Length + 1];

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
                if (a[c - 1] == b[d - 1])
                {
                    cost = 0;
                }
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

        int distance = matrix[a.Length, b.Length];
        return distance;
    }
}