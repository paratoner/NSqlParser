using NSqlParser.Schema;

namespace NSqlParser.Statement.Select
{
    public interface IFromItemVisitor {

        void Visit(Table tableName);

        void Visit(SubSelect subSelect);

        void Visit(SubJoin subjoin);

        void Visit(LateralSubSelect lateralSubSelect);

        void Visit(ValuesList valuesList);
    }
}
