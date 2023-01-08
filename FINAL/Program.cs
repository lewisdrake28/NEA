////////////////
// Program.cs //
////////////////

// install dependencies
using Views;
using Terminal.Gui;

// suppress warnings
# pragma warning disable

Application.Init();

try
{
    // used to fetch settings from saved file
    Settings thisSettings = new Settings();

    Application.Run(new MainView());
}
finally
{
    Application.Shutdown();
}
