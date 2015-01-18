using System.Linq;
using System.Text;
using NSqlParser.Statement;
using NSqlParser.Statement.Alter;
using NSqlParser.Statement.Create.Index;
using NSqlParser.Statement.Create.Table;
using NSqlParser.Statement.Create.View;
using NSqlParser.Statement.Delete;
using NSqlParser.Statement.Drop;
using NSqlParser.Statement.Execute;
using NSqlParser.Statement.Insert;
using NSqlParser.Statement.Replace;
using NSqlParser.Statement.Select;
using NSqlParser.Statement.Truncate;
using NSqlParser.Statement.Update;

namespace NSqlParser.Util.Deparser
{
    public class StatementDeParser : IStatementVisitor
    {

        private StringBuilder buffer;

        public StatementDeParser(StringBuilder buffer)
        {
            this.buffer = buffer;
        }

        public void Visit(CreateIndex createIndex)
        {
            CreateIndexDeParser createIndexDeParser = new CreateIndexDeParser(buffer);
            createIndexDeParser.deParse(createIndex);
        }

        public void Visit(CreateTable createTable)
        {
            CreateTableDeParser createTableDeParser = new CreateTableDeParser(buffer);
            createTableDeParser.deParse(createTable);
        }

        public void Visit(CreateView createView)
        {
            CreateViewDeParser createViewDeParser = new CreateViewDeParser(buffer);
            createViewDeParser.deParse(createView);
        }

        public void Visit(Delete delete)
        {
            SelectDeParser selectDeParser = new SelectDeParser();
            selectDeParser.setBuffer(buffer);
            ExpressionDeParser expressionDeParser = new ExpressionDeParser(selectDeParser, buffer);
            selectDeParser.setExpressionVisitor(expressionDeParser);
            DeleteDeParser deleteDeParser = new DeleteDeParser(expressionDeParser, buffer);
            deleteDeParser.deParse(delete);
        }

        public void Visit(Drop drop)
        {
            // TODO Auto-generated method stub
        }

        public void Visit(Insert insert)
        {
            SelectDeParser selectDeParser = new SelectDeParser();
            selectDeParser.setBuffer(buffer);
            ExpressionDeParser expressionDeParser = new ExpressionDeParser(selectDeParser, buffer);
            selectDeParser.setExpressionVisitor(expressionDeParser);
            InsertDeParser insertDeParser = new InsertDeParser(expressionDeParser, selectDeParser, buffer);
            insertDeParser.deParse(insert);

        }

        public void Visit(Replace replace)
        {
            SelectDeParser selectDeParser = new SelectDeParser();
            selectDeParser.setBuffer(buffer);
            ExpressionDeParser expressionDeParser = new ExpressionDeParser(selectDeParser, buffer);
            selectDeParser.setExpressionVisitor(expressionDeParser);
            ReplaceDeParser replaceDeParser = new ReplaceDeParser(expressionDeParser, selectDeParser, buffer);
            replaceDeParser.deParse(replace);
        }

        public void Visit(Select select)
        {
            SelectDeParser selectDeParser = new SelectDeParser();
            selectDeParser.setBuffer(buffer);
            ExpressionDeParser expressionDeParser = new ExpressionDeParser(selectDeParser, buffer);
            selectDeParser.setExpressionVisitor(expressionDeParser);
            if (select.getWithItemsList() != null && select.getWithItemsList().Count > 0)
            {
                buffer.Append("WITH ");
                foreach (WithItem withItem in select.getWithItemsList())
                {
                    withItem.Accept(selectDeParser);
                    if (!(withItem == select.getWithItemsList().Last()))
                    {
                        buffer.Append(",");
                    }
                    buffer.Append(" ");
                }
            }
            select.getSelectBody().Accept(selectDeParser);
        }

        public void Visit(Truncate truncate)
        {
        }

        public void Visit(Update update)
        {
            SelectDeParser selectDeParser = new SelectDeParser();
            selectDeParser.setBuffer(buffer);
            ExpressionDeParser expressionDeParser = new ExpressionDeParser(selectDeParser, buffer);
            UpdateDeParser updateDeParser = new UpdateDeParser(expressionDeParser, selectDeParser, buffer);
            selectDeParser.setExpressionVisitor(expressionDeParser);
            updateDeParser.deParse(update);

        }

        public StringBuilder getBuffer()
        {
            return buffer;
        }

        public void setBuffer(StringBuilder buffer)
        {
            this.buffer = buffer;
        }

        public void Visit(Alter alter)
        {

        }

        public void Visit(Statements stmts)
        {
            stmts.Accept(this);
        }

        public void Visit(Execute execute)
        {
            SelectDeParser selectDeParser = new SelectDeParser();
            selectDeParser.setBuffer(buffer);
            ExpressionDeParser expressionDeParser = new ExpressionDeParser(selectDeParser, buffer);
            ExecuteDeParser executeDeParser = new ExecuteDeParser(expressionDeParser, buffer);
            selectDeParser.setExpressionVisitor(expressionDeParser);
            executeDeParser.deParse(execute);
        }
    }
}
