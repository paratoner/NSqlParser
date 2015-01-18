using NSqlParser.Expression;
using NSqlParser.Expression.Operators.Relational;

namespace NSqlParser.Statement.Select
{
    public class ExpressionListItem 
    {

        private ExpressionList expressionList;
        private Alias alias;

        public ExpressionList getExpressionList()
        {
            return expressionList;
        }

        public void setExpressionList(ExpressionList expressionList)
        {
            this.expressionList = expressionList;
        }

        public Alias getAlias()
        {
            return alias;
        }

        public void setAlias(Alias alias)
        {
            this.alias = alias;
        }

        public override string ToString()
        {
            return expressionList + ((alias != null) ? alias.ToString() : "");
        }

   
    }
}
