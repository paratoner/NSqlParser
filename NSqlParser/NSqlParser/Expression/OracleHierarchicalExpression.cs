/**
 *
 * @author toben
 */

using System.Text;

namespace NSqlParser.Expression
{
    public class OracleHierarchicalExpression : IExpression
    {

        private IExpression startExpression;
        private IExpression connectExpression;
        private bool noCycle = false;

        public IExpression getStartExpression()
        {
            return startExpression;
        }

        public void setStartExpression(IExpression startExpression)
        {
            this.startExpression = startExpression;
        }

        public IExpression getConnectExpression()
        {
            return connectExpression;
        }

        public void setConnectExpression(IExpression connectExpression)
        {
            this.connectExpression = connectExpression;
        }

        public bool isNoCycle()
        {
            return noCycle;
        }

        public void setNoCycle(bool noCycle)
        {
            this.noCycle = noCycle;
        }


        public void Accept(IExpressionVisitor expressionVisitor)
        {
            expressionVisitor.Visit(this);
        }


        public override string ToString()
        {
            StringBuilder b = new StringBuilder();
            if (startExpression != null)
            {
                b.Append(" START WITH ").Append(startExpression.ToString());
            }
            b.Append(" CONNECT BY ");
            if (isNoCycle())
            {
                b.Append("NOCYCLE ");
            }
            b.Append(connectExpression.ToString());
            return b.ToString();
        }
    }
}
