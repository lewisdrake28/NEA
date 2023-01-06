/////////////////////////
// Views/SpellCheck.cs //
/////////////////////////

// install dependencies
using Terminal.Gui;
using BKTree;
using BloomFilter;
using System.Text.RegularExpressions;
using NStack;
using System.Linq;

namespace FINAL
{

    public partial class SpellCheck
    {
        protected int firstIndex;

        protected string? error;
        protected List<string> falseWords = new List<string>();
        protected List<string> words = new List<string>();
        protected List<string> ignoredWords = new List<string>();

        public SpellCheck(string textIn)
        {
            words.Clear();
            falseWords.Clear();
            words = textIn.Split(' ').ToList();

            InitializeComponent();
            DisplayWords();

            if (falseWords.Count == 0)
            {
                LeaveMessage();
            }
            else if (falseWords[0] == "")
            {
                LeaveMessage();
            }

            lookupBtn.Clicked += () =>
            {
                error = lookupTxt.Text.ToString();
            };

            ignoreBtn.Clicked += () =>
            {
                error = lookupTxt.Text.ToString();

                if (error is not null)
                {
                    ignoredWords.Add(error);

                    MessageBox.Query("Ignored", error + " has been ignored as an erorr", "Ok");

                    DisplayWords();
                }
            };

            acronymBtn.Clicked += () =>
            {
                ReaplceAcronyms();

                MessageBox.Query("Acronyms replaced", "All acronyms have been replaced", "Ok");

                DisplayWords();
            };

            HomeBtn.Clicked += () =>
            {
                string text = "";

                foreach (var a in words)
                {
                    text += a + " ";
                }

                Application.Run(new MainView(text));
            };

            HomeBtn2.Clicked += () =>
            {
                string text = "";

                foreach (var a in words)
                {
                    text += a + " ";
                }

                Application.Run(new MainView(text));
            };

            userBtn.Clicked += () =>
            {
                error = lookupTxt.Text.ToString();
                string newWord = userTxt.Text.ToString();

                if (newWord is not null)
                {
                    ReplaceUserWord(newWord);
                }

                MessageBox.Query("Replaced", error + " has been replaced to " + newWord, "Ok");

                DisplayWords();
            };

            replaceBtn.Clicked += () =>
            {
                error = lookupTxt.Text.ToString();
                string newWord = "";
                int maxWeight = 1;
                bool loop = true;
                string text;

                if (error is not null && falseWords.Contains(error))
                {
                    BkTree bktree = new BkTree(error);

                    do
                    {
                        string[] stringButtons = bktree.ReturnClosest(maxWeight);
                        ustring[] buttons = new ustring[stringButtons.Length + 1];
                        text = "";

                        for (int a = 0; a < stringButtons.Length; a++)
                        {
                            buttons[a] = (a + 1).ToString();
                            text += (a + 1) + ": " + stringButtons[a] + "   ";
                        }

                        text += "\nPress escape to exit";
                        buttons[buttons.Length - 1] = "More words";

                        int choice = MessageBox.Query("Recommended words: " + buttons.Length, text, buttons);

                        if (choice == -1)
                        {
                            return;
                        }
                        else if (choice == buttons.Length - 1)
                        {
                            maxWeight++;
                        }
                        else
                        {
                            newWord = stringButtons[choice];
                            words[words.IndexOf(error)] = newWord;
                            falseWords.Remove(error);
                            loop = false;

                            MessageBox.Query("Replaced", error + " has been replaced to " + newWord, "Ok");
                        }
                    } while (loop);
                }
                else if (!falseWords.Contains(error))
                {
                    MessageBox.ErrorQuery("Error", error + " is not in the false words list\nMake sure it is spelt correctly and in the list", "Ok");
                }

                DisplayWords();
            };
        }

        protected void CheckSpelling()
        {
            BloomFilter.BloomFilter bloom = new BloomFilter.BloomFilter();
            falseWords.Clear();

            // check all words in the bloom filter 
            if (words.Count != 0)
            {
                for (int a = 0; a < words.Count; a++)
                {
                    if (!bloom.Lookup(words[a]))
                    {
                        falseWords.Add(words[a]);
                    }
                }
            }


            if (falseWords.Count != 0)
            {
                // check all wrong words in the text file of added words 
                foreach (var a in falseWords)
                {
                    if (bloom.BinarySearch(a))
                    {
                        falseWords.Remove(a);
                    }
                }

                // check all wrong words againt the array of ignored words
                for (int a = 0; a < falseWords.Count; a++)
                {
                    for (int b = 0; b < ignoredWords.Count; b++)
                    {
                        if (falseWords[a] == ignoredWords[b])
                        {
                            falseWords.Remove(ignoredWords[b]);
                        }
                    }
                }
            }
        }

