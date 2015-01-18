using NSqlParser.Statement.Select;

namespace NSqlParser.Expression
{
    public class AllComparisonExpression : IExpression {

        private SubSelect subSelect;

        public AllComparisonExpression(SubSelect subSelect) {
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
