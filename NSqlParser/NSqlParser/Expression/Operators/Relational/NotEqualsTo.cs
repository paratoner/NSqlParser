using System;

namespace NSqlParser.Expression.Operators.Relational
{
    public class NotEqualsTo : OldOracleJoinBinaryExpression
    {

        private readonly string _operator;

        public NotEqualsTo()
        {
            _operator = "<>";
        }

        public NotEqualsTo(string oprtr)
        {
            this._operator = oprtr;
            if (!"!=".Equals(oprtr) && !"<>".Equals(oprtr))
            {
                throw new ArgumentException("only <> or != allowed");
            }
        }


        public override void Accept(IExpressionVisitor expressionVisitor)
        {
            expressionVisitor.Visit(this);
        }


        public override string getStringExpression()
        {
            return _operator;
        }
    }
}
