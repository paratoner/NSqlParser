using System.Collections.Generic;
using System.Linq;
using System.Text;
using NSqlParser.Expression;
using NSqlParser.Schema;
using NSqlParser.Statement.Select;
/**
 * A class to de-parse (that is, tranform from NSqlParser hierarchy into a
 * string) a {@link NSqlParser.statement.select.Select}
 */

namespace NSqlParser.Util.Deparser
{
    public class SelectDeParser : ISelectVisitor, IOrderByVisitor, ISelectItemVisitor, IFromItemVisitor, IPivotVisitor
    {

        private StringBuilder buffer;
        private IExpressionVisitor expressionVisitor;

        public SelectDeParser()
        {
        }

        /**
     * @param expressionVisitor a {@link ExpressionVisitor} to de-parse
     * expressions. It has to share the same<br>
     * StringBuilder (buffer parameter) as this object in order to work
     * @param buffer the buffer that will be filled with the select
     */
        public SelectDeParser(IExpressionVisitor expressionVisitor, StringBuilder buffer)
        {
            this.buffer = buffer;
            this.expressionVisitor = expressionVisitor;
        }

        public void Visit(PlainSelect plainSelect)
        {
            buffer.Append("SELECT ");
            if (plainSelect.getDistinct() != null)
            {
                buffer.Append("DISTINCT ");
                if (plainSelect.getDistinct().getOnSelectItems() != null)
                {
                    buffer.Append("ON (");
                    foreach (ISelectItem selectItem in plainSelect.getDistinct().getOnSelectItems())
                    {
                        selectItem.Accept(this);
                        if (!(selectItem == plainSelect.getDistinct().getOnSelectItems().Last()))
                        {
                            buffer.Append(", ");
                        }
                    }
                    buffer.Append(") ");
                }

            }
            Top top = plainSelect.getTop();
            if (top != null)
            {
                buffer.Append(top).Append(" ");
            }
            foreach (ISelectItem selectItem in plainSelect.getSelectItems())
            {
                selectItem.Accept(this);
                if (!(selectItem == plainSelect.getSelectItems().Last()))
                {
                    buffer.Append(", ");
                }
            }


            if (plainSelect.getIntoTables() != null)
            {
                buffer.Append(" INTO ");
                foreach (var item in plainSelect.getIntoTables())
                {
                    Visit(item);
                    if (!(item == plainSelect.getIntoTables().Last()))
                    {
                        buffer.Append(", ");
                    }
                }
            }

            if (plainSelect.getFromItem() != null)
            {
                buffer.Append(" FROM ");
                plainSelect.getFromItem().Accept(this);
            }

            if (plainSelect.getJoins() != null)
            {
                foreach (Join join in plainSelect.getJoins())
                {
                    deparseJoin(join);
                }
            }

            if (plainSelect.getWhere() != null)
            {
                buffer.Append(" WHERE ");
                plainSelect.getWhere().Accept(expressionVisitor);
            }

            if (plainSelect.getOracleHierarchical() != null)
            {
                plainSelect.getOracleHierarchical().Accept(expressionVisitor);
            }

            if (plainSelect.getGroupByColumnReferences() != null)
            {
                buffer.Append(" GROUP BY ");
                foreach (IExpression columnReference in plainSelect.getGroupByColumnReferences())
                {
                    columnReference.Accept(expressionVisitor);
                    if (!(columnReference == plainSelect.getGroupByColumnReferences().Last()))
                    {
                        buffer.Append(", ");
                    }
                }

            }

            if (plainSelect.getHaving() != null)
            {
                buffer.Append(" HAVING ");
                plainSelect.getHaving().Accept(expressionVisitor);
            }

            if (plainSelect.getOrderByElements() != null)
            {
                deparseOrderBy(plainSelect.isOracleSiblings(), plainSelect.getOrderByElements());
            }

            if (plainSelect.getLimit() != null)
            {
                deparseLimit(plainSelect.getLimit());
            }
            if (plainSelect.getOffset() != null)
            {
                deparseOffset(plainSelect.getOffset());
            }
            if (plainSelect.getFetch() != null)
            {
                deparseFetch(plainSelect.getFetch());
            }
            if (plainSelect.isForUpdate())
            {
                buffer.Append(" FOR UPDATE");
            }

        }

        public void Visit(OrderByElement orderBy)
        {
            orderBy.getExpression().Accept(expressionVisitor);
            if (!orderBy.isAsc())
            {
                buffer.Append(" DESC");
            }
            else if (orderBy.isAscDescPresent())
            {
                buffer.Append(" ASC");
            }
            if (orderBy.getNullOrdering() != null)
            {
                buffer.Append(' ');
                buffer.Append(orderBy.getNullOrdering() == OrderByElement.NullOrdering.NULLS_FIRST ? "NULLS FIRST" : "NULLS LAST");
            }
        }

