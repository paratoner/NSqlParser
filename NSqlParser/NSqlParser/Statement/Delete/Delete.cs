using NSqlParser.Expression;
using NSqlParser.Schema;

namespace NSqlParser.Statement.Delete
{
    public class Delete : IStatement {

        private Table table;
        private IExpression where;

        public void Accept(IStatementVisitor statementVisitor) {
            statementVisitor.Visit(this);
        }

        public Table getTable() {
            return table;
        }

        public IExpression getWhere() {
            return where;
        }

        public void setTable(Table name) {
            table = name;
        }

        public void setWhere(IExpression expression) {
            where = expression;
        }

        public override string ToString() {
            return "DELETE FROM " + table + ((where != null) ? " WHERE " + where : "");
        }
    }
}
