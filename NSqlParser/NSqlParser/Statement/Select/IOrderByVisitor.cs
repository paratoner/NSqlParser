namespace NSqlParser.Statement.Select
{
    public interface IOrderByVisitor {

        void Visit(OrderByElement orderBy);
    }
}
