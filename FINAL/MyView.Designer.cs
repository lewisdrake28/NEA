using System;
using Terminal.Gui;

namespace FINAL
{

    public partial class MyView : Window
    {

    }

    public class ExampleWindow : Window
    {
        // public TextField usernameText, indexText;

        public ExampleWindow()
        {
            var menu = new MenuBar(new MenuBarItem[] {
                new MenuBarItem("_File", new MenuItem[] {
                    new MenuItem("_Quit", "", () => {
                        Application.RequestStop();
                    })
                })
            });

            // Title = "Example App (Ctrl+Q to quit)";
            var usernameLabel = new Label()
            {
                Text = "Username:",
                Y = Pos.Center(),
                X = Pos.Center() - 20,
            };

            var usernameText = new TextField("")
            {
                X = Pos.Right(usernameLabel) + 1,
                Width = Dim.Fill(),
            };

            var indexLabel = new Label()
            {
                Text = "Index:",
                Y = Pos.Center() + 1,
                X = usernameLabel.X,
            };

            var indexText = new TextField("")
            {
                X = Pos.Right(usernameText) + 1,
                Width = Dim.Fill(),
            };

            var btnLogin = new Button()
            {
                Text = "Login",
                Y = Pos.Bottom(usernameLabel) + 1,
                X = Pos.Center(),
                IsDefault = true,
            };

            // btnLogin.Clicked += () =>
            // {
            //     int index = int.Parse(indexText.Text.ToString());
            //     string text = usernameText.Text.ToString();
            //     MessageBox.Query("Title(?)", "The char at index " + indexText.Text.ToString() + " is: " + text[index], "OK");
            // };

            Add(usernameLabel, usernameText, btnLogin, menu, indexLabel, indexText);
        }
    }
}
