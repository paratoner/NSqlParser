using System.Collections.Generic;
using NSqlParser.Expression;
using NSqlParser.Expression.Operators.Arithmetic;
using NSqlParser.Expression.Operators.Conditional;
using NSqlParser.Expression.Operators.Relational;
using NSqlParser.Schema;
using NSqlParser.Statement.Delete;
using NSqlParser.Statement.Insert;
using NSqlParser.Statement.Replace;
using NSqlParser.Statement.Select;
using NSqlParser.Statement.Update;
/**
 * Find all used tables within an select statement.
 */

namespace NSqlParser.Util
{
    public class TablesNamesFinder : ISelectVisitor, IFromItemVisitor, IExpressionVisitor, IItemsListVisitor, ISelectItemVisitor {

        private List<string> tables;
        /**
     * There are special names, that are not table names but are parsed as
     * tables. These names are collected here and are not included in the tables
     * - names anymore.
     */
        private List<string> otherItemNames;

        /**
     * Main entry for this Tool class. A list of found tables is returned.
     *
     * @param delete
     * @return
     */
        public List<string> getTableList(Delete delete) {
            init();
            tables.Add(delete.getTable().getName());
            if (delete.getWhere() != null) {
                delete.getWhere().Accept(this);
            }

            return tables;
        }

        /**
     * Main entry for this Tool class. A list of found tables is returned.
     *
     * @param insert
     * @return
     */
        public List<string> getTableList(Insert insert) {
            init();
            tables.Add(insert.getTable().getName());
            if (insert.getItemsList() != null) {
                insert.getItemsList().Accept(this);
            }

            return tables;
        }

        /**
     * Main entry for this Tool class. A list of found tables is returned.
     *
     * @param replace
     * @return
     */
        public List<string> getTableList(Replace replace) {
            init();
            tables.Add(replace.getTable().getName());
            if (replace.getExpressions() != null) {
                foreach (IExpression expression in replace.getExpressions()) {
                    expression.Accept(this);
                }
            }
            if (replace.getItemsList() != null) {
                replace.getItemsList().Accept(this);
            }

            return tables;
        }

        /**
     * Main entry for this Tool class. A list of found tables is returned.
     *
     * @param select
     * @return
     */
        public List<string> getTableList(Select select) {
            init();
            if (select.getWithItemsList() != null) {
                foreach (WithItem withItem in select.getWithItemsList()) {
                    withItem.Accept(this);
                }
            }
            select.getSelectBody().Accept(this);

            return tables;
        }

        /**
     * Main entry for this Tool class. A list of found tables is returned.
     *
     * @param update
     * @return
     */
        public List<string> getTableList(Update update) {
            init();
            foreach (Table table in update.getTables()) {
                tables.Add(table.getName());
            }
            if (update.getExpressions() != null) {
                foreach (IExpression expression in update.getExpressions()) {
                    expression.Accept(this);
                }
            }

            if (update.getFromItem() != null) {
                update.getFromItem().Accept(this);
            }

            if (update.getJoins() != null) {
                foreach (Join join in update.getJoins()) {
                    join.getRightItem().Accept(this);
                }
            }

            if (update.getWhere() != null) {
                update.getWhere().Accept(this);
            }

            return tables;
        }

        public void Visit(WithItem withItem) {
            otherItemNames.Add(withItem.getName().ToLower());
            withItem.getSelectBody().Accept(this);
        }

        public void Visit(PlainSelect plainSelect) {
            if (plainSelect.getSelectItems() != null) {
                foreach (ISelectItem item in plainSelect.getSelectItems()) {
                    item.Accept(this);
                }
            }

            plainSelect.getFromItem().Accept(this);

            if (plainSelect.getJoins() != null) {
                foreach (Join join in plainSelect.getJoins()) {
                    join.getRightItem().Accept(this);
                }
            }
            if (plainSelect.getWhere() != null) {
                plainSelect.getWhere().Accept(this);
            }
            if (plainSelect.getOracleHierarchical() != null) {
                plainSelect.getOracleHierarchical().Accept(this);
            }
        }

        public void Visit(Table tableName) {
            string tableWholeName = tableName.GetFullyQualifiedName();
            if (!otherItemNames.Contains(tableWholeName.ToLower())
                && !tables.Contains(tableWholeName)) {
                    tables.Add(tableWholeName);
                }
        }

        public void Visit(SubSelect subSelect) {
            subSelect.getSelectBody().Accept(this);
        }

        public void Visit(Addition addition) {
            visitBinaryExpression(addition);
        }

        public void Visit(AndExpression andExpression) {
            visitBinaryExpression(andExpression);
        }

        public void Visit(Between between) {
            between.getLeftExpression().Accept(this);
            between.getBetweenExpressionStart().Accept(this);
            between.getBetweenExpressionEnd().Accept(this);
        }

        public void Visit(Column tableColumn) {
        }

        public void Visit(Division division) {
            visitBinaryExpression(division);
        }

        public void Visit(DoubleValue doubleValue) {
        }

        public void Visit(EqualsTo equalsTo) {
            visitBinaryExpression(equalsTo);
        }

        public void Visit(Function function) {
        }

        public void Visit(GreaterThan greaterThan) {
            visitBinaryExpression(greaterThan);
        }

        public void Visit(GreaterThanEquals greaterThanEquals) {
            visitBinaryExpression(greaterThanEquals);
        }

