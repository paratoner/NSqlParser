/**
 * Analytic function. The name of the function is variable but the parameters
 * following the special analytic function path. e.g. row_number() over (order
 * by test). Additional there can be an expression for an analytical aggregate
 * like sum(col) or the "all collumns" wildcard like count(*).
 *
 * @author Paratoner
 */

using System.Collections.Generic;
using System.Linq;
using System.Text;
using NSqlParser.Expression.Operators.Relational;
using NSqlParser.Statement.Select;

namespace NSqlParser.Expression
{
    public class AnalyticExpression : IExpression
    {

        //private List<Column> partitionByColumns;
        private ExpressionList partitionExpressionList;
        private List<OrderByElement> orderByElements;
        private string name;
        private IExpression expression;
        private IExpression offset;
        private IExpression defaultValue;
        private bool allColumns = false;
        private WindowElement windowElement;


        public void Accept(IExpressionVisitor expressionVisitor)
        {
            expressionVisitor.Visit(this);
        }

        public List<OrderByElement> getOrderByElements()
        {
            return orderByElements;
        }

        public void setOrderByElements(List<OrderByElement> orderByElements)
        {
            this.orderByElements = orderByElements;
        }

        //	public List<Column> getPartitionByColumns() {
        //		return partitionByColumns;
        //	}
        //
        //	public void setPartitionByColumns(List<Column> partitionByColumns) {
        //		this.partitionByColumns = partitionByColumns;
        //	}

        public ExpressionList getPartitionExpressionList()
        {
            return partitionExpressionList;
        }

        public void setPartitionExpressionList(ExpressionList partitionExpressionList)
        {
            this.partitionExpressionList = partitionExpressionList;
        }



        public string getName()
        {
            return name;
        }

        public void setName(string name)
        {
            this.name = name;
        }

        public IExpression getExpression()
        {
            return expression;
        }

        public void setExpression(IExpression expression)
        {
            this.expression = expression;
        }

        public IExpression getOffset()
        {
            return offset;
        }

        public void setOffset(IExpression offset)
        {
            this.offset = offset;
        }

        public IExpression getDefaultValue()
        {
            return defaultValue;
        }

        public void setDefaultValue(IExpression defaultValue)
        {
            this.defaultValue = defaultValue;
        }

        public WindowElement getWindowElement()
        {
            return windowElement;
        }

        public void setWindowElement(WindowElement windowElement)
        {
            this.windowElement = windowElement;
        }


        public override string ToString()
        {
            StringBuilder b = new StringBuilder();

            b.Append(name).Append("(");
            if (expression != null)
            {
                b.Append(expression.ToString());
                if (offset != null)
                {
                    b.Append(", ").Append(offset.ToString());
                    if (defaultValue != null)
                    {
                        b.Append(", ").Append(defaultValue.ToString());
                    }
                }
            }
            else if (isAllColumns())
            {
                b.Append("*");
            }
            b.Append(") OVER (");

            toStringPartitionBy(b);
            toStringOrderByElements(b);

            b.Append(")");

            return b.ToString();
        }

        public bool isAllColumns()
        {
            return allColumns;
        }

        public void setAllColumns(bool allColumns)
        {
            this.allColumns = allColumns;
        }

        private void toStringPartitionBy(StringBuilder b)
        {
            if (partitionExpressionList != null && partitionExpressionList.getExpressions().Any())
            {
                b.Append("PARTITION BY ");
                b.Append(PlainSelect.getStringList(partitionExpressionList.getExpressions(), true, false));
                b.Append(" ");
            }
        }

        private void toStringOrderByElements(StringBuilder b)
        {
            if (orderByElements != null && orderByElements.Any())
            {
                b.Append("ORDER BY ");
                for (int i = 0; i < orderByElements.Count; i++)
                {
                    if (i > 0)
                    {
                        b.Append(", ");
                    }
                    b.Append(orderByElements[i].ToString());
                }

                if (windowElement != null)
                {
                    b.Append(' ');
                    b.Append(windowElement);
                }
            }
        }
    }
}
