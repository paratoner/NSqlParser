/**
 * An element (column reference) in an "ORDER BY" clause.
 */

using System.Text;
using NSqlParser.Expression;

namespace NSqlParser.Statement.Select
{
    public class OrderByElement {

        public enum NullOrdering {

            NULLS_FIRST,
            NULLS_LAST
        }

        private IExpression expression;
        private bool asc = true;
        private NullOrdering nullOrdering;
        private bool ascDesc = false;

        public bool isAsc() {
            return asc;
        }

        public NullOrdering getNullOrdering() {
            return nullOrdering;
        }

        public void setNullOrdering(NullOrdering nullOrdering) {
            this.nullOrdering = nullOrdering;
        }

        public void setAsc(bool b) {
            asc = b;
        }
    
        public void setAscDescPresent(bool b) {
            ascDesc = b;
        }

        public bool isAscDescPresent() {
            return ascDesc;
        }

        public void Accept(IOrderByVisitor orderByVisitor) {
            orderByVisitor.Visit(this);
        }

        public IExpression getExpression() {
            return expression;
        }

        public void setExpression(IExpression expression) {
            this.expression = expression;
        }

        public override string ToString() {
            StringBuilder b = new StringBuilder();
            b.Append(expression.ToString());
        
            if (!asc) {
                b.Append(" DESC");
            } else if (ascDesc) {
                b.Append(" ASC");
            }

            b.Append(' ');
            b.Append(nullOrdering == NullOrdering.NULLS_FIRST ? "NULLS FIRST" : "NULLS LAST");
            return b.ToString();
        }
    }
}
