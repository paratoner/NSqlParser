
namespace NSqlParser.Statement
{
    public interface IStatement {

        void Accept(IStatementVisitor statementVisitor);
    }
}
