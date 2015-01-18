/**
 * A list of expressions, as in SELECT A FROM TAB WHERE B IN (expr1,expr2,expr3)
 */

using System.Collections.Generic;
using NSqlParser.Statement.Select;

namespace NSqlParser.Expression.Operators.Relational
{
    public class ExpressionList : IItemsList {

        private List<IExpression> expressions;

        public ExpressionList() {
        }

        public ExpressionList(List<IExpression> expressions) {
            this.expressions = expressions;
        }

        public List<IExpression> getExpressions() {
            return expressions;
        }

        public void setExpressions(List<IExpression> list) {
            expressions = list;
        }

        public void Accept(IItemsListVisitor itemsListVisitor) {
            itemsListVisitor.Visit(this);
        }

        public override string ToString() {
            return PlainSelect.getStringList(expressions, true, true);
        }
    }
}
