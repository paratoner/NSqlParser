
using NSqlParser.Schema;

namespace NSqlParser.Statement.Select
{
    public class AllTableColumns : ISelectItem
    {

        private Table table;

        public AllTableColumns()
        {
        }

        public AllTableColumns(Table tableName)
        {
            this.table = tableName;
        }

        public Table getTable()
        {
            return table;
        }

        public void setTable(Table table)
        {
            this.table = table;
        }

        public void Accept(ISelectItemVisitor selectItemVisitor)
        {
            selectItemVisitor.Visit(this);
        }

        public override string ToString()
        {
            return table + ".*";
        }
    }
}
