using System.Collections.Generic;
using System.Text;

namespace NSqlParser.Statement
{
    public class Statements
    {

        private List<IStatement> statements;

        public List<IStatement> getStatements()
        {
            return statements;
        }

        public void setStatements(List<IStatement> statements)
        {
            this.statements = statements;
        }

        public void Accept(IStatementVisitor statementVisitor)
        {
            statementVisitor.Visit(this);
        }

        public override string ToString()
        {
            StringBuilder b = new StringBuilder();
            foreach (IStatement stmt in statements)
            {
                b.Append(stmt.ToString()).Append(";\n");
            }
            return b.ToString();
        }
    }
}
