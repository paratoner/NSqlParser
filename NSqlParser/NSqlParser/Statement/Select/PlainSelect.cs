using System.Collections.Generic;
using System.Linq;
using System.Text;
using NSqlParser.Expression;
using NSqlParser.Schema;

namespace NSqlParser.Statement.Select
{
    public class PlainSelect : ISelectBody
    {

        private Distinct distinct = null;
        private List<ISelectItem> selectItems;
        private List<Table> intoTables;
        private IFromItem fromItem;
        private List<Join> joins;
        private IExpression where;
        private List<IExpression> groupByColumnReferences;
        private List<OrderByElement> orderByElements;
        private IExpression having;
        private Limit limit;
        private Offset offset;
        private Fetch fetch;
        private Top top;
        private OracleHierarchicalExpression oracleHierarchical = null;
        private bool oracleSiblings = false;
        private bool forUpdate = false;

        /**
     * The {@link FromItem} in this query
     *
     * @return the {@link FromItem}
     */
        public IFromItem getFromItem()
        {
            return fromItem;
        }

        public List<Table> getIntoTables()
        {
            return intoTables;
        }

        /**
     * The {@link SelectItem}s in this query (for example the A,B,C in "SELECT
     * A,B,C")
     *
     * @return a list of {@link SelectItem}s
     */
        public List<ISelectItem> getSelectItems()
        {
            return selectItems;
        }

        public IExpression getWhere()
        {
            return where;
        }

        public void setFromItem(IFromItem item)
        {
            fromItem = item;
        }

        public void setIntoTables(List<Table> intoTables)
        {
            this.intoTables = intoTables;
        }

        public void setSelectItems(List<ISelectItem> list)
        {
            selectItems = list;
        }

        public void addSelectItems(ISelectItem[] items)
        {
            if (selectItems == null)
            {
                selectItems = new List<ISelectItem>();
            }
            selectItems.AddRange(items);
        }

        public void setWhere(IExpression where)
        {
            this.where = where;
        }

        /**
     * The list of {@link Join}s
     *
     * @return the list of {@link Join}s
     */
        public List<Join> getJoins()
        {
            return joins;
        }

        public void setJoins(List<Join> list)
        {
            joins = list;
        }

        public void Accept(ISelectVisitor selectVisitor)
        {
            selectVisitor.Visit(this);
        }

        public List<OrderByElement> getOrderByElements()
        {
            return orderByElements;
        }

        public void setOrderByElements(List<OrderByElement> orderByElements)
        {
            this.orderByElements = orderByElements;
        }

        public Limit getLimit()
        {
            return limit;
        }

        public void setLimit(Limit limit)
        {
            this.limit = limit;
        }

        public Offset getOffset()
        {
            return offset;
        }

        public void setOffset(Offset offset)
        {
            this.offset = offset;
        }

        public Fetch getFetch()
        {
            return fetch;
        }

        public void setFetch(Fetch fetch)
        {
            this.fetch = fetch;
        }

        public Top getTop()
        {
            return top;
        }

        public void setTop(Top top)
        {
            this.top = top;
        }

        public Distinct getDistinct()
        {
            return distinct;
        }

        public void setDistinct(Distinct distinct)
        {
            this.distinct = distinct;
        }

        public IExpression getHaving()
        {
            return having;
        }

        public void setHaving(IExpression expression)
        {
            having = expression;
        }

        /**
     * A list of {@link Expression}s of the GROUP BY clause. It is null in case
     * there is no GROUP BY clause
     *
     * @return a list of {@link Expression}s
     */
        public List<IExpression> getGroupByColumnReferences()
        {
            return groupByColumnReferences;
        }

        public void setGroupByColumnReferences(List<IExpression> list)
        {
            groupByColumnReferences = list;
        }

        public void addGroupByColumnReference(IExpression expr)
        {
            if (groupByColumnReferences == null)
            {
                groupByColumnReferences = new List<IExpression>();
            }
            groupByColumnReferences.Add(expr);
        }

        public OracleHierarchicalExpression getOracleHierarchical()
        {
            return oracleHierarchical;
        }

        public void setOracleHierarchical(OracleHierarchicalExpression oracleHierarchical)
        {
            this.oracleHierarchical = oracleHierarchical;
        }

