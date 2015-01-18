using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NSqlParser.Expression;

namespace NSqlParser.Schema
{
    public sealed class Column : IExpression, IMultiPartName
    {

        private Table table;
        private string columnName;

        public Column()
        {
        }

        public Column(Table table, string columnName)
        {
            setTable(table);
            setColumnName(columnName);
        }

        public Column(string columnName)
        {
            setTable(null);
            setColumnName(columnName);
        }

        public Table getTable()
        {
            return table;
        }

        public void setTable(Table table)
        {
            this.table = table;
        }

        public string getColumnName()
        {
            return columnName;
        }

        public void setColumnName(string stringc)
        {
            columnName = stringc;
        }

        public string GetFullyQualifiedName()
        {
            StringBuilder fqn = new StringBuilder();

            if (table != null)
            {
                fqn.Append(table.GetFullyQualifiedName());
            }
            if (fqn.Length > 0)
            {
                fqn.Append('.');
            }
            if (columnName != null)
            {
                fqn.Append(columnName);
            }
            return fqn.ToString();
        }

        public void Accept(IExpressionVisitor expressionVisitor)
        {
            expressionVisitor.Visit(this);
        }

        public override string ToString()
        {
            return GetFullyQualifiedName();
        }

    }
}
