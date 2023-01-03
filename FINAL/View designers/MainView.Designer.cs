/////////////////////////////////////////
// View designers/MainView.Designer.cs //
/////////////////////////////////////////

// install dependencies
using System;
using Terminal.Gui;
using System.Collections.Generic;

namespace FINAL
{
    public partial class MainView : MasterView
    {
        protected Button spellBtn = new Button();

        protected TextView textEntry = new TextView();

        protected void InitializeComponent()
        {
            textEntry.X = 1;
            textEntry.Y = 2;
            textEntry.Width = 76;
            textEntry.Height = 5;
            textEntry.WordWrap = true;

            Add(textEntry);

            spellBtn.Text = "Spell check";
            spellBtn.X = 1;
            spellBtn.Y = 20;

            Add(spellBtn);
        }
    }
}
