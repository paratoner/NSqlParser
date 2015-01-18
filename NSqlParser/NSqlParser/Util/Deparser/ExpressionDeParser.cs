using System.Linq;
using System.Text;
using NSqlParser.Expression;
using NSqlParser.Expression.Operators.Arithmetic;
using NSqlParser.Expression.Operators.Conditional;
using NSqlParser.Expression.Operators.Relational;
using NSqlParser.Schema;
using NSqlParser.Statement.Select;
/**
 * A class to de-parse (that is, tranform from NSqlParser hierarchy into a
 * string) an {@link NSqlParser.expression.Expression}
 */

namespace NSqlParser.Util.Deparser
{
    public class ExpressionDeParser : IExpressionVisitor, IItemsListVisitor
    {

        private StringBuilder buffer;
        private ISelectVisitor selectVisitor;
        private bool useBracketsInExprList = true;

        public ExpressionDeParser()
        {
        }

        /**
     * @param selectVisitor a SelectVisitor to de-parse SubSelects. It has to
     * share the same<br> StringBuilder as this object in order to work, as:
     *
     * <pre>
     * <code>
     * StringBuilder myBuf = new StringBuilder();
     * MySelectDeparser selectDeparser = new  MySelectDeparser();
     * selectDeparser.setBuffer(myBuf);
     * ExpressionDeParser expressionDeParser = new ExpressionDeParser(selectDeparser, myBuf);
     * </code>
     * </pre>
     *
     * @param buffer the buffer that will be filled with the expression
     */
        public ExpressionDeParser(ISelectVisitor selectVisitor, StringBuilder buffer)
        {
            this.selectVisitor = selectVisitor;
            this.buffer = buffer;
        }

        public StringBuilder getBuffer()
        {
            return buffer;
        }

        public void setBuffer(StringBuilder buffer)
        {
            this.buffer = buffer;
        }

        public void Visit(Addition addition)
        {
            visitBinaryExpression(addition, " + ");
        }

        public void Visit(AndExpression andExpression)
        {
            visitBinaryExpression(andExpression, " AND ");
        }

        public void Visit(Between between)
        {
            between.getLeftExpression().Accept(this);
            if (between.isNot())
            {
                buffer.Append(" NOT");
            }

            buffer.Append(" BETWEEN ");
            between.getBetweenExpressionStart().Accept(this);
            buffer.Append(" AND ");
            between.getBetweenExpressionEnd().Accept(this);

        }

        public void Visit(EqualsTo equalsTo)
        {
            visitOldOracleJoinBinaryExpression(equalsTo, " = ");
        }

        public void Visit(Division division)
        {
            visitBinaryExpression(division, " / ");

        }

        public void Visit(DoubleValue doubleValue)
        {
            buffer.Append(doubleValue.ToString());

        }

        public void visitOldOracleJoinBinaryExpression(OldOracleJoinBinaryExpression expression, string operatorr)
        {
            if (expression.isNot())
            {
                buffer.Append(" NOT ");
            }
            expression.getLeftExpression().Accept(this);
            if (expression.getOldOracleJoinSyntax() == EqualsTo.ORACLE_JOIN_RIGHT)
            {
                buffer.Append("(+)");
            }
            buffer.Append(operatorr);
            expression.getRightExpression().Accept(this);
            if (expression.getOldOracleJoinSyntax() == EqualsTo.ORACLE_JOIN_LEFT)
            {
                buffer.Append("(+)");
            }
        }

        public void Visit(GreaterThan greaterThan)
        {
            visitOldOracleJoinBinaryExpression(greaterThan, " > ");
        }

        public void Visit(GreaterThanEquals greaterThanEquals)
        {
            visitOldOracleJoinBinaryExpression(greaterThanEquals, " >= ");

        }

        public void Visit(InExpression inExpression)
        {
            if (inExpression.getLeftExpression() == null)
            {
                inExpression.getLeftItemsList().Accept(this);
            }
            else
            {
                inExpression.getLeftExpression().Accept(this);
                if (inExpression.getOldOracleJoinSyntax() == BinaryExpression.ORACLE_JOIN_RIGHT)
                {
                    buffer.Append("(+)");
                }
            }
            if (inExpression.isNot())
            {
                buffer.Append(" NOT");
            }
            buffer.Append(" IN ");

            inExpression.getRightItemsList().Accept(this);
        }

        public void Visit(SignedExpression signedExpression)
        {
            buffer.Append(signedExpression.getSign());
            signedExpression.getExpression().Accept(this);
        }