        public bool isOracleSiblings()
        {
            return oracleSiblings;
        }

        public void setOracleSiblings(bool oracleSiblings)
        {
            this.oracleSiblings = oracleSiblings;
        }

        public bool isForUpdate()
        {
            return forUpdate;
        }

        public void setForUpdate(bool forUpdate)
        {
            this.forUpdate = forUpdate;
        }

        public override string ToString()
        {
            StringBuilder sql = new StringBuilder("SELECT ");
            if (distinct != null)
            {
                sql.Append(distinct).Append(" ");
            }
            if (top != null)
            {
                sql.Append(top).Append(" ");
            }
            sql.Append(getStringList(selectItems));

            if (intoTables != null)
            {
                sql.Append(" INTO ");
                foreach (var table in intoTables)
                {
                    sql.Append(table.ToString());
                    if (!(table == intoTables.Last()))
                        sql.Append(", ");
                }
            }

            if (fromItem != null)
            {
                sql.Append(" FROM ").Append(fromItem);
                if (joins != null)
                {
                    foreach (var join in joins)
                    {
                        if (join.isSimple())
                        {
                            sql.Append(", ").Append(join);
                        }
                        else
                        {
                            sql.Append(" ").Append(join);
                        }
                    }
                }
                // sql += getFormatedList(joins, "", false, false);
                if (where != null)
                {
                    sql.Append(" WHERE ").Append(where);
                }
                if (oracleHierarchical != null)
                {
                    sql.Append(oracleHierarchical.ToString());
                }
                sql.Append(getFormatedList(groupByColumnReferences, "GROUP BY"));
                if (having != null)
                {
                    sql.Append(" HAVING ").Append(having);
                }
                sql.Append(orderByToString(oracleSiblings, orderByElements));
                if (limit != null)
                {
                    sql.Append(limit);
                }
                if (offset != null)
                {
                    sql.Append(offset);
                }
                if (fetch != null)
                {
                    sql.Append(fetch);
                }
                if (isForUpdate())
                {
                    sql.Append(" FOR UPDATE");
                }
            }
            return sql.ToString();
        }

        public static string orderByToString(List<OrderByElement> orderByElements)
        {
            return orderByToString(false, orderByElements);
        }

        public static string orderByToString(bool oracleSiblings, List<OrderByElement> orderByElements)
        {
            return getFormatedList(orderByElements, oracleSiblings ? "ORDER SIBLINGS BY" : "ORDER BY");
        }

        public static string getFormatedList<T>(List<T> list, string expression)
        {
            return getFormatedList(list, expression, true, false);
        }

        public static string getFormatedList<T>(List<T> list, string expression, bool useComma, bool useBrackets)
        {
            string sql = getStringList(list, useComma, useBrackets);

            if (sql.Length > 0)
            {
                if (expression.Length > 0)
                {
                    sql = " " + expression + " " + sql;
                }
                else
                {
                    sql = " " + sql;
                }
            }

            return sql;
        }

        /**
     * List the ToString out put of the objects in the List comma separated. If
     * the List is null or empty an empty string is returned.
     *
     * The same as getStringList(list, true, false)
     *
     * @see #getStringList(List, bool, bool)
     * @param list list of objects with ToString methods
     * @return comma separated list of the elements in the list
     */
        public static string getStringList<T>(List<T> list)
        {
            return getStringList(list, true, false);
        }

        /**
     * List the ToString out put of the objects in the List that can be comma
     * separated. If the List is null or empty an empty string is returned.
     *
     * @param list list of objects with ToString methods
     * @param useComma true if the list has to be comma separated
     * @param useBrackets true if the list has to be enclosed in brackets
     * @return comma separated list of the elements in the list
     */
        public static string getStringList<T>(List<T> list, bool useComma, bool useBrackets)
        {
            string ans = "";
            string comma = ",";
            if (!useComma)
            {
                comma = "";
            }
            if (list != null)
            {
                if (useBrackets)
                {
                    ans += "(";
                }

                for (int i = 0; i < list.Count; i++)
                {
                    ans += "" + list[i] + ((i < list.Count - 1) ? comma + " " : "");
                }

                if (useBrackets)
                {
                    ans += ")";
                }
            }

            return ans;
        }
    }
}
