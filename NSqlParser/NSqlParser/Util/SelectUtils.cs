using System;
using System.Collections.Generic;
using NSqlParser.Expression;
using NSqlParser.Parser;
using NSqlParser.Schema;
using NSqlParser.Statement.Select;
/**
 * Utility function for select statements.
 *
 * @author Paratoner
 */

namespace NSqlParser.Util
{
    public sealed class SelectUtils
    {

        private SelectUtils()
        {
        }

        /**
     * Builds select expr1, expr2 from table.
     * @param table
     * @param expr
     * @return 
     */
        public static Select buildSelectFromTableAndExpressions(Table table, IExpression[] expr)
        {
            List<ISelectItem> list = new List<ISelectItem>(expr.Length);
            for (int i = 0; i < expr.Length; i++)
            {
                list[i] = new SelectExpressionItem(expr[i]);
            }
            return buildSelectFromTableAndSelectItems(table, list.ToArray());
        }

        /**
     * Builds select expr1, expr2 from table.
     * @param table
     * @param expr
     * @return 
     * @throws net.sf.jsqlparser.JSQLParserException 
     */
        public static Select buildSelectFromTableAndExpressions(Table table, string[] expr)
        {
            List<ISelectItem> list = new List<ISelectItem>(expr.Length);
            for (int i = 0; i < expr.Length; i++)
            {
                list[i] = new SelectExpressionItem(NSqlParserUtil.parseExpression(expr[i]));
            }
            return buildSelectFromTableAndSelectItems(table, list.ToArray());
        }

        public static Select buildSelectFromTableAndSelectItems(Table table, ISelectItem[] selectItems)
        {
            Select select = new Select();
            PlainSelect body = new PlainSelect();
            body.addSelectItems(selectItems);
            body.setFromItem(table);
            select.setSelectBody(body);
            return select;
        }

        /**
     * Builds select * from table.
     * @param table
     * @return 
     */
        public static Select buildSelectFromTable(Table table)
        {
            return buildSelectFromTableAndSelectItems(table, new[] { new AllColumns() });
        }

        /**
     * Adds an expression to select statements. E.g. a simple column is an
     * expression.
     *
     * @param select
     * @param expr
     */
        public static void addExpression(Select select, IExpression expr)
        {
            select.getSelectBody().Accept(new AddExpressionSelectVisitor(expr));
        }

        /**
     * Adds a simple join to a select statement. The introduced join is returned for
     * more configuration settings on it (e.g. left join, right join).
     * @param select
     * @param table
     * @param onExpression
     * @return 
     */
        public static Join addJoin(Select select, Table table, IExpression onExpression)
        {
            if (select.getSelectBody() == typeof(PlainSelect))
            {
                PlainSelect plainSelect = (PlainSelect)select.getSelectBody();
                List<Join> joins = plainSelect.getJoins();
                if (joins == null)
                {
                    joins = new List<Join>();
                    plainSelect.setJoins(joins);
                }
                Join join = new Join();
                join.setRightItem(table);
                join.setOnExpression(onExpression);
                joins.Add(join);
                return join;
            }
            throw new NotSupportedException("Not supported yet.");
        }

        /**
     * Adds group by to a plain select statement.
     * @param select
     * @param expr 
     */
        public static void addGroupBy(Select select, IExpression expr)
        {
            select.getSelectBody().Accept(new AddGroupBySelectVisitor(expr));

        }
    }
}
