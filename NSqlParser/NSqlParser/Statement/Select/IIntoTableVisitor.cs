using NSqlParser.Schema;

namespace NSqlParser.Statement.Select
{
    public interface IIntoTableVisitor {

        void Visit(Table tableName);
    }
}
