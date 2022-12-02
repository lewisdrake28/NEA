// install dependencies
using Terminal.Gui;
using System.IO;
using System.Collections.Generic;
using DefineWords;
using System.Linq;

namespace FINAL
{

    public partial class Dictionary
    {
        public string[] words;
        protected string? currentLookup;

        protected int firstIndex;
        protected int currentFilter;

        protected List<string> toDisplay = new List<string>();

        public Dictionary()
        {
            words = FetchAllWords();
            firstIndex = 0;
            currentFilter = filterRad.SelectedItem;

            UpdateWords();

            InitializeComponent();

            lookupBtn.Clicked += () => { Lookup(0); };

            nextBtn.Clicked += () => { NextPage(); };

            prevBtn.Clicked += () => { PrevPage(); };

            defineBtn.Clicked += () =>
            {
                currentLookup = lookupTxt.Text.ToString();

                if (currentLookup != null)
                {
                    DefineThis(currentLookup);
                }
                else
                {
                    MessageBox.ErrorQuery("Error", "There is no word to define\nEnter a value in the lookup field", "Ok");
                }
            };

            addBtn.Clicked += () =>
            {
                currentLookup = lookupTxt.Text.ToString();

                if (currentLookup != null && !currentLookup.Contains(" "))
                {
                    AddThis(currentLookup);
                    UpdateWords();
                }
                else
                {
                    MessageBox.ErrorQuery("Error", "There is no word to add to the dictionary\nEnter a value in the lookup field", "Ok");
                }
            };

            removeBtn.Clicked += () =>
            {
                currentLookup = lookupTxt.Text.ToString();
                if (currentLookup != null)
                {
                    RemoveThis(currentLookup);
                    UpdateWords();
                }
                else
                {
                    MessageBox.ErrorQuery("Error", "There is no word to remove fomr the dictionary\nEnter a value in the lookup field", "Ok");
                }
            };

            // filterBtn.Clicked += () => MessageBox.Query("title", filterRad.SelectedItem.ToString(), "Ok"); //FilterList(filterRad.SelectedItem); 
            filterBtn.Clicked += () =>
            {
                currentFilter = filterRad.SelectedItem;
                UpdateWords();
            };
        }

        protected string[] FetchAllWords()
        {
            string defPath = "Resources/TrueWords.txt";
            string addedPath = "Resources/AddedWords.txt";

            List<string> words = File.ReadAllLines(defPath).ToList();
            List<string> addedWords = File.ReadAllLines(addedPath).ToList();
            words.AddRange(addedWords);
            words.Sort();
            string[] values = words.ToArray();

            return values;
        }

        protected string[] FetchWords(int num)
        {
            string path;

            if (num == 1)
            {
                path = "Resources/TrueWords.txt";
            }
            else
            {
                path = "Resources/AddedWords.txt";
            }

            words = File.ReadAllLines(path);
            return words;
        }

        protected void NextPage()
        {
            firstIndex += 20;
            if ((toDisplay == null && firstIndex > words.Length) || (toDisplay != null && firstIndex > toDisplay.Count))
            {
                firstIndex -= 20;
            }

            Lookup(firstIndex);
        }

        protected void PrevPage()
        {
            if (firstIndex != 0)
            {
                firstIndex = firstIndex - 20;
            }

            Lookup(firstIndex);
        }

        protected void Lookup(int firstIndexIn)
        {
            firstIndex = firstIndexIn;
            currentLookup = lookupTxt.Text.ToString();

            UpdateWords();
        }

        protected void UpdateWords()
        {
            if (currentFilter == 0)
            {
                FetchAllWords();
            }
            else
            {
                FetchWords(currentFilter);
            }

            foreach (var a in wordLabs)
            {
                Remove(a);
            }
            toDisplay.Clear();

            if (currentLookup == null)
            {
                toDisplay = words.ToList();
            }

            else
            {
                for (int a = 0; a < words.Length; a++)
                {
                    if (words[a].StartsWith(currentLookup))
                    {
                        toDisplay.Add(words[a]);
                    }
                }
            }

            DisplayLabels(toDisplay);
        }

        protected void DisplayLabels(List<string> toDisplay)
        {
            int yPos = 2;
            int count = 0;
            int thisIndex = firstIndex;
            int lastIndex = firstIndex + 20;

            while (thisIndex < lastIndex)
            {
                if (thisIndex < toDisplay.Count)
                {
                    wordLabs[count] = new Label();
                    wordLabs[count].X = 2;
                    wordLabs[count].Y = yPos;
                    wordLabs[count].Text = toDisplay[thisIndex];

                    Add(wordLabs[count]);
                }
                else
                {
                    break;
                }

                count++;
                yPos++;
                thisIndex++;
            }
        }

        protected void DefineThis(string word)
        {
            try
            {
                List<string> definitions = DefineWords.Program.DefineWord(word);

                string definitionstring = "";
                for (int a = 0; a < definitions.Count; a++)
                {
                    definitionstring += definitions[a];
                    definitionstring += "\n";
                }

                MessageBox.Query("Definitions of " + word, definitionstring, "Ok");
            }
            catch (Exception e)
            {
                MessageBox.ErrorQuery("Error", "Word could not be defined\n" + e.Message, "Ok");
            }
        }

        protected void AddThis(string word)
        {
            string path = "Resources/AddedWords.txt";
            string[] words = File.ReadAllLines(path);

            if (!words.Contains(word))
            {
                List<string> newWords = new List<string>();

                newWords = words.ToList();
                newWords.Add(word);
                newWords.Sort();

                string[] toWrite = newWords.ToArray();
                File.WriteAllLines(path, toWrite);
            }

            MessageBox.Query("Word added", word + " has been added to the dictionary", "Ok");
        }

        protected void RemoveThis(string word)
        {
            string path = "/Users/lewisdrake/NEA/FINAL/Resources/AddedWords.txt";
            string[] words = File.ReadAllLines(path);

            if (words.Contains(word))
            {
                List<string> newWords = new List<string>();

                newWords = words.ToList();
                newWords.Remove(word);
                newWords.Sort();

                string[] toWrite = newWords.ToArray();
                File.WriteAllLines(path, toWrite);
            }

            MessageBox.Query("Word removed", word + " has been removed from the dictionary", "Ok");
        }

        protected void FilterList(int choice)
        {
            if (choice == 1)
            {
                string path = "Resources/TrueWords.txt";
                string[] words = File.ReadAllLines(path);
            }
            if (choice == 2)
            {
                string path = "Resources/AddedWords.txt";
                string[] words = File.ReadAllLines(path);
            }

            UpdateWords();
        }
    }
}
