namespace NSqlParser.Statement.Select
{
    public interface ISelectItemVisitor {

        void Visit(AllColumns allColumns);

        void Visit(AllTableColumns allTableColumns);

        void Visit(SelectExpressionItem selectExpressionItem);
    }
}