        public void Visit(Column column)
        {
            buffer.Append(column.GetFullyQualifiedName());
        }

        public void Visit(AllTableColumns allTableColumns)
        {
            buffer.Append(allTableColumns.getTable().GetFullyQualifiedName()).Append(".*");
        }

        public void Visit(SelectExpressionItem selectExpressionItem)
        {
            selectExpressionItem.getExpression().Accept(expressionVisitor);
            if (selectExpressionItem.getAlias() != null)
            {
                buffer.Append(selectExpressionItem.getAlias().ToString());
            }

        }

        public void Visit(SubSelect subSelect)
        {
            buffer.Append("(");
            if (subSelect.getWithItemsList() != null && subSelect.getWithItemsList().Count>0)
            {
                buffer.Append("WITH ");
                foreach (WithItem withItem in subSelect.getWithItemsList())
                {
                    withItem.Accept(this);
                    if (!(withItem == subSelect.getWithItemsList().Last()))
                    {
                        buffer.Append(",");
                    }
                    buffer.Append(" ");
                }

            }
            subSelect.getSelectBody().Accept(this);
            buffer.Append(")");
            Pivot pivot = subSelect.getPivot();
            if (pivot != null)
            {
                pivot.Accept(this);
            }
            Alias alias = subSelect.getAlias();
            if (alias != null)
            {
                buffer.Append(alias.ToString());
            }
        }

        public void Visit(Table tableName)
        {
            buffer.Append(tableName.GetFullyQualifiedName());
            Pivot pivot = tableName.getPivot();
            if (pivot != null)
            {
                pivot.Accept(this);
            }
            Alias alias = tableName.getAlias();
            if (alias != null)
            {
                buffer.Append(alias);
            }
        }

        public void Visit(Pivot pivot)
        {
            List<Column> forColumns = pivot.getForColumns();
            buffer.Append(" PIVOT (")
                .Append(PlainSelect.getStringList(pivot.getFunctionItems()))
                .Append(" FOR ")
                .Append(PlainSelect.getStringList(forColumns, true, forColumns != null && forColumns.Count > 1))
                .Append(" IN ")
                .Append(PlainSelect.getStringList(pivot.getSingleInItems(), true, true))
                .Append(")");
        }

        public void Visit(PivotXml pivot)
        {
            List<Column> forColumns = pivot.getForColumns();
            buffer.Append(" PIVOT XML (")
                .Append(PlainSelect.getStringList(pivot.getFunctionItems()))
                .Append(" FOR ")
                .Append(PlainSelect.getStringList(forColumns, true, forColumns != null && forColumns.Count > 1))
                .Append(" IN (");
            if (pivot.isInAny())
            {
                buffer.Append("ANY");
            }
            else if (pivot.getInSelect() != null)
            {
                buffer.Append(pivot.getInSelect());
            }
            else
            {
                buffer.Append(PlainSelect.getStringList(pivot.getSingleInItems()));
            }
            buffer.Append("))");
        }

        public void deparseOrderBy(List<OrderByElement> orderByElements)
        {
            deparseOrderBy(false, orderByElements);
        }

        public void deparseOrderBy(bool oracleSiblings, List<OrderByElement> orderByElements)
        {
            if (oracleSiblings)
            {
                buffer.Append(" ORDER SIBLINGS BY ");
            }
            else
            {
                buffer.Append(" ORDER BY ");
            }
            foreach (OrderByElement orderByElement in orderByElements)
            {
                orderByElement.Accept(this);
                if (!(orderByElement == orderByElements.Last()))
                {
                    buffer.Append(", ");
                }
            }

        }

        public void deparseLimit(Limit limit)
        {
            // LIMIT n OFFSET skip
            if (limit.isRowCountJdbcParameter())
            {
                buffer.Append(" LIMIT ");
                buffer.Append("?");
            }
            else if (limit.getRowCount() >= 0)
            {
                buffer.Append(" LIMIT ");
                buffer.Append(limit.getRowCount());
            }
            else if (limit.isLimitNull())
            {
                buffer.Append(" LIMIT NULL");
            }

            if (limit.isOffsetJdbcParameter())
            {
                buffer.Append(" OFFSET ?");
            }
            else if (limit.getOffset() != 0)
            {
                buffer.Append(" OFFSET ").Append(limit.getOffset());
            }

        }

