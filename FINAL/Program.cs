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
    Application.Run(new ExampleWindow());
}
finally
{
    Application.Shutdown();
}