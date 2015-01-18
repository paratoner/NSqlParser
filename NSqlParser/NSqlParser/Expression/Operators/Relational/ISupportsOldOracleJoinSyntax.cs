namespace NSqlParser.Expression.Operators.Relational
{
    public interface ISupportsOldOracleJoinSyntax
    {



        int getOldOracleJoinSyntax();

        void setOldOracleJoinSyntax(int oldOracleJoinSyntax);

        int getOraclePriorPosition();

        void setOraclePriorPosition(int priorPosition);
    }
}
