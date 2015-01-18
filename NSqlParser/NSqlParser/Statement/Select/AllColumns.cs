namespace NSqlParser.Statement.Select
{
    public class AllColumns : ISelectItem
    {

        public AllColumns()
        {
        }

        public void Accept(ISelectItemVisitor selectItemVisitor)
        {
            selectItemVisitor.Visit(this);
        }

        public override string ToString()
        {
            return "*";
        }
    }
}
