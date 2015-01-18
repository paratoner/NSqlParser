using NSqlParser.Statement.Create.Index;
using NSqlParser.Statement.Create.Table;
using NSqlParser.Statement.Create.View;

namespace NSqlParser.Statement
{
    public class StatementVisitorAdapter : IStatementVisitor {

        public void Visit(Select.Select select)
        {
            throw new System.NotImplementedException();
        }

        public void Visit(Delete.Delete delete)
        {
            throw new System.NotImplementedException();
        }

        public void Visit(Update.Update update)
        {
        }

        public void Visit(Insert.Insert insert)
        {
        }

        public void Visit(Replace.Replace replace)
        {
        }

        public void Visit(Drop.Drop drop)
        {
        }

        public void Visit(Truncate.Truncate truncate)
        {
        }

        public void Visit(CreateIndex createIndex)
        {
        }

        public void Visit(CreateTable createTable)
        {
        }

        public void Visit(CreateView createView)
        {
        }

        public void Visit(Alter.Alter alter)
        {
        }

        public void Visit(Statements stmts)
        {
        }

        public void Visit(Execute.Execute execute)
        {
        }
    }
}
