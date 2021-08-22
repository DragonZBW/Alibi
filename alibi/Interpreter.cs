// This file takes the parse tree and interprets it in C#
namespace Alibi
{
      /// <summary>
      /// Interprets the parse tree and executes instructions
      /// </summary>
      internal class Interpreter
      {
            private Node Root { get; }

            public Interpreter(Node root)
            {
                  Root = root;
            }

            public object Interpret()
            {
                  return InterpretExpr(Root);
            }

            private double InterpretExpr(Node expr)
            {
                  // Expr is a Expr
                  if(expr is Expr e)
                  {
                        var left = InterpretTerm(e.Left);
                        var op = e.Op;
                        var right = InterpretExpr(e.Right);

                        if(((Token)op).Type == TokenType.Plus)
                        {
                              return left + right;
                        }
                        else
                        {
                              return left - right;
                        }
                  }
                  // Expr is a Term
                  else
                  {
                        return InterpretTerm(expr);
                  }
            }

            private double InterpretTerm(Node term)
            {
                  // Term is a Term
                  if(term is Term t)
                  {
                        var left = double.Parse(((Token)t.Left).Text);
                        var op = t.Op;
                        var right = InterpretTerm(t.Right);

                        if(((Token)op).Type == TokenType.Star)
                        {
                              return left * right;
                        }
                        else
                        {
                              return left / right;
                        }
                  }
                  // Term is a NUM
                  else
                  {
                        return double.Parse(((Token)term).Text);
                  }
            }
      }
}