        public void Visit(IsNullExpression isNullExpression)
        {
            isNullExpression.getLeftExpression().Accept(this);
            if (isNullExpression.isNot())
            {
                buffer.Append(" IS NOT NULL");
            }
            else
            {
                buffer.Append(" IS NULL");
            }
        }

        public void Visit(JdbcParameter jdbcParameter)
        {
            buffer.Append("?");

        }

        public void Visit(LikeExpression likeExpression)
        {
            visitBinaryExpression(likeExpression, " LIKE ");
            string escape = likeExpression.getEscape();
            if (escape != null)
            {
                buffer.Append(" ESCAPE '").Append(escape).Append('\'');
            }
        }

        public void Visit(ExistsExpression existsExpression)
        {
            if (existsExpression.isNot())
            {
                buffer.Append("NOT EXISTS ");
            }
            else
            {
                buffer.Append("EXISTS ");
            }
            existsExpression.getRightExpression().Accept(this);
        }

        public void Visit(LongValue longValue)
        {
            buffer.Append(longValue.getStringValue());

        }

        public void Visit(MinorThan minorThan)
        {
            visitOldOracleJoinBinaryExpression(minorThan, " < ");

        }

        public void Visit(MinorThanEquals minorThanEquals)
        {
            visitOldOracleJoinBinaryExpression(minorThanEquals, " <= ");

        }

        public void Visit(Multiplication multiplication)
        {
            visitBinaryExpression(multiplication, " * ");

        }

        public void Visit(NotEqualsTo notEqualsTo)
        {
            visitOldOracleJoinBinaryExpression(notEqualsTo, " " + notEqualsTo.getStringExpression() + " ");

        }

        public void Visit(NullValue nullValue)
        {
            buffer.Append("NULL");

        }

        public void Visit(OrExpression orExpression)
        {
            visitBinaryExpression(orExpression, " OR ");

        }

        public void Visit(Parenthesis parenthesis)
        {
            if (parenthesis.isNot())
            {
                buffer.Append(" NOT ");
            }

            buffer.Append("(");
            parenthesis.getExpression().Accept(this);
            buffer.Append(")");

        }

        public void Visit(StringValue stringValue)
        {
            buffer.Append("'").Append(stringValue.getValue()).Append("'");

        }

        public void Visit(Subtraction subtraction)
        {
            visitBinaryExpression(subtraction, " - ");

        }

        private void visitBinaryExpression(BinaryExpression binaryExpression, string operatorr)
        {
            if (binaryExpression.isNot())
            {
                buffer.Append(" NOT ");
            }
            binaryExpression.getLeftExpression().Accept(this);
            buffer.Append(operatorr);
            binaryExpression.getRightExpression().Accept(this);

        }

        public void Visit(SubSelect subSelect)
        {
            buffer.Append("(");
            subSelect.getSelectBody().Accept(selectVisitor);
            buffer.Append(")");
        }

        public void Visit(Column tableColumn)
        {
            Table table = tableColumn.getTable();
            string tableName = null;
            if (table != null)
            {
                if (table.getAlias() != null)
                {
                    tableName = table.getAlias().getName();
                }
                else
                {
                    tableName = table.GetFullyQualifiedName();
                }
            }
            if (tableName != null && !string.IsNullOrEmpty(tableName))
            {
                buffer.Append(tableName).Append(".");
            }

            buffer.Append(tableColumn.getColumnName());
        }

        public void Visit(Function function)
        {
            if (function.IsEscaped())
            {
                buffer.Append("{fn ");
            }

            buffer.Append(function.getName());
            if (function.isAllColumns() && function.getParameters() == null)
            {
                buffer.Append("(*)");
            }
            else if (function.getParameters() == null)
            {
                buffer.Append("()");
            }
            else
            {
                bool oldUseBracketsInExprList = useBracketsInExprList;
                if (function.isDistinct())
                {
                    useBracketsInExprList = false;
                    buffer.Append("(DISTINCT ");
                }
                else if (function.isAllColumns())
                {
                    useBracketsInExprList = false;
                    buffer.Append("(ALL ");
                }
                Visit(function.getParameters());
                useBracketsInExprList = oldUseBracketsInExprList;
                if (function.isDistinct() || function.isAllColumns())
                {
                    buffer.Append(")");
                }
            }

            if (function.getAttribute() != null)
            {
                buffer.Append(".").Append(function.getAttribute());
            }

            if (function.IsEscaped())
            {
                buffer.Append("}");
            }
        }

        public void Visit(ExpressionList expressionList)
        {
            if (useBracketsInExprList)
            {
                buffer.Append("(");
            }
            foreach (var expression in expressionList.getExpressions())
            {
                expression.Accept(this);
                if (!(expression == expressionList.getExpressions().Last()))
                {
                    buffer.Append(", ");
                }
            }
            if (useBracketsInExprList)
            {
                buffer.Append(")");
            }
        }

