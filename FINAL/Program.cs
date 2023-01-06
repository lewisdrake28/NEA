////////////////
// Program.cs //
////////////////

// install dependencies
using FINAL;
using Terminal.Gui;
using BKTree;
using BloomFilter;
using DefineWords;
using Levenshtein;

Application.Init();

try
{
    Settings thisSettings = new Settings();

    // string[] falseWords = new string[3];
    // falseWords[0] = "this";
    // falseWords[1] = "is";
    // falseWords[2] = "you";

    // string[] words = new string[3];
    // words[0] = "test";
    // words[1] = "do";
    // words[2] = "you";

    // Application.Run(new SpellCheck(falseWords, words));


    // Application.Run(new MainView());


    string text = "thjis is a tezt";
    Application.Run(new SpellCheck(text));
}
finally
{
    Application.Shutdown();
}
