// This class represents a token of some type

namespace Alibi
{
      internal enum TokenType
      {
            Number,
            Plus,
            Minus,
            Star,
            Slash,
            Identifier,
            True,
            False,
            EOF
      }
      
      internal class Token : Node
      {
            public TokenType Type { get; }
            public string Text { get; }
            public int StartPos { get; }

            public Token(TokenType type, string text, int startPos)
            {
                  Type = type;
                  Text = text;
                  StartPos = startPos;
            }
      }
}