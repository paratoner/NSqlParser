using NSqlParser.Expression.Operators.Arithmetic;
using NSqlParser.Expression.Operators.Conditional;
using NSqlParser.Expression.Operators.Relational;
using NSqlParser.Schema;
using NSqlParser.Statement.Select;

namespace NSqlParser.Expression
{
    public class ExpressionVisitorAdapter : IExpressionVisitor, IItemsListVisitor
    {

        public void Visit(NullValue value)
        {

        }


        public void Visit(Function function)
        {

        }


        public void Visit(SignedExpression expr)
        {
            expr.getExpression().Accept(this);
        }


        public void Visit(JdbcParameter parameter)
        {

        }


        public void Visit(JdbcNamedParameter parameter)
        {

        }


        public void Visit(DoubleValue value)
        {

        }


        public void Visit(LongValue value)
        {

        }


        public void Visit(DateValue value)
        {

        }


        public void Visit(TimeValue value)
        {

        }


        public void Visit(TimestampValue value)
        {

        }


        public void Visit(Parenthesis parenthesis)
        {
            parenthesis.getExpression().Accept(this);
        }


        public void Visit(StringValue value)
        {

        }


        public void Visit(Addition expr)
        {
            VisitBinaryExpression(expr);
        }


        public void Visit(Division expr)
        {
            VisitBinaryExpression(expr);
        }


        public void Visit(Multiplication expr)
        {
            VisitBinaryExpression(expr);
        }


        public void Visit(Subtraction expr)
        {
            VisitBinaryExpression(expr);
        }


        public void Visit(AndExpression expr)
        {
            VisitBinaryExpression(expr);
        }


        public void Visit(OrExpression expr)
        {
            VisitBinaryExpression(expr);
        }


        public void Visit(Between expr)
        {
            expr.getLeftExpression().Accept(this);
            expr.getBetweenExpressionStart().Accept(this);
            expr.getBetweenExpressionEnd().Accept(this);
        }


        public void Visit(EqualsTo expr)
        {
            VisitBinaryExpression(expr);
        }


        public void Visit(GreaterThan expr)
        {
            VisitBinaryExpression(expr);
        }


        public void Visit(GreaterThanEquals expr)
        {
            VisitBinaryExpression(expr);
        }


        public void Visit(InExpression expr)
        {
            expr.getLeftExpression().Accept(this);
            expr.getLeftItemsList().Accept(this);
            expr.getRightItemsList().Accept(this);
        }


        public void Visit(IsNullExpression expr)
        {
            expr.getLeftExpression().Accept(this);
        }


        public void Visit(LikeExpression expr)
        {
            VisitBinaryExpression(expr);
        }


        public void Visit(MinorThan expr)
        {
            VisitBinaryExpression(expr);
        }


        public void Visit(MinorThanEquals expr)
        {
            VisitBinaryExpression(expr);
        }


        public void Visit(NotEqualsTo expr)
        {
            VisitBinaryExpression(expr);
        }


        public void Visit(Column column)
        {

        }


        public void Visit(SubSelect subSelect)
        {

        }


        public void Visit(CaseExpression expr)
        {
            expr.getSwitchExpression().Accept(this);

            foreach (IExpression x in expr.getWhenClauses())
            {
                x.Accept(this);
            }
            expr.getElseExpression().Accept(this);
        }


        public void Visit(WhenClause expr)
        {
            expr.getWhenExpression().Accept(this);
            expr.getThenExpression().Accept(this);
        }


        public void Visit(ExistsExpression expr)
        {
            expr.getRightExpression().Accept(this);
        }


        public void Visit(AllComparisonExpression expr)
        {

        }


        public void Visit(AnyComparisonExpression expr)
        {

        }


        public void Visit(Concat expr)
        {
            VisitBinaryExpression(expr);
        }


        public void Visit(Matches expr)
        {
            VisitBinaryExpression(expr);
        }


        public void Visit(BitwiseAnd expr)
        {
            VisitBinaryExpression(expr);
        }


        public void Visit(BitwiseOr expr)
        {
            VisitBinaryExpression(expr);
        }


        public void Visit(BitwiseXor expr)
        {
            VisitBinaryExpression(expr);
        }


        public void Visit(CastExpression expr)
        {
            expr.getLeftExpression().Accept(this);
        }


        public void Visit(Modulo expr)
        {
            VisitBinaryExpression(expr);
        }


        public void Visit(AnalyticExpression expr) {
            expr.getExpression().Accept(this);
            expr.getDefaultValue().Accept(this);
            expr.getOffset().Accept(this);
            foreach (OrderByElement element in expr.getOrderByElements()) {
                element.getExpression().Accept(this);
            }

            expr.getWindowElement().getRange().getStart().getExpression().Accept(this);
            expr.getWindowElement().getRange().getEnd().getExpression().Accept(this);
            expr.getWindowElement().getOffset().getExpression().Accept(this);
        }


        public void Visit(ExtractExpression expr)
        {
            expr.getExpression().Accept(this);
        }


        public void Visit(IntervalExpression expr)
        {

        }


        public void Visit(OracleHierarchicalExpression expr)
        {
            expr.getConnectExpression().Accept(this);
            expr.getStartExpression().Accept(this);
        }


        public void Visit(RegExpMatchOperator expr)
        {
            VisitBinaryExpression(expr);
        }


        public void Visit(ExpressionList expressionList) {
            foreach (IExpression expr in expressionList.getExpressions()) {
                expr.Accept(this);
            }
        }


        public void Visit(MultiExpressionList multiExprList) {
            foreach (ExpressionList list in multiExprList.getExprList()) {
                Visit(list);
            }
        }

        protected void VisitBinaryExpression(BinaryExpression expr)
        {
            expr.getLeftExpression().Accept(this);
            expr.getRightExpression().Accept(this);
        }


        public void Visit(JsonExpression jsonExpr)
        {
            Visit(jsonExpr.getColumn());
        }


        public void Visit(RegExpMySQLOperator expr)
        {
            VisitBinaryExpression(expr);
        }
    }
}
