/**
 * This is a container for a values item within a select statement. It holds
 * some syntactical stuff that differs from values within an insert statement.
 *
 * @author Paratoner
 */

using System.Collections.Generic;
using System.Linq;
using System.Text;
using NSqlParser.Expression;
using NSqlParser.Expression.Operators.Relational;

namespace NSqlParser.Statement.Select
{
    public class ValuesList : IFromItem
    {

        private Alias alias;
        private MultiExpressionList multiExpressionList;
        private bool noBrackets = false;
        private List<string> columnNames;

        public ValuesList()
        {
        }

        public ValuesList(MultiExpressionList multiExpressionList)
        {
            this.multiExpressionList = multiExpressionList;
        }

        public void Accept(IFromItemVisitor fromItemVisitor)
        {
            fromItemVisitor.Visit(this);
        }

        public Alias getAlias()
        {
            return alias;
        }

        public void setAlias(Alias alias)
        {
            this.alias = alias;
        }

        public Pivot getPivot()
        {
            return null;
        }

        public void setPivot(Pivot pivot)
        {
        }

        public MultiExpressionList getMultiExpressionList()
        {
            return multiExpressionList;
        }

        public void setMultiExpressionList(MultiExpressionList multiExpressionList)
        {
            this.multiExpressionList = multiExpressionList;
        }

        public bool isNoBrackets()
        {
            return noBrackets;
        }

        public void setNoBrackets(bool noBrackets)
        {
            this.noBrackets = noBrackets;
        }

        public override string ToString()
        {
            StringBuilder b = new StringBuilder();

            b.Append("(VALUES ");
            var list = getMultiExpressionList().getExprList();
            foreach (var it in list)
            {
                b.Append(PlainSelect.getStringList(it.getExpressions(), true, !isNoBrackets()));
                if (!(it == list.Last()))
                    b.Append(", ");
            }
            b.Append(")");
            if (alias != null)
            {
                b.Append(alias.ToString());

                if (columnNames != null)
                {
                    b.Append("(");
                    foreach (var it in columnNames)
                    {
                        b.Append(it);
                        if (!(it == columnNames.Last()))
                        {
                            b.Append(", ");
                        }
                    }

                    b.Append(")");
                }
            }
            return b.ToString();
        }

        public List<string> getColumnNames()
        {
            return columnNames;
        }

        public void setColumnNames(List<string> columnNames)
        {
            this.columnNames = columnNames;
        }
    }
}
