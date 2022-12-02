// install dependencies
using System;
using Terminal.Gui;

namespace FINAL
{
    public partial class SpellCheck : MasterView
    {
        protected Button lookupBtn = new Button();
        protected Button ignoreBtn = new Button();
        protected Button userBtn = new Button();
        protected Button acronymBtn = new Button();

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
            lookupLab.X = 50;
            lookupLab.Y = 2;
            lookupLab.Text = "Enter word from left side";

            lookupLab2.X = lookupLab.X;
            lookupLab2.Y = lookupLab.Y + 1;
            lookupLab2.Text = "in the field below. Press";

            lookupLab3.X = lookupLab.X;
            lookupLab3.Y = lookupLab2.Y + 1;
            lookupLab3.Text = "enter, then handle error";

            lookupTxt.X = lookupLab.X;
            lookupTxt.Y = lookupLab3.Y + 2;;
            lookupTxt.Width = 14;

            lookupBtn.X = lookupLab.X;
            lookupBtn.Y = lookupTxt.Y + 2;
            lookupBtn.Text = "Enter";

            Add(lookupLab, lookupLab2, lookupLab3, lookupTxt, lookupTxt, lookupBtn);

            // build ignore error elements
            ignoreBtn.X = lookupLab.X;
            ignoreBtn.Y = lookupBtn.Y + 2;
            ignoreBtn.Text = "Ignore error";

            Add(ignoreBtn);

            // build change error to user word elements
            userLab.X = lookupLab.X;
            userLab.Y = ignoreBtn.Y + 2;
            userLab.Text = "Enter new word";

            userTxt.X = lookupLab.X;
            userTxt.Y = userLab.Y + 2;
            userTxt.Width = 14;

            userBtn.X = lookupLab.X;
            userBtn.Y = userTxt.Y + 2;
            userBtn.Text = "Replace";

            Add(userLab, userTxt, userBtn);

            // build replace acronyms elements 
            acronymBtn.X = lookupLab.X;
            acronymBtn.Y = userBtn.Y + 2;
            acronymBtn.Text = "Replace acronyms";

            Add(acronymBtn);
        }
    }
}

// ingore the error √
// button
// remove from the false words list
//
// DOING THIS !
// change the error to a user specified word x
// label, text field, button
// have another text field and button that user can enter word into
// change the word in the text to the new one
// and remove from the false words list
//
// change the error to one recommended by bk-tree x
// button, query box
// open a query box when a button is pressed
// query will have multiple buttons (if possible) each being a word suggestion
// if not possible, then a new view (like the dictionary) will be called. but will use buttons instead of labels
// and remove from the false words list
//
// change acronyms x
// button
// check the text for all acronyms and change them to the fully expanded version