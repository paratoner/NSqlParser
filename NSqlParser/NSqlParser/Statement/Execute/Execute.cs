using NSqlParser.Expression.Operators.Relational;
using NSqlParser.Statement.Select;

namespace NSqlParser.Statement.Execute
{
    public class Execute : IStatement {

        private string name;
        private ExpressionList exprList;

        public string getName() {
            return name;
        }

        public void setName(string name) {
            this.name = name;
        }

        public ExpressionList getExprList() {
            return exprList;
        }

        public void setExprList(ExpressionList exprList) {
            this.exprList = exprList;
        }
    
        public void Accept(IStatementVisitor statementVisitor) {
            statementVisitor.Visit(this);
        }

        public override string ToString() {
            return "EXECUTE " + name + " " + PlainSelect.getStringList(exprList.getExpressions(), true, false);
        }

    }
}
