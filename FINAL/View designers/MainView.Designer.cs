/////////////////////////////////////////
// View designers/MainView.Designer.cs //
/////////////////////////////////////////

// install dependencies
using System;
using Terminal.Gui;

namespace FINAL
{
    public partial class MainView : MasterView
    {
        protected Button settingsBtn = new Button();
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

            settingsBtn.Text = "Settings";
            settingsBtn.X = 1;
            settingsBtn.Y = 20;

            spellBtn.Text = "Spell check";
            spellBtn.X = settingsBtn.X + 13;
            spellBtn.Y = settingsBtn.Y;

            Add(settingsBtn, spellBtn);
        }
    }
}
