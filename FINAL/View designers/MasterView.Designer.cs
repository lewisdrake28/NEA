///////////////////////////////////////////
// View designers/MasterView.Designer.cs //
///////////////////////////////////////////

// install dependencies
using System;
using Terminal.Gui;

namespace FINAL
{
    public partial class MasterView : Window
    {
        protected void Build()
        {
            Title = "Spell Checker " + this;

            var menuBar = new MenuBar(new MenuBarItem[] {
                new MenuBarItem("_File", new MenuItem[] {
                    new MenuItem("_Quit", "", () => {
                        // Application.RequestStop();
                        Application.Shutdown();
                    })
                })
            });
            Add(menuBar);
        }
    }
}
