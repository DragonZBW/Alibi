using System;

namespace Alibi
{
      class Program
      {
            static void Main(string[] args)
            {
                  // File input
                  if (args.Length > 0)
                  {
                        // TODO: File input
                  }

                  // Console input
                  while (true)
                  {
                        // Prompt
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.Write("alibi > ");

                        // Reset color for user's text
                        Console.ResetColor();

                        // Get user input
                        var input = Console.ReadLine();

                        // Stop prompting if the user enters a blank line
                        if(string.IsNullOrWhiteSpace(input))
                        {
                              break;
                        }

                        // TODO: Interpret user input
                        Lexer lexer = new Lexer();
                        var tokens = lexer.Lex(input);
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        foreach (var token in tokens)
                        {
                              Console.WriteLine(token.Type);
                        }

                        Parser parser = new Parser(tokens);
                        var root = parser.Parse();
                        root.WriteTo(Console.Out);
                  }
            }
      }
}