        public void deparseOffset(Offset offset)
        {
            // OFFSET offset
            // or OFFSET offset (ROW | ROWS)
            if (offset.isOffsetJdbcParameter())
            {
                buffer.Append(" OFFSET ?");
            }
            else if (offset.getOffset() != 0)
            {
                buffer.Append(" OFFSET ");
                buffer.Append(offset.getOffset());
            }
            if (offset.getOffsetParam() != null)
            {
                buffer.Append(" ").Append(offset.getOffsetParam());
            }

        }

        public void deparseFetch(Fetch fetch)
        {
            // FETCH (FIRST | NEXT) row_count (ROW | ROWS) ONLY
            buffer.Append(" FETCH ");
            if (fetch.IsFetchParamFirst())
            {
                buffer.Append("FIRST ");
            }
            else
            {
                buffer.Append("NEXT ");
            }
            if (fetch.isFetchJdbcParameter())
            {
                buffer.Append("?");
            }
            else
            {
                buffer.Append(fetch.getRowCount());
            }
            buffer.Append(" ").Append(fetch.getFetchParam()).Append(" ONLY");

        }

        public StringBuilder getBuffer()
        {
            return buffer;
        }

        public void setBuffer(StringBuilder buffer)
        {
            this.buffer = buffer;
        }

        public IExpressionVisitor getExpressionVisitor()
        {
            return expressionVisitor;
        }

        public void setExpressionVisitor(IExpressionVisitor visitor)
        {
            expressionVisitor = visitor;
        }

        public void Visit(SubJoin subjoin)
        {
            buffer.Append("(");
            subjoin.getLeft().Accept(this);
            deparseJoin(subjoin.getJoin());
            buffer.Append(")");

            if (subjoin.getPivot() != null)
            {
                subjoin.getPivot().Accept(this);
            }
        }

        public void deparseJoin(Join join)
        {
            if (join.isSimple())
            {
                buffer.Append(", ");
            }
            else
            {

                if (join.isRight())
                {
                    buffer.Append(" RIGHT");
                }
                else if (join.isNatural())
                {
                    buffer.Append(" NATURAL");
                }
                else if (join.isFull())
                {
                    buffer.Append(" FULL");
                }
                else if (join.isLeft())
                {
                    buffer.Append(" LEFT");
                }
                else if (join.isCross())
                {
                    buffer.Append(" CROSS");
                }

                if (join.isOuter())
                {
                    buffer.Append(" OUTER");
                }
                else if (join.isInner())
                {
                    buffer.Append(" INNER");
                }

                buffer.Append(" JOIN ");

            }

            IFromItem fromItem = join.getRightItem();
            fromItem.Accept(this);
            if (join.getOnExpression() != null)
            {
                buffer.Append(" ON ");
                join.getOnExpression().Accept(expressionVisitor);
            }
            if (join.getUsingColumns() != null)
            {
                buffer.Append(" USING (");
                foreach (Column column in join.getUsingColumns())
                {
                    buffer.Append(column.GetFullyQualifiedName());
                    if (!(column == join.getUsingColumns().Last()))
                    {
                        buffer.Append(", ");
                    }
                }
                buffer.Append(")");
            }

        }

        public void Visit(SetOperationList list)
        {
            for (int i = 0; i < list.getPlainSelects().Count; i++)
            {
                if (i != 0)
                {
                    buffer.Append(' ').Append(list.getOperations()[i - 1]).Append(' ');
                }
                buffer.Append("(");
                PlainSelect plainSelect = list.getPlainSelects()[i];
                plainSelect.Accept(this);
                buffer.Append(")");
            }
            if (list.getOrderByElements() != null)
            {
                deparseOrderBy(list.getOrderByElements());
            }

            if (list.getLimit() != null)
            {
                deparseLimit(list.getLimit());
            }
            if (list.getOffset() != null)
            {
                deparseOffset(list.getOffset());
            }
            if (list.getFetch() != null)
            {
                deparseFetch(list.getFetch());
            }
        }

        public void Visit(WithItem withItem)
        {
            buffer.Append(withItem.getName());
            if (withItem.getWithItemList() != null)
            {
                buffer.Append(" ").Append(PlainSelect.getStringList(withItem.getWithItemList(), true, true));
            }
            buffer.Append(" AS (");
            withItem.getSelectBody().Accept(this);
            buffer.Append(")");
        }

        public void Visit(LateralSubSelect lateralSubSelect)
        {
            buffer.Append(lateralSubSelect.ToString());
        }

        public void Visit(ValuesList valuesList)
        {
            buffer.Append(valuesList.ToString());
        }

        public void Visit(AllColumns allColumns)
        {
            buffer.Append('*');
        }
    }
}