        public void Visit(InExpression inExpression) {
            inExpression.getLeftExpression().Accept(this);
            inExpression.getRightItemsList().Accept(this);
        }

        public void Visit(SignedExpression signedExpression) {
            signedExpression.getExpression().Accept(this);
        }

        public void Visit(IsNullExpression isNullExpression) {
        }

        public void Visit(JdbcParameter jdbcParameter) {
        }

        public void Visit(LikeExpression likeExpression) {
            visitBinaryExpression(likeExpression);
        }

        public void Visit(ExistsExpression existsExpression) {
            existsExpression.getRightExpression().Accept(this);
        }

        public void Visit(LongValue longValue) {
        }

        public void Visit(MinorThan minorThan) {
            visitBinaryExpression(minorThan);
        }

        public void Visit(MinorThanEquals minorThanEquals) {
            visitBinaryExpression(minorThanEquals);
        }

        public void Visit(Multiplication multiplication) {
            visitBinaryExpression(multiplication);
        }

        public void Visit(NotEqualsTo notEqualsTo) {
            visitBinaryExpression(notEqualsTo);
        }

        public void Visit(NullValue nullValue) {
        }

        public void Visit(OrExpression orExpression) {
            visitBinaryExpression(orExpression);
        }

        public void Visit(Parenthesis parenthesis) {
            parenthesis.getExpression().Accept(this);
        }

        public void Visit(StringValue stringValue) {
        }

        public void Visit(Subtraction subtraction) {
            visitBinaryExpression(subtraction);
        }

        public void visitBinaryExpression(BinaryExpression binaryExpression) {
            binaryExpression.getLeftExpression().Accept(this);
            binaryExpression.getRightExpression().Accept(this);
        }

        public void Visit(ExpressionList expressionList) {
            foreach (IExpression expression in expressionList.getExpressions()) {
                expression.Accept(this);
            }

        }

        public void Visit(DateValue dateValue) {
        }

        public void Visit(TimestampValue timestampValue) {
        }

        public void Visit(TimeValue timeValue) {
        }

        /*
     * (non-Javadoc)
     *
     * @see net.sf.jsqlparser.expression.ExpressionVisitor#Visit(net.sf.jsqlparser.expression.CaseExpression)
     */
        public void Visit(CaseExpression caseExpression) {
        }

        /*
     * (non-Javadoc)
     *
     * @see net.sf.jsqlparser.expression.ExpressionVisitor#Visit(net.sf.jsqlparser.expression.WhenClause)
     */
        public void Visit(WhenClause whenClause) {
        }

        public void Visit(AllComparisonExpression allComparisonExpression) {
            allComparisonExpression.getSubSelect().getSelectBody().Accept(this);
        }

        public void Visit(AnyComparisonExpression anyComparisonExpression) {
            anyComparisonExpression.getSubSelect().getSelectBody().Accept(this);
        }

        public void Visit(SubJoin subjoin) {
            subjoin.getLeft().Accept(this);
            subjoin.getJoin().getRightItem().Accept(this);
        }

        public void Visit(Concat concat) {
            visitBinaryExpression(concat);
        }

        public void Visit(Matches matches) {
            visitBinaryExpression(matches);
        }

        public void Visit(BitwiseAnd bitwiseAnd) {
            visitBinaryExpression(bitwiseAnd);
        }

        public void Visit(BitwiseOr bitwiseOr) {
            visitBinaryExpression(bitwiseOr);
        }

        public void Visit(BitwiseXor bitwiseXor) {
            visitBinaryExpression(bitwiseXor);
        }

        public void Visit(CastExpression cast) {
            cast.getLeftExpression().Accept(this);
        }

        public void Visit(Modulo modulo) {
            visitBinaryExpression(modulo);
        }

        public void Visit(AnalyticExpression analytic) {
        }

        public void Visit(SetOperationList list) {
            foreach (PlainSelect plainSelect in list.getPlainSelects()) {
                Visit(plainSelect);
            }
        }

        public void Visit(ExtractExpression eexpr) {
        }

        public void Visit(LateralSubSelect lateralSubSelect) {
            lateralSubSelect.getSubSelect().getSelectBody().Accept(this);
        }

        public void Visit(MultiExpressionList multiExprList) {
            foreach (ExpressionList exprList in multiExprList.getExprList()) {
                exprList.Accept(this);
            }
        }

        public void Visit(ValuesList valuesList) {
        }

        private void init() {
            otherItemNames = new List<string>();
            tables = new List<string>();
        }

        public void Visit(IntervalExpression iexpr) {
        }

        public void Visit(JdbcNamedParameter jdbcNamedParameter) {
        }

        public void Visit(OracleHierarchicalExpression oexpr) {
            if (oexpr.getStartExpression() != null) {
                oexpr.getStartExpression().Accept(this);
            }

            if (oexpr.getConnectExpression() != null) {
                oexpr.getConnectExpression().Accept(this);
            }
        }

        public void Visit(RegExpMatchOperator rexpr) {
            visitBinaryExpression(rexpr);
        }

        public void Visit(RegExpMySQLOperator rexpr) {
            visitBinaryExpression(rexpr);
        }

        public void Visit(JsonExpression jsonExpr) {
        }

        public void Visit(AllColumns allColumns) {
        }

        public void Visit(AllTableColumns allTableColumns) {
        }

        public void Visit(SelectExpressionItem item) {
            item.getExpression().Accept(this);
        }
    }
}
