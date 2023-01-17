///////////////////////
// Views/Settings.cs //
///////////////////////

// install dependencies
using Terminal.Gui;

// suppress warnings
# pragma warning disable

namespace Views
{
    public partial class Settings
    {
        public bool spellCheck;
        public bool changeAcronyms;
        public bool grammarCheck;

        public int maxLength;

        public Color background;
        public Color foreground;

        public Settings()
        {
            FetchSettings();

            InitializeComponent();

            saveBtn.Clicked += () =>
            {
                SaveSettings();
            };

            viewDictBtn.Clicked += () =>
            {
                Application.Run(new Dictionary());
            };

            resetBtn.Clicked += () =>
            {
                ResetSettings();
            };

            acronymBtn.Clicked += () =>
            {
                if (shortAcrTxt.Text.ToString().Length != 0 && fullAcrTxt.Text.ToString().Length != 0)
                {
                    string shortAcr = shortAcrTxt.Text.ToString();
                    string fullAcr = fullAcrTxt.Text.ToString();

                    AddAcronym(shortAcr, fullAcr);

                    MessageBox.Query("Acronym added", shortAcr + " added as " + fullAcr, "Ok");
                }
                else
                {
                    MessageBox.ErrorQuery("Error", "Enter text in both short and full acronym fields", "Ok");
                }
            };
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
                + "Foreg " + foregroundPick.SelectedColor + "\n"
                + "Backg " + backgroundPick.SelectedColor + "\n"
                ;

            string path = "Resources/Settings.txt";
            File.WriteAllText(path, settings);

            FetchSettings();
            MessageBox.Query("Saved", "Settings saved successfully", "Ok");
        }

        public void FetchSettings()
        {
            string path = "Resources/Settings.txt";
            string[] settings = File.ReadAllLines(path);

            spellCheck = bool.Parse(settings[0].Substring(6));
            maxLength = int.Parse(settings[1].Substring(6));
            changeAcronyms = bool.Parse(settings[2].Substring(6));
            grammarCheck = bool.Parse(settings[3].Substring(6));
            foreground = GetColour(settings[4].Substring(6));
            background = GetColour(settings[5].Substring(6));

            Colors.Base.Normal = Application.Driver.MakeAttribute(foreground, background);
        }

        protected void ResetSettings()
        {
            spellCheck = true;
            maxLength = 0;
            changeAcronyms = true;
            grammarCheck = false;
            foreground = Color.Gray;
            background = Color.Blue;

            string settings = "Spell " + spellCheck + "\n"
                + "Lengt " + maxLength + "\n"
                + "Acron " + changeAcronyms + "\n"
                + "Grama " + grammarCheck + "\n"
                + "Foreg " + foreground + "\n"
                + "Backg " + background + "\n"
                ;

            string path = "Resources/Settings.txt";
            File.WriteAllText(path, settings);

            FetchSettings();
            MessageBox.Query("Reset", "Settings reset to default successfully", "Ok");
        }

        protected void AddAcronym(string shortAcr, string fullAcr)
        {
            string path = "Resources/Acronyms.txt";
            string text = shortAcr + "*" + fullAcr + "\n";

            File.AppendAllText(path, text);
        }

        protected Color GetColour(string colourStr)
        {
            switch (colourStr)
            {
                case "Black":
                    return Color.Black;
                case "Blue":
                    return Color.Blue;
                case "BrightBlue":
                    return Color.BrightBlue;
                case "BrightCyan":
                    return Color.BrightCyan;
                case "BrightGreen":
                    return Color.BrightGreen;
                case "BrightMagenta":
                    return Color.BrightMagenta;
                case "BrightRed":
                    return Color.BrightRed;
                case "BrightYellow":
                    return Color.BrightYellow;
                case "Brown":
                    return Color.Brown;
                case "Cyan":
                    return Color.Cyan;
                case "DarkGray":
                    return Color.DarkGray;
                case "Gray":
                    return Color.Gray;
                case "Green":
                    return Color.Green;
                case "Magenta":
                    return Color.Magenta;
                case "Red":
                    return Color.Red;
                case "White":
                    return Color.White;
            }

            return Color.Black;
        }
    }
}
