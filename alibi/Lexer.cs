// This file is the lexer that takes the text and converts it to tokens
using System.Collections.Immutable;

namespace Alibi
{
      internal class Lexer
      {
            /// <summary>
            /// Takes the text and converts it to tokens
            /// </summary>
            public ImmutableArray<Token> Lex(string input)
            {
                  // Create builder for tokens array
                  var tokens = ImmutableArray.CreateBuilder<Token>();

                  // Traverse input text and lex tokens
                  var currentPos = 0;
                  while (currentPos < input.Length)
                  {
                        var tokenStartPos = currentPos;
                        var lookahead = input[currentPos];

                        // Ignore whitespace
                        if (char.IsWhiteSpace(lookahead))
                        {
                              currentPos++;
                        }
                        // Plus token
                        else if (lookahead == '+')
                        {
                              currentPos++;
                              tokens.Add(new Token(TokenType.Plus, lookahead.ToString(), tokenStartPos));
                        }
                        // Minus token
                        else if (lookahead == '-')
                        {
                              currentPos++;
                              tokens.Add(new Token(TokenType.Minus, lookahead.ToString(), tokenStartPos));
                        }
                        // Star/Asterisk/Multiplication token
                        else if (lookahead == '*')
                        {
                              currentPos++;
                              tokens.Add(new Token(TokenType.Star, lookahead.ToString(), tokenStartPos));
                        }
                        // Slash/Division token
                        else if (lookahead == '/')
                        {
                              currentPos++;
                              tokens.Add(new Token(TokenType.Slash, lookahead.ToString(), tokenStartPos));
                        }
                        // Number token
                        else if (char.IsDigit(lookahead))
                        {
                              var text = "";
                              while (currentPos < input.Length && char.IsDigit(input[currentPos]))
                              {
                                    text += input[currentPos];
                                    currentPos++;
                              }
                              tokens.Add(new Token(TokenType.Number, text, tokenStartPos));
                        }
                        // Identifier/keyword token
                        else if(char.IsLetter(lookahead))
                        {
                              var text = "";
                              while(currentPos < input.Length && char.IsLetterOrDigit(input[currentPos])) 
                              {
                                    text += input[currentPos];
                                    currentPos++;
                              }
                              switch(text) 
                              {
                                    // True keyword
                                    case "true":
                                          tokens.Add(new Token(TokenType.True, text, tokenStartPos));
                                          break;
                                    // False keyword
                                    case "false":
                                          tokens.Add(new Token(TokenType.False, text, tokenStartPos));
                                          break;
                                    // TODO: more keywords
                                    // Identifier
                                    default:
                                          tokens.Add(new Token(TokenType.Identifier, text, tokenStartPos));
                                          break;
                              }
                        }
                        else
                        {
                              // TODO: Error handling for unexpected tokens
                        }
                  }

                  // Add EOF
                  tokens.Add(new Token(TokenType.EOF, "<EOF>", currentPos));
                  // Return tokens
                  return tokens.ToImmutable();
            }
      }
}