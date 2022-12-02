/////////////////////////////////////////
// View designers/Settings.Designer.cs //
/////////////////////////////////////////

// install dependencies
using System;
using Terminal.Gui;
using System.IO;
using NStack;

namespace FINAL
{
    public partial class Settings : MasterView
    {
        protected Button viewDictBtn = new Button();
        protected Button saveBtn = new Button();
        protected Button homeBtn = new Button();

        protected CheckBox spellCheckCbx = new CheckBox();
        protected CheckBox changeAcronymsCbx = new CheckBox();
        protected CheckBox grammarCheckCbx = new CheckBox();

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

            maxLengthLab.Text = "Maximum length";
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

            homeBtn.Text = "Home";
            homeBtn.X = maxLengthLab.X;
            homeBtn.Y = saveBtn.Y + 2;

            Add(viewDictBtn, saveBtn, homeBtn);

            // build text fields
            maxLengthTxt.X = spellCheckCbx.X;
            maxLengthTxt.Y = maxLengthLab.Y;
            maxLengthTxt.Width = 8;
            maxLengthTxt.Text = maxLength.ToString();

            Add(maxLengthTxt);
        }
    }
}

// spell check - check box
// view dictionary - button that links to new view 
// max length - int text field 
// change acronyms - check box 
// grammar check - check box - EXTENSTION
// save settings - button
