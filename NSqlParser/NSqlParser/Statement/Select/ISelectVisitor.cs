namespace NSqlParser.Statement.Select
{
    public interface ISelectVisitor {

        void Visit(PlainSelect plainSelect);

        void Visit(SetOperationList setOpList);

        void Visit(WithItem withItem);
    }
}
