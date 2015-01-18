using NSqlParser.Statement.Select;

namespace NSqlParser.Expression.Operators.Relational
{
    public interface IItemsListVisitor {

        void Visit(SubSelect subSelect);

        void Visit(ExpressionList expressionList);

        void Visit(MultiExpressionList multiExprList);
    }
}
