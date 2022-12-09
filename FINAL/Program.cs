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
    // string[] words = new string[2];
    // words[0] = "test";
    // words[1] = "this";

    // string[] words2 = new string[2];
    // words2[0] = "test";
    // words2[1] = "this";

    // Application.Run(new SpellCheck(words, words2));

    // Application.Run(new Dictionary());

    Settings thisSettings = new Settings();

    Application.Run(new Settings());
}
finally
{
    Application.Shutdown();
}
