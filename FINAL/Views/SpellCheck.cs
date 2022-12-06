/////////////////////////
// Views/SpellCheck.cs //
/////////////////////////

// install dependencies
using Terminal.Gui;

namespace FINAL
{

    public partial class SpellCheck
    {
        protected int firstIndex;

        protected string? error;
        protected List<string> falseWords = new List<string>();
        protected List<string> words = new List<string>();

        public SpellCheck(string[] flaseWordsIn, string[] wordsIn)
        {
            falseWords = flaseWordsIn.ToList();
            words = wordsIn.ToList();

            DisplayWords();

            InitializeComponent();

            lookupBtn.Clicked += () => { error = lookupTxt.Text.ToString(); };

            ignoreBtn.Clicked += () =>
            {
                error = lookupTxt.Text.ToString();

                if (error != null)
                {
                    falseWords.Remove(error);

                    DisplayWords();

                    MessageBox.Query("Ignored", error + " has been ignored as an error", "Ok");
                }
            };

            acronymBtn.Clicked += () => { ReaplceAcronyms(); };

            bktree.Clicked += () =>
            {
                BKTree.BkTree tree = new BKTree.BkTree("this");
                List<string> words = tree.ReturnClosest(1);
                string text = "";

                for (int a = 0; a < words.Count; a++)
                {
                    text += words[a] + "\n";
                }

                MessageBox.Query(words.Count.ToString(), text, "OK");
            };
        }

        protected void DisplayWords()
        {
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
                if (thisIndex < falseWords.Count)
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
                int starIndex = acronyms[a].IndexOf('*');
                shortAcr[a] = acronyms[a].Substring(0, starIndex - 1);
                fullAcr[a] = acronyms[a].Substring(starIndex + 1);
            }

            for (int a = 0; a < words.Count; a++)
            {
                for (int b = 0; b < acronyms.Length; b++)
                {
                    if (words[a] == shortAcr[b])
                    {
                        words[a] = fullAcr[b];
                    }
                }
            }
        }
    }
}
