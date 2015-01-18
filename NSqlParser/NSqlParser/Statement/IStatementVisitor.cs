using NSqlParser.Statement.Create.Index;
using NSqlParser.Statement.Create.Table;
using NSqlParser.Statement.Create.View;

namespace NSqlParser.Statement
{
    public interface IStatementVisitor {

        void Visit(Select.Select select);

        void Visit(Delete.Delete delete);

        void Visit(Update.Update update);

        void Visit(Insert.Insert insert);

        void Visit(Replace.Replace replace);

        void Visit(Drop.Drop drop);

        void Visit(Truncate.Truncate truncate);

        void Visit(CreateIndex createIndex);

        void Visit(CreateTable createTable);

        void Visit(CreateView createView);
	
        void Visit(Alter.Alter alter);
    
        void Visit(Statements stmts);
    
        void Visit(Execute.Execute execute);
    }
}
