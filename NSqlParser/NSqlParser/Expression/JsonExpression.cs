/**
 *
 * @author toben
 */

using System.Collections.Generic;
using System.Text;
using NSqlParser.Schema;

namespace NSqlParser.Expression
{
    public class JsonExpression : IExpression
    {

        private Column column;

        private List<string> idents = new List<string>();


        public void Accept(IExpressionVisitor expressionVisitor)
        {
            expressionVisitor.Visit(this);
        }

        public Column getColumn()
        {
            return column;
        }

        public void setColumn(Column column)
        {
            this.column = column;
        }

        public List<string> getIdents()
        {
            return idents;
        }

        public void setIdents(List<string> idents)
        {
            this.idents = idents;
        }

        public void addIdent(string ident)
        {
            idents.Add(ident);
        }


        public override string ToString() {
            StringBuilder b = new StringBuilder();
            b.Append(column.ToString());
            foreach (string ident in idents) {
                b.Append("->").Append(ident);
            }
            return b.ToString();
        }
    }
}