        protected void CheckGrammar()
        {
            int a = 0;

            while (a < words.Count)
            {
                string word = words[a];

                Match match = Regex.Match(word, @"[^\w\s]");

                // if a word contains a special character
                if (match.Success && word.Length != 1 && match.Value != "'")
                {
                    string specialChar = word[match.Index].ToString();
                    string part1 = "";
                    string part2 = "";
                    // checks either side of the special character to ensure it is not null
                    //  then stores the values to new strings 
                    if (word[0] != char.Parse(specialChar))
                    {
                        part1 = word.Substring(0, match.Index);
                    }
                    if (word.IndexOf(specialChar) != (word.Length - 1))
                    {
                        part2 = word.Substring(match.Index + 1);
                    }

                    // creates a sub list
                    List<string> postWords = new List<string>();
                    if (match.Index != (words.Count - 1))
                    {
                        postWords = words.GetRange(a + 1, words.Count - a - 1);
                    }
                    // removes all words after & including special character 
                    words.RemoveRange(a, words.Count - a);
                    // adds the parts containing & including special character
                    if (part1 is not null)
                    {
                        words.Add(part1);
                    }
                    words.Add(specialChar);
                    if (part2 is not null)
                    {
                        words.Add(part2);
                    }
                    // concats both lists together
                    words.AddRange(postWords);

                    a += 2;
                }

                a++;
            }

            falseWords.RemoveAll(item => string.IsNullOrWhiteSpace(item));

            for (int b = 0; b < falseWords.Count; b++)
            {
                Match match = Regex.Match(falseWords[b], @"[^\w]");

                if (match.Success)
                {
                    falseWords.Remove(falseWords[b]);
                }
            }
        }

        protected void LeaveMessage()
        {
            RemoveAll();

            Label message1 = new Label();
            message1.Text = "Spell check complete";
            message1.X = Pos.Center();
            message1.Y = Pos.Center() - 1;

            Label message2 = new Label();
            message2.Text = "No errors found".PadLeft(message1.Text.ToString().Length / 2);
            message2.X = message1.X;
            message2.Y = message1.Y + 1;

            HomeBtn.Text = "Home";
            HomeBtn.X = message1.X;
            HomeBtn.Y = message2.Y + 2;

            Add(message1, message2, HomeBtn);
        }

        protected void DisplayWords()
        {
            // move any special characters to own index
            CheckGrammar();
            // find false words 
            CheckSpelling();
            CheckGrammar();

            if (falseWords.Count == 0)
            {
                LeaveMessage();
            }
            else if (falseWords[0] == "")
            {
                LeaveMessage();
            }

            int xPos = 2;
            int yPos = 2;
            int thisIndex = firstIndex;

            foreach (var a in wordLabs)
            {
                Remove(a);
            }
            wordLabs.Clear();

            for (int a = 0; a < 41; a++)
            {
                if (thisIndex < falseWords.Count && ignoredWords.Contains(falseWords[thisIndex]) == false)
                {
                    wordLabs.Add(new Label());
                    wordLabs[a] = new Label();
                    wordLabs[a].X = xPos;
                    wordLabs[a].Y = yPos;
                    wordLabs[a].Text = falseWords[thisIndex];

                    Add(wordLabs[a]);

                    yPos++;
                    thisIndex++;
                }

                if (yPos == 22)
                {
                    xPos = 30;
                    yPos = 2;
                }
            }
        }

        protected void ReaplceAcronyms()
        {
            string path = "Resources/Acronyms.txt";
            string[] acronyms = File.ReadAllLines(path);
            string[] shortAcr = new string[acronyms.Length];
            string[] fullAcr = new string[acronyms.Length];

            for (int a = 0; a < acronyms.Length; a++)
            {
                string[] parts = acronyms[a].Split('*', 2);
                shortAcr[a] = parts[0];
                fullAcr[a] = parts[1];
            }

            for (int a = 0; a < falseWords.Count; a++)
            {
                for (int b = 0; b < acronyms.Length; b++)
                {
                    if (falseWords[a] == shortAcr[b])
                    {
                        falseWords[a] = fullAcr[b];

                        int index = words.IndexOf(shortAcr[b]);
                        words[index] = falseWords[a];
                    }
                }
            }
        }

        protected void ReplaceUserWord(string newWord)
        {
            int index = falseWords.IndexOf(error);

            if (index != -1)
            {
                falseWords[index] = newWord;
                words[index] = newWord;
            }
        }
    }
}
