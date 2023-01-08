/////////////////////////////////////////
// View designers/Settings.Designer.cs //
/////////////////////////////////////////

// install dependencies
using Terminal.Gui;

// suppress warnings
# pragma warning disable

namespace Views
{
    public partial class Settings : MasterView
    {
        protected Button viewDictBtn = new Button();
        protected Button saveBtn = new Button();
        protected Button resetBtn = new Button();

        protected CheckBox spellCheckCbx = new CheckBox();
        protected CheckBox changeAcronymsCbx = new CheckBox();
        protected CheckBox grammarCheckCbx = new CheckBox();

        protected ColorPicker backgroundPick = new ColorPicker();
        protected ColorPicker foregroundPick = new ColorPicker();

        protected Label spellCheckLab = new Label();
        protected Label viewDictLab = new Label();
        protected Label maxLengthLab = new Label();
        protected Label changeAcryonymsLab = new Label();
        protected Label grammarCheckLab = new Label();

        protected TextField maxLengthTxt = new TextField();

        protected void InitializeComponent()
        {
            // build labels
            spellCheckLab.Text = "Spell check";
            spellCheckLab.X = 2;
            spellCheckLab.Y = 2;

            viewDictLab.Text = "View dictionary";
            viewDictLab.X = spellCheckLab.X;
            viewDictLab.Y = spellCheckLab.Y + 2;

            maxLengthLab.Text = "Maximum length                For no max length, enter \"0\"";
            maxLengthLab.X = spellCheckLab.X;
            maxLengthLab.Y = viewDictLab.Y + 2;

            changeAcryonymsLab.Text = "Change acronyms";
            changeAcryonymsLab.X = spellCheckLab.X;
            changeAcryonymsLab.Y = maxLengthLab.Y + 2;

            grammarCheckLab.Text = "Grammar check";
            grammarCheckLab.X = spellCheckLab.X;
            grammarCheckLab.Y = changeAcryonymsLab.Y + 2;

            Add(spellCheckLab, viewDictLab, maxLengthLab, changeAcryonymsLab, grammarCheckLab);

            // build check boxes
            spellCheckCbx.X = spellCheckLab.X + 20;
            spellCheckCbx.Y = spellCheckLab.Y;
            spellCheckCbx.Checked = spellCheck;

            changeAcronymsCbx.X = spellCheckCbx.X;
            changeAcronymsCbx.Y = changeAcryonymsLab.Y;
            changeAcronymsCbx.Checked = changeAcronyms;

            grammarCheckCbx.X = spellCheckCbx.X;
            grammarCheckCbx.Y = grammarCheckLab.Y;
            grammarCheckCbx.Checked = grammarCheck;

            Add(spellCheckCbx, changeAcronymsCbx, grammarCheckCbx);

            // build buttons
            viewDictBtn.Text = "View";
            viewDictBtn.X = spellCheckCbx.X;
            viewDictBtn.Y = viewDictLab.Y;

            saveBtn.Text = "_Save";
            saveBtn.X = maxLengthLab.X;
            saveBtn.Y = grammarCheckLab.Y + 2;
            saveBtn.IsDefault = true;

            resetBtn.X = maxLengthLab.X;
            resetBtn.Y = saveBtn.Y + 2;
            resetBtn.Text = "Reset to default settings";

            Add(viewDictBtn, saveBtn, resetBtn);

            // build text fields
            maxLengthTxt.X = spellCheckCbx.X;
            maxLengthTxt.Y = maxLengthLab.Y;
            maxLengthTxt.Width = 8;
            maxLengthTxt.Text = maxLength.ToString();

            Add(maxLengthTxt);

            // build color elements
            backgroundPick.X = maxLengthLab.X;
            backgroundPick.Y = resetBtn.Y + 2;
            backgroundPick.Text = "Background colour";
            backgroundPick.SelectedColor = background;

            foregroundPick.X = 38;
            foregroundPick.Y = backgroundPick.Y;
            foregroundPick.Text = "Foreground colour";
            foregroundPick.SelectedColor = foreground;

            Add(backgroundPick, foregroundPick);
        }
    }
}
