/**
 * A TRUNCATE TABLE statement
 */

using NSqlParser.Schema;

namespace NSqlParser.Statement.Truncate
{
    public class Truncate : IStatement
    {

        private Table table;

        public void Accept(IStatementVisitor statementVisitor)
        {
            statementVisitor.Visit(this);
        }

        public Table getTable()
        {
            return table;
        }

        public void setTable(Table table)
        {
            this.table = table;
        }

        public override string ToString()
        {
            return "TRUNCATE TABLE " + table;
        }
    }
}
