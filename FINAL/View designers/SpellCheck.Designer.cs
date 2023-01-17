///////////////////////////////////////////
// View designers/SpellCheck.Designer.cs //
///////////////////////////////////////////

// install dependencies
using Terminal.Gui;

// suppress warnings
# pragma warning disable

namespace Views
{
    public partial class SpellCheck : MasterView
    {
        protected Button ignoreBtn = new Button();
        protected Button userBtn = new Button();
        protected Button acronymBtn = new Button();
        protected Button homeBtn = new Button();
        protected Button homeBtn2 = new Button();
        protected Button replaceBtn = new Button();
        protected Button addBtn = new Button();
        protected Button resizeBtn = new Button();

        protected Label lookupLab = new Label();
        protected Label lookupLab2 = new Label();
        protected Label lookupLab3 = new Label();
        protected Label userLab = new Label();
        protected List<Label> wordLabs = new List<Label>();

        protected TextField lookupTxt = new TextField();
        protected TextField userTxt = new TextField();

        protected void InitializeComponent()
        {
            // build lookup elements
            lookupLab.Text = "Enter word from left side";
            lookupLab.X = 50;
            lookupLab.Y = 2;

            lookupLab2.Text = "in the field below.";
            lookupLab2.X = lookupLab.X;
            lookupLab2.Y = lookupLab.Y + 1;

            lookupLab3.Text = "Then handle error";
            lookupLab3.X = lookupLab.X;
            lookupLab3.Y = lookupLab2.Y + 1;

            lookupTxt.X = lookupLab.X;
            lookupTxt.Y = lookupLab3.Y + 1;
            lookupTxt.Width = 14;

            Add(lookupLab, lookupLab2, lookupLab3, lookupTxt, lookupTxt);

            // 
            ignoreBtn.X = lookupLab.X;
            ignoreBtn.Y = lookupTxt.Y + 2;
            ignoreBtn.Text = "Ignore error";

            Add(ignoreBtn);

            // 
            resizeBtn.Text = "Resize text to " + theseSettings.maxLength;
            resizeBtn.X = lookupLab.X;
            resizeBtn.Y = ignoreBtn.Y + 2;

            Add(resizeBtn);

            // build change error elements
            userLab.Text = "Enter new word";
            userLab.X = lookupLab.X;
            userLab.Y = resizeBtn.Y + 2;

            userTxt.X = lookupLab.X;
            userTxt.Y = userLab.Y + 1;
            userTxt.Width = 14;

            userBtn.Text = "Replace";
            userBtn.X = lookupLab.X;
            userBtn.Y = userTxt.Y + 1;

            replaceBtn.Text = "Recommend words";
            replaceBtn.X = lookupLab.X;
            replaceBtn.Y = userBtn.Y + 2;

            Add(userLab, userTxt, userBtn, replaceBtn);

            // 
            acronymBtn.Text = "Replace acronyms";
            acronymBtn.X = lookupLab.X;
            acronymBtn.Y = replaceBtn.Y + 2;

            Add(acronymBtn);

            // 
            addBtn.Text = "Learn spelling";
            addBtn.X = lookupLab.X;
            addBtn.Y = acronymBtn.Y + 2;

            Add(addBtn);

            // 
            homeBtn.Text = "Home";
            homeBtn.X = lookupLab.X;
            homeBtn.Y = addBtn.Y + 2;

            Add(homeBtn);
        }
    }
}
