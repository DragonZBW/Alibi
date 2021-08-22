// This file is the parser that takes tokens from the lexer and parses them into a tree
using System.Collections.Immutable;

namespace Alibi
{
      /// <summary>
      /// Takes tokens and parses them into a tree
      /// </summary>
      internal class Parser
      {
            private ImmutableArray<Token> Tokens { get; }

            private int currToken = 0;
            private Token Lookahead => Tokens[currToken];

            public Parser(ImmutableArray<Token> tokens)
            {
                  Tokens = tokens;
            }

            public Node Parse()
            {
                  var result = ParseExpr();
                  return result;
            }

            private Token Match(TokenType type)
            {
                  var result = Lookahead;
                  if(Lookahead.Type != type)
                  {
                        return null;
                  }

                  currToken++;
                  return result;
            }

            // GRAMMARS - Definitions of all the parse methods for each grammar

            // expr -> term [+-] expr | term
            private Node ParseExpr()
            {
                  var term = ParseTerm();
                  var op = Match(TokenType.Plus);

                  if(op == null)
                  {
                        op = Match(TokenType.Minus);
                        if(op == null)
                              return term;
                  }

                  var expr = ParseExpr();
                  return new Expr(term, op, expr);
            }

            // term -> NUM [/*] term | NUM
            private Node ParseTerm()
            {
                  var num = Match(TokenType.Number);
                  var op = Match(TokenType.Slash);

                  if(op == null)
                  {
                        op = Match(TokenType.Star);
                        if(op == null)
                              return num;
                  }

                  var term = ParseTerm();
                  return new Term(num, op, term);
            }
      }
}