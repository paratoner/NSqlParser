using NSqlParser.Expression.Operators.Arithmetic;
using NSqlParser.Expression.Operators.Conditional;
using NSqlParser.Expression.Operators.Relational;
using NSqlParser.Schema;
using NSqlParser.Statement.Select;

namespace NSqlParser.Expression
{
    public interface IExpressionVisitor {

        void Visit(NullValue nullValue);

        void Visit(Function function);

        void Visit(SignedExpression signedExpression);

        void Visit(JdbcParameter jdbcParameter);

        void Visit(JdbcNamedParameter jdbcNamedParameter);

        void Visit(DoubleValue doubleValue);

        void Visit(LongValue longValue);

        void Visit(DateValue dateValue);

        void Visit(TimeValue timeValue);

        void Visit(TimestampValue timestampValue);

        void Visit(Parenthesis parenthesis);

        void Visit(StringValue stringValue);

        void Visit(Addition addition);

        void Visit(Division division);

        void Visit(Multiplication multiplication);

        void Visit(Subtraction subtraction);

        void Visit(AndExpression andExpression);

        void Visit(OrExpression orExpression);

        void Visit(Between between);

        void Visit(EqualsTo equalsTo);

        void Visit(GreaterThan greaterThan);

        void Visit(GreaterThanEquals greaterThanEquals);

        void Visit(InExpression inExpression);

        void Visit(IsNullExpression isNullExpression);

        void Visit(LikeExpression likeExpression);

        void Visit(MinorThan minorThan);

        void Visit(MinorThanEquals minorThanEquals);

        void Visit(NotEqualsTo notEqualsTo);

        void Visit(Column tableColumn);

        void Visit(SubSelect subSelect);

        void Visit(CaseExpression caseExpression);

        void Visit(WhenClause whenClause);

        void Visit(ExistsExpression existsExpression);

        void Visit(AllComparisonExpression allComparisonExpression);

        void Visit(AnyComparisonExpression anyComparisonExpression);

        void Visit(Concat concat);

        void Visit(Matches matches);

        void Visit(BitwiseAnd bitwiseAnd);

        void Visit(BitwiseOr bitwiseOr);

        void Visit(BitwiseXor bitwiseXor);

        void Visit(CastExpression cast);

        void Visit(Modulo modulo);

        void Visit(AnalyticExpression aexpr);

        void Visit(ExtractExpression eexpr);

        void Visit(IntervalExpression iexpr);

        void Visit(OracleHierarchicalExpression oexpr);

        void Visit(RegExpMatchOperator rexpr);
    
        void Visit(JsonExpression jsonExpr);

        void Visit(RegExpMySQLOperator regExpMySQLOperator);
    }
}
