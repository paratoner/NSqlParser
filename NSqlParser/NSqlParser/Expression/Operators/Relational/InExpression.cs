
using System;

namespace NSqlParser.Expression.Operators.Relational
{
    public class InExpression : IExpression, ISupportsOldOracleJoinSyntax
    {

        private IExpression leftExpression;
        private IItemsList leftItemsList;
        private IItemsList rightItemsList;
        private bool not = false;

        private int oldOracleJoinSyntax = BinaryExpression.NO_ORACLE_JOIN;

        public InExpression()
        {
        }

        public InExpression(IExpression leftExpression, IItemsList itemsList)
        {
            setLeftExpression(leftExpression);
            setRightItemsList(itemsList);
        }


        public void setOldOracleJoinSyntax(int oldOracleJoinSyntax)
        {
            this.oldOracleJoinSyntax = oldOracleJoinSyntax;
            if (oldOracleJoinSyntax < 0 || oldOracleJoinSyntax > 1)
            {
                throw new ArgumentException("unexpected join type for oracle found with IN (type=" + oldOracleJoinSyntax + ")");
            }
        }


        public int getOldOracleJoinSyntax()
        {
            return oldOracleJoinSyntax;
        }

        public IItemsList getRightItemsList()
        {
            return rightItemsList;
        }

        public IExpression getLeftExpression()
        {
            return leftExpression;
        }

        public void setRightItemsList(IItemsList list)
        {
            rightItemsList = list;
        }

        public void setLeftExpression(IExpression expression)
        {
            leftExpression = expression;
        }

        public bool isNot()
        {
            return not;
        }

        public void setNot(bool b)
        {
            not = b;
        }

        public IItemsList getLeftItemsList()
        {
            return leftItemsList;
        }

        public void setLeftItemsList(IItemsList leftItemsList)
        {
            this.leftItemsList = leftItemsList;
        }


        public void Accept(IExpressionVisitor expressionVisitor)
        {
            expressionVisitor.Visit(this);
        }

        private string getLeftExpressionString()
        {
            return leftExpression + (oldOracleJoinSyntax == BinaryExpression.ORACLE_JOIN_RIGHT ? "(+)" : "");
        }


        public override string ToString()
        {
            return (leftExpression == null ? leftItemsList.ToString() : getLeftExpressionString()) + " " + ((not) ? "NOT " : "") + "IN " + rightItemsList + "";
        }


        public int getOraclePriorPosition()
        {
            return BinaryExpression.NO_ORACLE_PRIOR;
        }


        public void setOraclePriorPosition(int priorPosition)
        {
            if (priorPosition != BinaryExpression.NO_ORACLE_PRIOR)
            {
                throw new ArgumentException("unexpected prior for oracle found");
            }
        }
    }
}
