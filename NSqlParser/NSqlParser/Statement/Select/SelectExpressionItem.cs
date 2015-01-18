/**
 * An expression as in "SELECT expr1 AS EXPR"
 */

using NSqlParser.Expression;

namespace NSqlParser.Statement.Select
{
    public class SelectExpressionItem : ISelectItem
    {

        private IExpression expression;
        private Alias alias;

        public SelectExpressionItem()
        {
        }

        public SelectExpressionItem(IExpression expression)
        {
            this.expression = expression;
        }

        public Alias getAlias()
        {
            return alias;
        }

        public IExpression getExpression()
        {
            return expression;
        }

        public void setAlias(Alias alias)
        {
            this.alias = alias;
        }

        public void setExpression(IExpression expression)
        {
            this.expression = expression;
        }

        public void Accept(ISelectItemVisitor selectItemVisitor)
        {
            selectItemVisitor.Visit(this);
        }

        public override string ToString()
        {
            return expression + ((alias != null) ? alias.ToString() : "");
        }
    }
}
