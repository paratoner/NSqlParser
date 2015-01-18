using NSqlParser.Statement.Select;

namespace NSqlParser.Expression.Operators.Relational
{
    public class ItemsListVisitorAdapter : IItemsListVisitor
    {
        public void Visit(SubSelect subSelect)
        {

        }

        public void Visit(ExpressionList expressionList)
        {

        }

        public void Visit(MultiExpressionList multiExprList)
        {
            foreach (var list in multiExprList.getExprList())
            {
                Visit(list);
            }
        }
    }
}
