/**
 * A basic class for binary expressions, that is expressions having a left
 * member and a right member which are in turn expressions.
 */
namespace NSqlParser.Expression
{
    public abstract class BinaryExpression : IExpression
    {

        private IExpression leftExpression;
        private IExpression rightExpression;
        private bool not = false;

        public BinaryExpression()
        {
        }

        public IExpression getLeftExpression()
        {
            return leftExpression;
        }

        public IExpression getRightExpression()
        {
            return rightExpression;
        }

        public void setLeftExpression(IExpression expression)
        {
            leftExpression = expression;
        }

        public void setRightExpression(IExpression expression)
        {
            rightExpression = expression;
        }

        public void setNot()
        {
            not = true;
        }

        public virtual bool isNot()
        {
            return not;
        }


        public override string ToString()
        {
            return (not ? "NOT " : "") + getLeftExpression() + " " + getStringExpression() + " " + getRightExpression();
        }

        public abstract string getStringExpression();

        public const int NO_ORACLE_JOIN= 0;
        public const int ORACLE_JOIN_RIGHT = 1;
        public const int ORACLE_JOIN_LEFT = 2;

        public const int NO_ORACLE_PRIOR = 0;
        public const int ORACLE_PRIOR_START = 1;
        public const int ORACLE_PRIOR_END = 2;

        public virtual void Accept(IExpressionVisitor expressionVisitor)
        {
        }
    }
}
