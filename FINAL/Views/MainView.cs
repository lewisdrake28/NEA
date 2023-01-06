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
            BuildAll();
        }

        // used when coming from the spell check view
        // so the text field will have all the corrected words 
        public MainView(string text)
        {
            BuildAll();

            textEntry.Text = text;
        }

        public void BuildAll()
        {
            InitializeComponent();

            spellBtn.Clicked += () =>
            {
                string text = textEntry.Text.ToString();

                if (text is null)
                {
                    MessageBox.Query("Spell check complete", "No incorrect words", "OK");
                }
                else
                {
                    Application.Run(new SpellCheck(text));
                }
            };

            clearBtn.Clicked += () =>
            {
                textEntry.Text = "";
            };
        }
    }
}
