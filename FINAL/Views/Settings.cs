///////////////////////
// Views/Settings.cs //
///////////////////////

// install dependencies
using Terminal.Gui;
using System.IO;

namespace FINAL
{

    public partial class Settings
    {
        public bool spellCheck;
        public int maxLength;
        public bool changeAcronyms;
        public bool grammarCheck;

        public Settings()
        {
            FetchSettings();

            InitializeComponent();

            saveBtn.Clicked += () => { SaveSettings(); };
            viewDictBtn.Clicked += () => { Application.Run(new Dictionary()); };
            saveBtn.Clicked += () => { Application.Run(new MainView()); };
        }

        protected void SaveSettings()
        {
            spellCheck = spellCheckCbx.Checked;
            changeAcronyms = changeAcronymsCbx.Checked;
            grammarCheck = grammarCheckCbx.Checked;

            if (!int.TryParse(maxLengthTxt.Text.ToString(), out maxLength))
            {
                MessageBox.ErrorQuery("Error", "The input for max length must be a whole number\nFor no max length, enter 0", "Ok");

                return;
            }

            string settings = "Spell " + spellCheck + "\n"
                + "Lengt " + maxLength + "\n"
                + "Acron " + changeAcronyms + "\n"
                + "Grama " + grammarCheck + "\n"
                + "TextC"
                + "Backg"
                ;

            string path = "Resources/Settings.txt";
            File.WriteAllText(path, settings);

            MessageBox.Query("Saved", "Settings saved successfully", "Ok");
        }

        protected void FetchSettings()
        {
            string path = "Resources/Settings.txt";
            string[] settings = File.ReadAllLines(path);

            spellCheck = bool.Parse(settings[0].Substring(6));
            maxLength = int.Parse(settings[1].Substring(6));
            changeAcronyms = bool.Parse(settings[2].Substring(6));
            grammarCheck = bool.Parse(settings[3].Substring(6));
        }
    }
}
