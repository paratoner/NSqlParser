namespace NSqlParser.Statement.Select
{
    public interface ISelectBody {

        void Accept(ISelectVisitor selectVisitor);
    }
}
