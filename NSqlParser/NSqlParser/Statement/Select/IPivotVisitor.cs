namespace NSqlParser.Statement.Select
{
    public interface IPivotVisitor
    {
        void Visit(Pivot pivot);

        void Visit(PivotXml pivot);

    }
}
