using NSqlParser.Statement.Select;

namespace NSqlParser.Expression
{
    public class AnyComparisonExpression : IExpression {

        private SubSelect subSelect;

        public AnyComparisonExpression(SubSelect subSelect) {
            this.subSelect = subSelect;
        }

        public SubSelect getSubSelect() {
            return subSelect;
        }

	
        public void Accept(IExpressionVisitor expressionVisitor) {
            expressionVisitor.Visit(this);
        }
    }
}
