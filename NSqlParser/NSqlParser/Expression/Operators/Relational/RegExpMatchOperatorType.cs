/**
 * PostgresSQL match operators.
 * @author Paratoner
 */
namespace NSqlParser.Expression.Operators.Relational
{
    public enum RegExpMatchOperatorType {
        MATCH_CASESENSITIVE,
        MATCH_CASEINSENSITIVE,
        NOT_MATCH_CASESENSITIVE,
        NOT_MATCH_CASEINSENSITIVE
    }
}
