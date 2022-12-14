/////////////////////////////////////////
// View designers/MainView.Designer.cs //
/////////////////////////////////////////

// install dependencies
using Terminal.Gui;

// suppress warnings
# pragma warning disable

namespace Views
{
    public partial class MainView : MasterView
    {
        protected Button spellBtn = new Button();
        protected Button clearBtn = new Button();

        protected Label lengthLab = new Label();

        protected TextView textEntry = new TextView();

        protected void InitializeComponent()
        {
            //
            textEntry.X = 1;
            textEntry.Y = 2;
            textEntry.Width = 76;
            textEntry.Height = 5;
            textEntry.WordWrap = true;

            Add(textEntry);

            //
            spellBtn.Text = "Spell check";
            spellBtn.X = 1;
            spellBtn.Y = 20;

            clearBtn.Text = "Clear text";
            clearBtn.X = spellBtn.X + spellBtn.Text.Length + 6;
            clearBtn.Y = spellBtn.Y;

            Add(spellBtn, clearBtn);
        }
    }
}
