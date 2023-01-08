///////////////////////////////////////////
// View designers/Dictionary.Designer.cs //
///////////////////////////////////////////

// install dependencies
using Terminal.Gui;
using NStack;

// suppress warnings
# pragma warning disable

namespace Views
{
    public partial class Dictionary : MasterView
    {
        protected Button lookupBtn = new Button();
        protected Button nextBtn = new Button();
        protected Button prevBtn = new Button();
        protected Button defineBtn = new Button();
        protected Button addBtn = new Button();
        protected Button removeBtn = new Button();
        protected Button filterBtn = new Button();

        protected Label[] wordLabs = new Label[101988];

        protected RadioGroup filterRad = new RadioGroup();

        protected TextField lookupTxt = new TextField();

        protected void InitializeComponent()
        {
            // build lookup elements 
            lookupTxt.X = 50;
            lookupTxt.Y = 4;
            lookupTxt.Width = 14;

            lookupBtn.Text = "Lookup";
            lookupBtn.X = lookupTxt.X;
            lookupBtn.Y = lookupTxt.Y + 2;

            Add(lookupBtn, lookupTxt);

            // build navigation elements
            nextBtn.Text = "Next page";
            nextBtn.X = 64;
            nextBtn.Y = 21;

            prevBtn.Text = "Prev page";
            prevBtn.X = nextBtn.X - 14;
            prevBtn.Y = nextBtn.Y;

            Add(prevBtn, nextBtn);

            // build functional buttons
            defineBtn.X = lookupTxt.X;
            defineBtn.Y = lookupBtn.Y + 2;
            defineBtn.Text = "Define word";

            addBtn.X = lookupTxt.X;
            addBtn.Y = defineBtn.Y + 2;
            addBtn.Text = "Add word";

            removeBtn.X = lookupTxt.X;
            removeBtn.Y = addBtn.Y + 2;
            removeBtn.Text = "Remove word";

            Add(defineBtn, addBtn, removeBtn);

            // build fitering elements
            filterRad.X = lookupTxt.X;
            filterRad.Y = removeBtn.Y + 2;
            filterRad.RadioLabels = new ustring[] { "All words", "Default words", "Added words" };

            filterBtn.X = lookupTxt.X;
            filterBtn.Y = filterRad.Y + 4;
            filterBtn.Text = "Filter list";

            Add(filterRad, filterBtn);
        }
    }
}
