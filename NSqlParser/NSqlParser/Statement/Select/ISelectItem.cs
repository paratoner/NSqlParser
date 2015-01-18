namespace NSqlParser.Statement.Select
{
    public interface ISelectItem {

        void Accept(ISelectItemVisitor selectItemVisitor);
    }
}
