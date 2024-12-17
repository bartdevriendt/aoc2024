// See https://aka.ms/new-console-template for more information

using AOC2024;
using Spectre.Console.Cli;

var app = new CommandApp();
app.Configure(config =>
{
    config.AddCommand<PuzzleCommand>("puzzle");
});

return app.Run(args);