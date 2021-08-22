// This file defines all the grammar for the language/all the parse node types
// Expressions with terms
/*
 * expr    -> term [+-] expr | term
 * term    -> NUM [/*] term | NUM
 */

using System.Collections.Generic;
using System.Reflection;
using System;
using System.IO;
using System.Linq;

namespace Alibi
{
      /// <summary>
      /// Base class for all parse nodes
      /// </summary>
      internal class Node 
      { 
            public IEnumerable<Node> GetChildren()
            {
                  var properties = GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance);

                  foreach (var prop in properties)
                  {
                        if (typeof(Node).IsAssignableFrom(prop.PropertyType))
                        {
                              var child = (Node)prop.GetValue(this);
                              yield return child;
                        }
                        else if (typeof(IEnumerable<Node>).IsAssignableFrom(prop.PropertyType))
                        {
                              var children = (IEnumerable<Node>)prop.GetValue(this);
                              foreach(var child in children)
                                    yield return child;
                        }
                  }
            }

            public void WriteTo(TextWriter writer)
            {
                  PrintFormattedTree(writer, this);
            }

            // Prints the syntax tree for a node with a nice tree format
            private static void PrintFormattedTree(TextWriter writer, Node node, string indent = "", bool isLast = true)
            {
                  // ├─── │ └───

                  var isToConsole = writer == Console.Out;
                  var marker = isLast ? "└── " : "├── ";

                  writer.Write(indent);
                  if (isToConsole)
                        Console.ForegroundColor = ConsoleColor.DarkGray;
                        
                  writer.Write(marker);

                  if (isToConsole)
                        Console.ForegroundColor = node is Token ? ConsoleColor.Blue : ConsoleColor.Cyan;

                  writer.Write(node is Token ? ((Token)node).Type : node.GetType());

                  if (node is Token t && t.Text != null)
                  {
                        writer.Write(" ");
                        writer.Write(t.Text);
                  }

                  if (isToConsole)
                        Console.ResetColor();

                  writer.WriteLine();

                  indent += isLast ? "    " : "│   ";

                  var lastChild = node.GetChildren().LastOrDefault();

                  foreach (var child in node.GetChildren())
                        PrintFormattedTree(writer, child, indent, child == lastChild);
            }

            public override string ToString()
            {
                  using(var writer = new StringWriter())
                  {
                        WriteTo(writer);
                        return writer.ToString();
                  }
            }
      }

      /// <summary>
      /// expr -> term [+-] expr | term
      /// (addition/subtraction of a term and an expr, or a term alone)
      /// </summary>
      internal class Expr : Node
      {
            public Node Left { get; }
            public Node Op { get; }
            public Node Right { get; }

            public Expr(Node left, Node op, Node right)
            {
                  Left = left;
                  Op = op;
                  Right = right;
            }
      }

      /// <summary>
      /// term -> NUM [/*] term | NUM
      /// (multiplication/division of a number and a term, or a number alone)
      /// </summary>
      internal class Term : Node
      {
            public Node Left { get; }
            public Node Op { get; }
            public Node Right { get; }

            public Term(Node left, Node op, Node right)
            {
                  Left = left;
                  Op = op;
                  Right = right;
            }
      }
}