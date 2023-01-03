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
                        Application.Shutdown();
                    })
                }),

                new MenuBarItem("_Go", new MenuItem[] {
                    new MenuItem("_Settings", "", () => {
                        Application.Run(new Settings());
                    }),

                    new MenuItem("_Spell Check", "", () => {
                        Application.Run(new MainView());
                    }),

                    new MenuItem("_Dictionary", "", () => {
                        Application.Run(new Dictionary());
                    })
                })
            });
            Add(menuBar);
        }
    }
}
