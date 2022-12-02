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
    string[] words = new string[2];
    words[0] = "test";
    words[1] = "this";

    Application.Run(new SpellCheck(words));

    // Application.Run(new Dictionary());
}
finally
{
    Application.Shutdown();
}