        public ISelectVisitor getSelectVisitor()
        {
            return selectVisitor;
        }

        public void setSelectVisitor(ISelectVisitor visitor)
        {
            selectVisitor = visitor;
        }

        public void Visit(DateValue dateValue)
        {
            buffer.Append("{d '").Append(dateValue.getValue().ToString()).Append("'}");
        }

        public void Visit(TimestampValue timestampValue)
        {
            buffer.Append("{ts '").Append(timestampValue.getValue().ToString()).Append("'}");
        }

        public void Visit(TimeValue timeValue)
        {
            buffer.Append("{t '").Append(timeValue.getValue().ToString()).Append("'}");
        }

        public void Visit(CaseExpression caseExpression)
        {
            buffer.Append("CASE ");
            IExpression switchExp = caseExpression.getSwitchExpression();
            if (switchExp != null)
            {
                switchExp.Accept(this);
                buffer.Append(" ");
            }

            foreach (IExpression exp in caseExpression.getWhenClauses())
            {
                exp.Accept(this);
            }

            IExpression elseExp = caseExpression.getElseExpression();
            if (elseExp != null)
            {
                buffer.Append("ELSE ");
                elseExp.Accept(this);
                buffer.Append(" ");
            }

            buffer.Append("END");
        }

        public void Visit(WhenClause whenClause)
        {
            buffer.Append("WHEN ");
            whenClause.getWhenExpression().Accept(this);
            buffer.Append(" THEN ");
            whenClause.getThenExpression().Accept(this);
            buffer.Append(" ");
        }

        public void Visit(AllComparisonExpression allComparisonExpression)
        {
            buffer.Append(" ALL ");
            allComparisonExpression.getSubSelect().Accept((IExpressionVisitor)this);
        }

        public void Visit(AnyComparisonExpression anyComparisonExpression)
        {
            buffer.Append(" ANY ");
            anyComparisonExpression.getSubSelect().Accept((IExpressionVisitor)this);
        }

        public void Visit(Concat concat)
        {
            visitBinaryExpression(concat, " || ");
        }

        public void Visit(Matches matches)
        {
            visitOldOracleJoinBinaryExpression(matches, " @@ ");
        }

        public void Visit(BitwiseAnd bitwiseAnd)
        {
            visitBinaryExpression(bitwiseAnd, " & ");
        }

        public void Visit(BitwiseOr bitwiseOr)
        {
            visitBinaryExpression(bitwiseOr, " | ");
        }

        public void Visit(BitwiseXor bitwiseXor)
        {
            visitBinaryExpression(bitwiseXor, " ^ ");
        }

        public void Visit(CastExpression cast)
        {
            if (cast.isUseCastKeyword())
            {
                buffer.Append("CAST(");
                buffer.Append(cast.getLeftExpression());
                buffer.Append(" AS ");
                buffer.Append(cast.getType());
                buffer.Append(")");
            }
            else
            {
                buffer.Append(cast.getLeftExpression());
                buffer.Append("::");
                buffer.Append(cast.getType());
            }
        }

        public void Visit(Modulo modulo)
        {
            visitBinaryExpression(modulo, " % ");
        }

        public void Visit(AnalyticExpression aexpr)
        {
            buffer.Append(aexpr.ToString());
        }

        public void Visit(ExtractExpression eexpr)
        {
            buffer.Append(eexpr.ToString());
        }

        public void Visit(MultiExpressionList multiExprList)
        {
            foreach (var it in multiExprList.getExprList())
            {
                it.Accept(this);
                if (!(it == multiExprList.getExprList().Last()))
                {
                    buffer.Append(", ");
                }
            }
        }

        public void Visit(IntervalExpression iexpr)
        {
            buffer.Append(iexpr.ToString());
        }

        public void Visit(JdbcNamedParameter jdbcNamedParameter)
        {
            buffer.Append(jdbcNamedParameter.ToString());
        }

        public void Visit(OracleHierarchicalExpression oexpr)
        {
            buffer.Append(oexpr.ToString());
        }

        public void Visit(RegExpMatchOperator rexpr)
        {
            visitBinaryExpression(rexpr, " " + rexpr.getStringExpression() + " ");
        }

        public void Visit(RegExpMySQLOperator rexpr)
        {
            visitBinaryExpression(rexpr, " " + rexpr.getStringExpression() + " ");
        }

        public void Visit(JsonExpression jsonExpr)
        {
            buffer.Append(jsonExpr.ToString());
        }

    }
}
