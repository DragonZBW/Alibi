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

                        // Lex text to tokens
                        Lexer lexer = new Lexer();
                        var tokens = lexer.Lex(input);
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        foreach (var token in tokens)
                        {
                              Console.WriteLine(token.Type);
                        }

                        // Parse tokens to tree
                        Parser parser = new Parser(tokens);
                        var root = parser.Parse();
                        Console.WriteLine();
                        root.WriteTo(Console.Out);

                        // Interpret tree to value
                        Interpreter interpreter = new Interpreter(root);
                        Console.WriteLine();
                        Console.WriteLine(interpreter.Interpret());
                  }
            }
      }
}
