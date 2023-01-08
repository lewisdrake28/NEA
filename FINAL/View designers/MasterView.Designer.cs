///////////////////////////////////////////
// View designers/MasterView.Designer.cs //
///////////////////////////////////////////

// install dependencies
using Terminal.Gui;

// suppress warnings
# pragma warning disable

namespace Views
{
    public partial class MasterView : Window
    {
        public void Build()
        {
            Title = "Spell Checker " + this;

            var menuBar = new MenuBar(new MenuBarItem[] {
                new MenuBarItem("_File", new MenuItem[] {
                    new MenuItem("_Quit", "", () => {
                        Application.Shutdown();
                    }),
                    
                    new MenuItem("_Settings", "", () => {
                        Application.Run(new Settings());
                    }),

                    new MenuItem("_Main View", "", () => {
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
