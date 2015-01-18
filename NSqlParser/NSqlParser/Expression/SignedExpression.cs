/**
 * It represents a "-" or "+" before an expression
 */

using System;

namespace NSqlParser.Expression
{
    public class SignedExpression : IExpression
    {

        private char sign;
        private IExpression expression;

        public SignedExpression(char sign, IExpression expression)
        {
            setSign(sign);
            setExpression(expression);
        }

        public char getSign()
        {
            return sign;
        }

        public void setSign(char sign)
        {
            this.sign = sign;
            if (sign != '+' && sign != '-')
            {
                throw new ArgumentException("illegal sign character, only + - allowed");
            }
        }

        public IExpression getExpression()
        {
            return expression;
        }

        public void setExpression(IExpression expression)
        {
            this.expression = expression;
        }


        public void Accept(IExpressionVisitor expressionVisitor)
        {
            expressionVisitor.Visit(this);
        }


        public override string ToString()
        {
            return getSign() + expression.ToString();
        }
    }
}
