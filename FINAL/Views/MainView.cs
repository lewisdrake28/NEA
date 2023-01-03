///////////////////////
// Views/MainView.cs //
///////////////////////

// install dependencies
using Terminal.Gui;
using BloomFilter;

namespace FINAL
{

    public partial class MainView
    {
        public MainView()
        {
            InitializeComponent();

            spellBtn.Clicked += () => { CheckSpelling(); };
        }

        protected void CheckSpelling()
        {
            BloomFilter.BloomFilter bloom = new BloomFilter.BloomFilter();
            string text = textEntry.Text.ToString()!;
            string[] words = text.Split(' ');
            List<string> falseWords = new List<string>();

            for (int a = 0; a < words.Length; a++)
            {
                if (!bloom.Lookup(words[a]))
                {
                    falseWords.Add(words[a]);
                }
            }

            string[] falseWordsArr = falseWords.ToArray();

            Application.Run(new SpellCheck(falseWordsArr, words));
        }
    }
}
